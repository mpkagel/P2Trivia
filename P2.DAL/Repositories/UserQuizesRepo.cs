using BLL.Library.IRepositories;
using BLL.Library.Models;
using Microsoft.Extensions.Logging;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace P2.DAL.Repositories
{
    public class UserQuizesRepo : IUserQuizzesRepo
    {
        private readonly ILogger<UserQuizesRepo> _logger;
        private Project2Context _db { get; set; }
        private readonly IMapper _mapper;

        public UserQuizesRepo(Project2Context dbContext,
            ILogger<UserQuizesRepo> logger, IMapper mapper)
        {
            _db = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> SaveChangesAndCheckException()
        {
            try
            {
                await _db.SaveChangesAsync();
                return 0;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
                return 1;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return 1;
            }
        }

        public async Task<IEnumerable<UserQuizzesModel>> GetUserQuizesByUser(int userId)
        {
            try
            {
                IEnumerable<UserQuizzesModel> list = _mapper.Map(await (_db.UserQuizzes.Where(u => u.UserId == userId)).ToListAsync());
                return list;
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

        public List<UserQuizzesModel> GetUserQuizesByQuiz(int QId)
        {
            List<UserQuizzesModel> list = new List<UserQuizzesModel>();
            return list;
        }

        public double GetMaxScoreOfQuiz(QuizzesModel quiz)
        {
            return 0.0;
        }

        public UserQuizzesModel GetLastQuiz()
        {
            try
            {
                return _mapper.Map(_db.UserQuizzes.MaxBy(r => r.QuizDate).First());
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

        public async Task<int> AddUserQuiz(UserQuizzesModel userQuizzesModel)
        {
            UserQuiz newUserQuiz = new UserQuiz();
            newUserQuiz.UserId = userQuizzesModel.UserId;
            newUserQuiz.QuizId = userQuizzesModel.QuizId;
            newUserQuiz.QuizMaxScore = userQuizzesModel.QuizMaxScore;
            newUserQuiz.QuizActualScore = userQuizzesModel.QuizActualScore;
            newUserQuiz.QuizDate = DateTime.Parse(userQuizzesModel.QuizDate);
            _db.UserQuizzes.Add(newUserQuiz);
            return await SaveChangesAndCheckException();
        }

        public async Task<int> GetLastUserQuizId(int userId)
        {
            try
            {
                var userQuizzes = await _db.UserQuizzes.Where(uq => uq.UserId == userId).ToListAsync();
                return userQuizzes.Max(uq => uq.UserQuizId);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return 0;
            }
        }

        public async Task<UserQuizzesModel> EditUserQuizzesAsync(UserQuizzesModel userQuiz)
        {
            var entity = await _db.UserQuizzes.FindAsync(userQuiz.UserQuizId);
            _db.Entry(entity).CurrentValues.SetValues(_mapper.Map(userQuiz));
            await SaveChangesAndCheckException();
            return userQuiz;
        }
    }
}
