using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Library.Models;

namespace BLL.Library.IRepositories
{
    public interface IQuizQuestionsRepo
    {
        List<QuestionsModel> GetQuestionsByQuizId(int QId);
        List<QuestionsModel> GetQuestionsByQuizAndQuestionId(int QId, int QuestionId);
        Task<int> AddQuizQuestion(int quizId, int questionId);
    }
}
