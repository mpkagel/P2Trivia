using System;
using System.Collections.Generic;

#nullable disable

namespace P2.DAL
{
    public partial class Result
    {
        public int ResultId { get; set; }
        public int UserQuizId { get; set; }
        public int Qid { get; set; }
        public string UserAnswer { get; set; }
        public bool Correct { get; set; }

        public virtual Question QidNavigation { get; set; }
        public virtual UserQuiz UserQuiz { get; set; }
    }
}
