using System;
using System.Collections.Generic;

#nullable disable

namespace P2.DAL
{
    public partial class UserQuiz
    {
        public UserQuiz()
        {
            Results = new HashSet<Result>();
        }

        public int UserQuizId { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int QuizMaxScore { get; set; }
        public int QuizActualScore { get; set; }
        public DateTime QuizDate { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual Tuser User { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
