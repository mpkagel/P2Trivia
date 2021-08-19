using System;
using System.Collections.Generic;
using System.Text;
using BLL.Library.Models;

namespace P2.DAL
{
    public interface IMapper
    {
        Answer Map(AnswerModel answers);
        AnswerModel Map(Answer answers);
        Question Map(QuestionsModel questions);
        QuestionsModel Map(Question questions);
        Quiz Map(QuizzesModel quizes);
        QuizzesModel Map(Quiz quizes);
        QuizQuestion Map(QuizQuestionsModel qq);
        QuizQuestionsModel Map(QuizQuestion qq);
        Result Map(ResultsModel results);
        ResultsModel Map(Result results);
        Review Map(ReviewsModel reviews);
        ReviewsModel Map(Review reviews);
        Tuser Map(UsersModel users);
        UsersModel Map(Tuser users);
        UserQuiz Map(UserQuizzesModel quizes);
        UserQuizzesModel Map(UserQuiz quizes);
        IEnumerable<Answer> Map(IEnumerable<AnswerModel> Answer);
        IEnumerable<AnswerModel> Map(IEnumerable<Answer> Answer);
        IEnumerable<Question> Map(IEnumerable<QuestionsModel> Question);
        IEnumerable<QuestionsModel> Map(IEnumerable<Question> Question);
        IEnumerable<Quiz> Map(IEnumerable<QuizzesModel> _Quiz);
        IEnumerable<QuizzesModel> Map(IEnumerable<Quiz> _Quiz);
        IEnumerable<QuizQuestion> Map(IEnumerable<QuizQuestionsModel> qq);
        IEnumerable<QuizQuestionsModel> Map(IEnumerable<QuizQuestion> qq);
        IEnumerable<Result> Map(IEnumerable<ResultsModel> Result);
        IEnumerable<ResultsModel> Map(IEnumerable<Result> Result);
        IEnumerable<Review> Map(IEnumerable<ReviewsModel> Review);
        IEnumerable<ReviewsModel> Map(IEnumerable<Review> Review);
        IEnumerable<UserQuiz> Map(IEnumerable<UserQuizzesModel> uQuiz);
        IEnumerable<UserQuizzesModel> Map(IEnumerable<UserQuiz> uQuiz);
        IEnumerable<Tuser> Map(IEnumerable<UsersModel> User);
        IEnumerable<UsersModel> Map(IEnumerable<Tuser> User);
    }
}
