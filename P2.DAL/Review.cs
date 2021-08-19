using System;
using System.Collections.Generic;

#nullable disable

namespace P2.DAL
{
    public partial class Review
    {
        public int Rid { get; set; }
        public int? Qid { get; set; }
        public int? QuizId { get; set; }
        public int UserId { get; set; }
        public int Rratings { get; set; }

        public virtual Question QidNavigation { get; set; }
        public virtual Tuser User { get; set; }
    }
}
