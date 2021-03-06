using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using P2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using BLL.Library.IRepositories;
using P2.DAL.Repositories;

namespace P2.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.WithOrigins(new[]
                    {
                        Configuration["P2MVCCORSURL"],
                        Configuration["P2AngularCORSURL"]
                    })
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddScoped<IUsersRepo, UsersRepo>();

            // add question into scope
            services.AddScoped<IQuestionRepo, QuestionRepo>();
            // add answers into scope
            services.AddScoped<IAnswersRepo, AnswersRepo>();
            services.AddScoped<IResultsRepo, ResultsRepo>();
            services.AddScoped<IReviewRepo, ReviewRepo>();

            services.AddScoped<IQuizRepo, QuizRepo>();
            services.AddScoped<IUserQuizzesRepo, UserQuizesRepo>();

            services.AddScoped<IQuizQuestionsRepo, QuizQuestionsRepo>();
            services.AddScoped<IAnswersRepo, AnswersRepo>();

            services.AddSingleton<IMapper, Mapper>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "P2", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("P2Auth")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // we could just use defaults and not set anything on options
                //options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                // many options here
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddDbContext<Project2Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("P2App")));

            var cookieName = Configuration["AuthCookieName"];
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = Configuration["AuthCookieName"];
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        // prevent redirect, just return unauthorized
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.Headers.Remove("Location");
                        // we use Task.FromResult when we're in an async context
                        // but there's nothing to await.
                        return Task.FromResult(0);
                    },
                    OnRedirectToAccessDenied = context =>
                    {
                        // prevent redirect, just return forbidden
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.Headers.Remove("Location");
                        // we use Task.FromResult when we're in an async context
                        // but there's nothing to await.
                        return Task.FromResult(0);
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "P2 v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
