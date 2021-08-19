using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Library.Models;

namespace BLL.Library.IRepositories
{
    public interface IQuizRepo
    {
        Task<int> AddQuiz(QuizzesModel quiz);
        QuizzesModel GetQuizById(int QId);
        IEnumerable<QuizzesModel> GetAllQuizzes();
        Task<IEnumerable<QuizzesModel>> GetAllQuizesByCategoryAndDifficulty(string category, int difficulty);
        int GetLastQuizId();
    }
}
