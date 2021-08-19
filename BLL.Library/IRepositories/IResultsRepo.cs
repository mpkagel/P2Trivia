using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Library.Models;

namespace BLL.Library.IRepositories
{
    public interface IResultsRepo
    {
        IEnumerable<ResultsModel> GetAllResults();
        List<ResultsModel> GetResultsByUserQuizId(int userQuizId);
        ResultsModel GetResultsById(int resultId);
        Task<int> AddResults(ResultsModel result);
        Task<int> SaveChangesAndCheckException();
    }
}
