﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using P2.DAL;
using P2.WebAPI.AuthModels;
using BLL.Library.IRepositories;
using BLL.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using System.Net;
using NLog;

namespace P2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUsersRepo _usersRepo { get; set; }
        public IUserQuizzesRepo _userQuizzesRepo { get; set; }
        public SignInManager<IdentityUser> SignInManager { get; }
        private readonly ILogger<UsersController> _logger;
        //private readonly Logger logger;// = LogManager.GetLogger("default");

        public UsersController(IUsersRepo newUsersRepo,
            IUserQuizzesRepo newUserQuizzesRepo,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext dbContext,
            ILogger<UsersController> logger)
        {
            _usersRepo = newUsersRepo;
            _userQuizzesRepo = newUserQuizzesRepo;
            SignInManager = signInManager;
            //logger = LogManager.GetLogger("allfile");
            _logger = logger;
            dbContext.Database.EnsureCreated();

        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public AuthAccountDetails Details()
        {
            //logger.Debug("Account details");
            // if we want to know which user is logged in or which roles he has
            // apart from [Authorize] attribute...
            // we have User.Identity.IsAuthenticated
            // User.IsInRole("admin")
            // User.Identity.Name
            //logger.Info("Running details");
            if (!User.Identity.IsAuthenticated)
            {
                //_logger.LogInformation("");
                //logger.Info("User not authenticated");
                return null;
            }
            //logger.Debug("User authenticated, getting their details, binding to AuthAccountDetails");
            var details = new AuthAccountDetails
            {
                UserId = _usersRepo.GetUserId(User.Identity.Name),
                Username = User.Identity.Name,
                AccountType = User.IsInRole("admin"),
                Roles = User.Claims.Where(c => c.Type == ClaimTypes.Role)
                                   .Select(c => c.Value)
            };
            return details;
        }

        [HttpGet]
        [Route("Account")]
        public UsersModel GetDetails()
        {
            if (!User.Identity.IsAuthenticated)
            {
                _logger.LogInformation("");
                return null;
            }
            UsersModel details = _usersRepo.GetUserById(_usersRepo.GetUserId(User.Identity.Name));
            return details;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthLogin login,
            [FromServices] RoleManager<IdentityRole> roleManager,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            //logger.Debug(login.ToString);
            //logger.Debug(login.Username);
            //logger.Debug(login.Password);
            if (userManager.FindByNameAsync(login.Username) != null &&
                _usersRepo.CheckUserByName(login.Username))
            {
                //logger.Debug("attempting to login user");
                SignInResult result = await SignInManager.PasswordSignInAsync(
                login.Username, login.Password, login.RememberMe, false);

                //logger.Debug(result);
                if (!result.Succeeded)
                {
                    return Unauthorized(); // 401 for login failure
                }

                return NoContent();
            }
            else
            {
                return Forbid();
            }
        }

        // POST /account/logout
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();

            return NoContent();
        }

        // POST /account
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AuthRegister register,
            [FromServices] RoleManager<IdentityRole> roleManager,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            //logger.Info("Running Post Register");
            if (await userManager.FindByNameAsync(register.Username) == null)
            {
                var user = new IdentityUser { UserName = register.Username };

                IdentityResult createUserResult = await userManager.CreateAsync(user,
                    register.Password);

                if (!createUserResult.Succeeded) // e.g. did not meet password policy
                {
                    return BadRequest(createUserResult);
                }
                //logger.Info("register.AccountType: " + register.AccountType);
                if (register.AccountType == true)
                {
                    //logger.Info("trying to make admin account");
                    // make sure admin role exists
                    if (!await roleManager.RoleExistsAsync("admin"))
                    {
                        var role = new IdentityRole("admin");
                        IdentityResult createRoleResult = await roleManager.CreateAsync(role);
                        if (!createRoleResult.Succeeded)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError,
                                "failed to create admin role");
                        }
                    }

                    // add user to admin role
                    IdentityResult addRoleResult = await userManager.AddToRoleAsync(user, "admin");
                    if (!addRoleResult.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "failed to add user to admin role");
                    }
                }

                await SignInManager.SignInAsync(user, false);

                await _usersRepo.AddAsync(new UsersModel
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    PW = "a",
                    Username = register.Username,
                    
                    PointTotal = 0,
                    AccountType = register.AccountType
                });

                return NoContent(); // nothing to show the user that he can access
            }
            else
            {
                return Forbid();
            }
        }

        // GET: api/TUsers/5
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsersModel> GetById(int id)
        {
            return _usersRepo.GetUserById(id);
        }

        [HttpPost]
        [Route("{id}/Quizzes/")]
        public async Task<ActionResult> UserQuiz(int id, UserQuizzesModel userQuizzesModel)
        {
            userQuizzesModel.UserId = id;
            userQuizzesModel.QuizDate = DateTime.Now.ToString();
    
            await _userQuizzesRepo.AddUserQuiz(userQuizzesModel);

            return CreatedAtAction(nameof(UserQuiz), userQuizzesModel);
        }

        [HttpGet("{id}/Quizzes", Name = "GetUserQuizByUser")]
        public async Task<List<UserQuizzesModel>> GetUserQuizzesByUser(int userID)
        {
            IEnumerable<UserQuizzesModel> temp = await _userQuizzesRepo.GetUserQuizesByUser(userID);
            List<UserQuizzesModel> userQuizzes = temp.ToList();

            return userQuizzes;
        }

        [HttpPut]
        public async Task<ActionResult> EditUser([FromBody] UsersModel usersModel)
        {
            UsersModel currentUser = await _usersRepo.GetUserByName(usersModel.Username);

            currentUser.FirstName = usersModel.FirstName;
            currentUser.LastName = usersModel.LastName;
            

            await _usersRepo.EditUserAsync(currentUser);

            return CreatedAtAction(nameof(EditUser), usersModel);
        }

        [HttpPut("UserQuiz/{actualScore}", Name = "EditUserQuizScore")]
        public async Task<ActionResult> EditUserQuizScore(int actualScore)
        {

            UserQuizzesModel userQuiz = _userQuizzesRepo.GetLastQuiz();
            userQuiz.QuizActualScore = actualScore;

            await _userQuizzesRepo.EditUserQuizzesAsync(userQuiz);
            return CreatedAtAction(nameof(EditUserQuizScore), userQuiz);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(UsersModel usersModel,
            [FromServices] RoleManager<IdentityRole> roleManager,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            await SignInManager.SignOutAsync();

            UsersModel currentUser = await _usersRepo.GetUserByName(usersModel.Username);

            var user = new IdentityUser(usersModel.Username);

            await userManager.DeleteAsync(user);
            await _usersRepo.DeleteAsync(currentUser);

            return CreatedAtAction(nameof(DeleteUser), usersModel);
        }
    }
}