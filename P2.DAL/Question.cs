using System;
using System.Collections.Generic;

#nullable disable

namespace P2.DAL
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Results = new HashSet<Result>();
            Reviews = new HashSet<Review>();
        }

        public int Qid { get; set; }
        public string Qcategory { get; set; }
        public string Qtype { get; set; }
        public int Qrating { get; set; }
        public decimal? QaverageReview { get; set; }
        public string Qstring { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
