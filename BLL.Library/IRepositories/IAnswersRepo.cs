using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Library.Models;

namespace BLL.Library.IRepositories
{
    public interface IAnswersRepo
    {
        AnswerModel GetAnswerById(int answerId);
        Task<IEnumerable<AnswerModel>> GetAnswerByQuestion(int questionId);
        List<AnswerModel> GetQuizAnswers(int quizId);
        Task<int> AddAnswer(AnswerModel answer);
        int DeleteAnswer(int Id);
        Task<int> Save();
    }
}
