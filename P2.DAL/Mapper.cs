using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Library.Models;

namespace P2.DAL
{
    public class Mapper : IMapper
    {
        public Answer Map(AnswerModel answers) => new Answer
        {
            Aid = answers.Id,
            Qid = answers.QuestionId,
            Correct = answers.Correct,
            Aanswer = answers.Answer
        };

        public AnswerModel Map(Answer answers) => new AnswerModel
        {
            Id = answers.Aid,
            QuestionId = answers.Qid,
            Correct = answers.Correct,
            Answer = answers.Aanswer
        };

        public Question Map(QuestionsModel questions) => new Question
        {
            Qid = questions.Id,
            Qcategory = questions.Category,
            QaverageReview = questions.AverageReview,
            Qrating = questions.Rating,
            Qtype = questions.Type,
            Qstring = questions.Qstring
        };

        public QuestionsModel Map(Question questions) => new QuestionsModel
        {
            Id = questions.Qid,
            Category = questions.Qcategory,
            AverageReview = questions.QaverageReview,
            Rating = questions.Qrating,
            Type = questions.Qtype,
            Qstring = questions.Qstring
        };

        public Quiz Map(QuizzesModel quizes) => new Quiz
        {
            QuizId = quizes.Id,
            QuizDifficulty = quizes.Difficulty,
            QuizMaxScore = quizes.MaxScore,
            QuizCategory = quizes.Category
        };

        public QuizzesModel Map(Quiz quizes) => new QuizzesModel
        {
            Id = quizes.QuizId,
            Difficulty = quizes.QuizDifficulty,
            MaxScore = quizes.QuizMaxScore,
            Category = quizes.QuizCategory
        };

        public QuizQuestion Map(QuizQuestionsModel qq) => new QuizQuestion
        {
            QuizId = qq.QuizId,
            Qid = qq.Qid,
        };

        public QuizQuestionsModel Map(QuizQuestion qq) => new QuizQuestionsModel
        {
            QuizId = qq.QuizId,
            Qid = qq.Qid,
        };

        public Result Map(ResultsModel results) => new Result
        {
            ResultId = results.ResultId,
            UserQuizId = results.UserQuizId,
            Qid = results.Qid,
            Correct = results.Correct,
            UserAnswer = results.UserAnswer
        };

        public ResultsModel Map(Result results) => new ResultsModel
        {
            ResultId = results.ResultId,
            UserQuizId = results.UserQuizId,
            Qid = results.Qid,
            Correct = results.Correct,
            UserAnswer = results.UserAnswer
        };

        public Review Map(ReviewsModel reviews) => new Review
        {
            Rid = reviews.Id,
            Qid = reviews.Qid,
            QuizId = reviews.QuizId,
            UserId = reviews.UserId,
            Rratings = reviews.Ratings
        };

        public ReviewsModel Map(Review reviews) => new ReviewsModel
        {
            Id = reviews.Rid,
            Qid = reviews.Qid,
            QuizId = reviews.QuizId,
            UserId = reviews.UserId,
            Ratings = reviews.Rratings
        };

        public Tuser Map(UsersModel users) => new Tuser
        {
            UserId = users.UserId,
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            Pw = users.PW,
            //CreditCardNumber = users.CreditCardNumber,
            PointTotal = users.PointTotal,
            AccountType = users.AccountType

        };

        public UsersModel Map(Tuser users) => new UsersModel
        {
            UserId = users.UserId,
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            PW = users.Pw,
            //CreditCardNumber = users.CreditCardNumber,
            PointTotal = users.PointTotal,
            AccountType = users.AccountType
        };

        public UserQuiz Map(UserQuizzesModel quizes) => new UserQuiz
        {
            UserQuizId = quizes.UserQuizId,
            UserId = quizes.UserId,
            QuizId = quizes.QuizId,
            QuizMaxScore = quizes.QuizMaxScore,
            QuizDate = DateTime.Parse(quizes.QuizDate),
            QuizActualScore = quizes.QuizActualScore
        };

        public UserQuizzesModel Map(UserQuiz quizes) => new UserQuizzesModel
        {
            UserQuizId = quizes.UserQuizId,
            UserId = quizes.UserId,
            QuizId = quizes.QuizId,
            QuizMaxScore = quizes.QuizMaxScore,
            QuizDate = quizes.QuizDate.ToString(),
            QuizActualScore = quizes.QuizActualScore
        };

        public IEnumerable<Answer> Map(IEnumerable<AnswerModel> Answer) => Answer.Select(Map);

        public IEnumerable<AnswerModel> Map(IEnumerable<Answer> Answer) => Answer.Select(Map);

        public IEnumerable<Question> Map(IEnumerable<QuestionsModel> Question) => Question.Select(Map);

        public IEnumerable<QuestionsModel> Map(IEnumerable<Question> Question) => Question.Select(Map);

        public IEnumerable<Quiz> Map(IEnumerable<QuizzesModel> _Quiz) => _Quiz.Select(Map);

        public IEnumerable<QuizzesModel> Map(IEnumerable<Quiz> _Quiz) => _Quiz.Select(Map);

        public IEnumerable<QuizQuestion> Map(IEnumerable<QuizQuestionsModel> qq) => qq.Select(Map);

        public IEnumerable<QuizQuestionsModel> Map(IEnumerable<QuizQuestion> qq) => qq.Select(Map);

        public IEnumerable<Result> Map(IEnumerable<ResultsModel> Result) => Result.Select(Map);

        public IEnumerable<ResultsModel> Map(IEnumerable<Result> Result) => Result.Select(Map);

        public IEnumerable<Review> Map(IEnumerable<ReviewsModel> Review) => Review.Select(Map);

        public IEnumerable<ReviewsModel> Map(IEnumerable<Review> Review) => Review.Select(Map);

        public IEnumerable<UserQuiz> Map(IEnumerable<UserQuizzesModel> uQuiz) => uQuiz.Select(Map);

        public IEnumerable<UserQuizzesModel> Map(IEnumerable<UserQuiz> uQuiz) => uQuiz.Select(Map);

        public IEnumerable<Tuser> Map(IEnumerable<UsersModel> User) => User.Select(Map);

        public IEnumerable<UsersModel> Map(IEnumerable<Tuser> User) => User.Select(Map);
    }
}
