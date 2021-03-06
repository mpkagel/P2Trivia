using BLL.Library.IRepositories;
using BLL.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2.DAL.Repositories
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly ILogger<ReviewRepo> _logger;
        private readonly IMapper _mapper;
        private Project2Context Context { get; set; }

        public ReviewRepo(Project2Context dbContext,
            ILogger<ReviewRepo> logger, IMapper mapper)
        {
            Context = dbContext;
            _logger = logger;
            _mapper = mapper;

        }
        public List<ReviewsModel> GetReviewsByQuizId(int quizId)
        {
            try
            {
                List<ReviewsModel> reviews = _mapper.Map(Context.Reviews.Where(r => r.QuizId == quizId)).ToList();
                return reviews;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public List<ReviewsModel> GetReviewsByQuestionId(int questionId)
        {
            try
            {
                List<ReviewsModel> reviews = _mapper.Map(Context.Reviews.Where(r => r.Qid == questionId)).ToList();
                return reviews;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public List<ReviewsModel> GetReviewsByUserId(int userId)
        {
            try
            {
                List<ReviewsModel> reviews = _mapper.Map(Context.Reviews.Where(r => r.UserId == userId)).ToList();
                return reviews;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public List<ReviewsModel> GetReviewsByUserIdQuizOnly(int userId)
        {
            try
            {
                List<ReviewsModel> reviews = _mapper.Map(
                    Context.Reviews
                    .Where(r => r.UserId == userId)
                    .Where(q => q.QuizId != null))
                    .ToList();
                return reviews;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<ReviewsModel> GetAllReviews()
        {
            try
            {
                return _mapper.Map(Context.Reviews);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public List<ReviewsModel> GetAllQuizReviews()
        {
            try
            {
                return _mapper.Map(
                    Context.Reviews
                    .Where(r => r.QuizId != null)).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<int> AddReview(ReviewsModel review)
        {
            var value = _mapper.Map(review);
            Context.Add(value);

            try
            {
                await Context.SaveChangesAsync();
                return 1;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
        }

        public int DeleteReviewAsync(int Id)
        {
            try
            {
                Context.Remove(Context.Reviews.Find(Id));
                return 1;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
        }

        public async Task<int> Save()
        {
            try
            {
                await Context.SaveChangesAsync();
                return 1;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
        }

        public ReviewsModel GetByReviewId(int reviewId)
        {
            try
            {
                return _mapper.Map(Context.Reviews.Single(r => r.Rid == reviewId));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public List<ReviewsModel> GetReviewsByUserIdQuizId(int userId, int quizId)
        {
            try
            {
                List<ReviewsModel> reviews = _mapper.Map(
                    Context.Reviews
                    .Where(r => r.UserId == userId)
                    .Where(q => q.QuizId == quizId))
                    .ToList();
                return reviews;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
