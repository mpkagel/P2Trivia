using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Library.Models;

namespace BLL.Library.IRepositories
{
    public interface IUserQuizzesRepo
    {
        Task<IEnumerable<UserQuizzesModel>> GetUserQuizesByUser(int userId);
        List<UserQuizzesModel> GetUserQuizesByQuiz(int QId);
        UserQuizzesModel GetLastQuiz();
        Task<int> AddUserQuiz(UserQuizzesModel newUserQuizzesModel);
        Task<int> GetLastUserQuizId(int userId);
        Task<UserQuizzesModel> EditUserQuizzesAsync(UserQuizzesModel userQuiz);
        double GetMaxScoreOfQuiz(QuizzesModel quiz);
    }
}
