using System;
using System.Collections.Generic;

#nullable disable

namespace P2.DAL
{
    public partial class Quiz
    {
        public Quiz()
        {
            UserQuizzes = new HashSet<UserQuiz>();
        }

        public int QuizId { get; set; }
        public int QuizMaxScore { get; set; }
        public int QuizDifficulty { get; set; }
        public string QuizCategory { get; set; }

        public virtual ICollection<UserQuiz> UserQuizzes { get; set; }
    }
}
