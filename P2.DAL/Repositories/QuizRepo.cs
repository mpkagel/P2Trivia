using BLL.Library.IRepositories;
using BLL.Library.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2.DAL.Repositories
{
    public class QuizRepo : IQuizRepo
    {
        private readonly ILogger<QuizRepo> _logger;
        private Project2Context _db { get; set; }
        private readonly IMapper _mapper;

        public QuizRepo(Project2Context dbContext,
            ILogger<QuizRepo> logger, IMapper mapper)
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

        public async Task<int> AddQuiz(QuizzesModel quiz)
        {
            Quiz newQuiz = new Quiz();
            newQuiz.QuizMaxScore = quiz.MaxScore;
            newQuiz.QuizDifficulty = quiz.Difficulty;
            newQuiz.QuizCategory = quiz.Category;
            _db.Quizzes.Add(newQuiz);
            return await SaveChangesAndCheckException();
        }

        public QuizzesModel GetQuizById(int QId)
        {

            try
            {
                return _mapper.Map(_db.Quizzes.Single(r => r.QuizId == QId));
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

        public IEnumerable<QuizzesModel> GetAllQuizzes()
        {
            try
            {
                return _mapper.Map(_db.Quizzes);
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

        public async Task<IEnumerable<QuizzesModel>> GetAllQuizesByCategoryAndDifficulty(string category, int difficulty)
        {
            try
            {
                return _mapper.Map(await _db.Quizzes
                   .Where(c => c.QuizCategory == category)
                   .Where(d => d.QuizDifficulty == difficulty)
                   .ToListAsync());
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

        public int GetLastQuizId()
        {
            try
            {
                return _db.Quizzes.Max(q => q.QuizId);
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
    }
}
