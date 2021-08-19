using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace P2.DAL
{
    public partial class Project2Context : DbContext
    {
        public Project2Context()
        {
        }

        public Project2Context(DbContextOptions<Project2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Tuser> Tusers { get; set; }
        public virtual DbSet<UserQuiz> UserQuizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.Aid)
                    .HasName("PK__Answers__C6900628A24242C6");

                entity.ToTable("Answers", "P2");

                entity.Property(e => e.Aid).HasColumnName("AId");

                entity.Property(e => e.Aanswer)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("AAnswer");

                entity.Property(e => e.Qid).HasColumnName("QId");

                entity.HasOne(d => d.QidNavigation)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.Qid);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Qid)
                    .HasName("PK__Question__CAB1462B3334A5BB");

                entity.ToTable("Questions", "P2");

                entity.Property(e => e.Qid).HasColumnName("QId");

                entity.Property(e => e.QaverageReview)
                    .HasColumnType("decimal(6, 2)")
                    .HasColumnName("QAverageReview")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Qcategory)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("QCategory");

                entity.Property(e => e.Qrating)
                    .HasColumnName("QRating")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Qstring)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("QString");

                entity.Property(e => e.Qtype)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("QType")
                    .HasDefaultValueSql("('Multiple')");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quiz", "P2");

                entity.Property(e => e.QuizCategory)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.QuizDifficulty).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<QuizQuestion>(entity =>
            {
                entity.HasKey(e => new { e.QuizId, e.Qid })
                    .HasName("PK__QuizQues__27E9BAEC17616440");

                entity.ToTable("QuizQuestions", "P2");

                entity.Property(e => e.Qid).HasColumnName("QId");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("Results", "P2");

                entity.Property(e => e.Qid).HasColumnName("QId");

                entity.Property(e => e.UserAnswer).HasMaxLength(300);

                entity.HasOne(d => d.QidNavigation)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.Qid);

                entity.HasOne(d => d.UserQuiz)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.UserQuizId);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Rid)
                    .HasName("PK__Reviews__CAFF40D29D101EF8");

                entity.ToTable("Reviews", "P2");

                entity.Property(e => e.Rid).HasColumnName("RId");

                entity.Property(e => e.Qid).HasColumnName("QId");

                entity.Property(e => e.Rratings).HasColumnName("RRatings");

                entity.HasOne(d => d.QidNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.Qid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Reviews_Question_QId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Reviews_User_UserId");
            });

            modelBuilder.Entity<Tuser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__TUsers__1788CC4CA51CD972");

                entity.ToTable("TUsers", "P2");

                //entity.HasIndex(e => e.CreditCardNumber, "UQ__TUsers__315DB9256B962BB8")
                //    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__TUsers__536C85E4A3387B7C")
                    .IsUnique();

                entity.Property(e => e.AccountType).HasDefaultValueSql("((0))");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Pw)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("PW");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserQuiz>(entity =>
            {
                entity.ToTable("UserQuizzes", "P2");

                entity.Property(e => e.QuizDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.UserQuizzes)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK_UserQuizes_Quiz_QuizId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserQuizzes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserQuizes_User_UserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
