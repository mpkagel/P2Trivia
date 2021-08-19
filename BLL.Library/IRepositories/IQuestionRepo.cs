using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Library.Models;

namespace BLL.Library.IRepositories
{
    public interface IQuestionRepo
    {
        QuestionsModel GetQuestionById(int questionId);
        Task<int> AddQuestion(QuestionsModel question);
        List<QuestionsModel> GetQuestionByCategory(string category);
        List<QuestionsModel> GetQuestionByDifficulty(int difficulty);
        List<QuestionsModel> GetQuestionByDifficultyAndCategory(int difficulty, string category);
    }
}
