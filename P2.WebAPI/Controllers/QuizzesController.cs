using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Library.IRepositories;
using BLL.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace P2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class QuizzesController : ControllerBase
    {
        private readonly ILogger<QuizzesController> _logger;
        public IQuizRepo quizRepo { get; set; }
        public IUserQuizzesRepo _userQuizzesRepo { get; set; }
        public IQuizQuestionsRepo quizQuestionRepo { get; set; }
        public IAnswersRepo _answersRepo { get; set; }
        public IQuestionRepo _questionsRepo { get; set; }
        public IUsersRepo _usersRepo { get; set; }
        //private readonly Logger logger;

        public QuizzesController(IQuizRepo _quizRepo,
            IUserQuizzesRepo userQuizzesRepo,
            IQuizQuestionsRepo _quizQuestionRepo,
            IAnswersRepo answersRepo,
            IQuestionRepo questionsRepo,
            IUsersRepo usersRepo,
            ILogger<QuizzesController> logger)
        {
            quizRepo = _quizRepo;
            _userQuizzesRepo = userQuizzesRepo;
            _logger = logger;
            quizQuestionRepo = _quizQuestionRepo;
            _answersRepo = answersRepo;
            _usersRepo = usersRepo;
            _questionsRepo = questionsRepo;
            //logger = LogManager.GetLogger("allfile");
        }

        // GET: Quizzes/Create
        //[HttpGet("{Quizzes}", Name = "Create")]
        [HttpGet]
        public IEnumerable<QuizzesModel> Get()
        {
            IEnumerable<QuizzesModel> quizzes = quizRepo.GetAllQuizzes();
            return quizzes;
        }

        [HttpGet]
        [Route("Latest")]
        public UserQuizzesModel GetLastQuiz()
        {
            UserQuizzesModel quiz = _userQuizzesRepo.GetLastQuiz();
            return quiz;
        }

        [HttpGet]
        [Route("{id}/Answers")]
        public IEnumerable<AnswerModel> GetQuizAnswers(int id)
        {
            IEnumerable<AnswerModel> answers = _answersRepo.GetQuizAnswers(id);
            //logger.Debug(answers);
            return answers;
        }

        //GET: Quizzes/Find/5
        [HttpGet("{id}", Name = "GetQuizById")]
        public ActionResult<QuizzesModel> Find(int id)
        {
            return quizRepo.GetQuizById(id);
        }

        // POST: Quizzes
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] QuizzesModel quizzesModel)
        {
            try
            {
                //finds all questions that were on that quiz
                List<QuestionsModel> questions = quizQuestionRepo.GetQuestionsByQuizId(quizzesModel.Id);

                return CreatedAtAction(nameof(Create), questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return CreatedAtAction(nameof(Create), null);
            }
        }

        [HttpPost]
        [Route("Random")]
        public async Task<ActionResult> CreateRandomQuiz(QuizzesModel quiz)
        {
            try
            {
                //logger.Debug("Running generate random quiz");

                Random random = new Random();

                // Quiz
                quiz.MaxScore = 10;

                // Get some random quiz questions based upon difficulty
                int numQuestions = 10;

                List<QuestionsModel> categoryQuestions = _questionsRepo.GetQuestionByCategory(quiz.Category);

                List<QuestionsModel> questions1 = new List<QuestionsModel>();
                //_questionsRepo.GetQuestionByDifficultyAndCategory(
                // quiz.Difficulty, quiz.Category).Result;
                List<QuestionsModel> questions2 = new List<QuestionsModel>();
                List<QuestionsModel> questions3 = new List<QuestionsModel>();
                if (quiz.Difficulty == 1)
                {
                    questions1 = categoryQuestions.Where(cq => cq.Rating == 1).ToList();
                    questions2 = categoryQuestions.Where(cq => cq.Rating == 2).ToList();
                    questions3 = categoryQuestions.Where(cq => cq.Rating == 3).ToList();
                }
                else if (quiz.Difficulty == 3)
                {
                    questions1 = categoryQuestions.Where(cq => cq.Rating == 3).ToList();
                    questions2 = categoryQuestions.Where(cq => cq.Rating == 4).ToList();
                    questions3 = categoryQuestions.Where(cq => cq.Rating == 5).ToList();
                }
                else
                {
                    quiz.Difficulty = 2;
                    questions1 = categoryQuestions.Where(cq => cq.Rating == 2).ToList();
                    questions2 = categoryQuestions.Where(cq => cq.Rating == 3).ToList();
                    questions3 = categoryQuestions.Where(cq => cq.Rating == 4).ToList();
                }

                List<QuestionsModel> quizQuestionsPool = new List<QuestionsModel>();
                foreach (var item in questions1)
                {
                    quizQuestionsPool.Add(item);
                }
                foreach (var item in questions2)
                {
                    quizQuestionsPool.Add(item);
                }
                foreach (var item in questions3)
                {
                    quizQuestionsPool.Add(item);
                }

                List<QuestionsModel> quizQuestions = new List<QuestionsModel>();
                int randNum;
                for (int i = 0; i < numQuestions; i++)
                {
                    randNum = random.Next() % (15 - i);
                    quizQuestions.Add(quizQuestionsPool[randNum]);
                    quizQuestionsPool.RemoveAt(randNum);
                }
               
                int lastQuizId = quizRepo.GetLastQuizId();
                //quiz.Id = lastQuizId + 1;
                //logger.Debug(quiz.Id);
                //logger.Debug(lastQuizId);
                await quizRepo.AddQuiz(quiz);
                lastQuizId = quizRepo.GetLastQuizId();
                quiz.Id = lastQuizId;

                for (int i = 0; i < quizQuestions.Count(); i++)
                {
                    await quizQuestionRepo.AddQuizQuestion(lastQuizId, quizQuestions[i].Id);
                }

                return CreatedAtAction(nameof(Create), quiz);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString());
                //logger.Debug(ex.ToString());
                return CreatedAtAction(nameof(Create), null);
            }
        }

        // POST: Quizzes/Answers

        [HttpPost("Answers")]
        //[ProducesResponseType(typeof(List<AnswerModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> Answer([FromBody] List<QuestionsModel> quizQuestions)
        {
            List<AnswerModel> answers = new List<AnswerModel>();

            for (int i = 0; i < quizQuestions.Count; i++)
            {
                IEnumerable<AnswerModel> Ianswers = await _answersRepo.GetAnswerByQuestion(quizQuestions[i].Id);
                foreach (var item in Ianswers)
                {
                    answers.Add(item);
                }
            }

            return CreatedAtAction(nameof(Answer), answers);
        }

        //// GET: Quizzes/Edit/5
        //[HttpPut("{id}", Name = "EditQuizById")]
        //public ActionResult<QuizzesModel> Edit(int id)
        //{
        //    return quizRepo.GetQuizById(id);
        //}

        //// GET: Quizzes/Delete/5
        //[HttpDelete("{id}", Name = "DeleteQuizById")]
        //public ActionResult<QuizzesModel> Delete(int id)
        //{
        //    return quizRepo.GetQuizById(id);
        //}
    }
}