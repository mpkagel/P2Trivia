using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Library.Models;

namespace BLL.Library.IRepositories
{
    public interface IReviewRepo
    {
        ReviewsModel GetByReviewId(int reviewId);
        List<ReviewsModel> GetReviewsByQuizId(int quizId);
        List<ReviewsModel> GetReviewsByQuestionId(int questionId);
        List<ReviewsModel> GetReviewsByUserId(int userId);
        List<ReviewsModel> GetReviewsByUserIdQuizOnly(int userId);
        List<ReviewsModel> GetReviewsByUserIdQuizId(int userId, int quizId);
        List<ReviewsModel> GetAllQuizReviews();

        IEnumerable<ReviewsModel> GetAllReviews();

        Task<int> AddReview(ReviewsModel review);
        int DeleteReviewAsync(int Id);
        Task<int> Save();
    }
}
