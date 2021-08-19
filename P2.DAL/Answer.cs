using System;
using System.Collections.Generic;

#nullable disable

namespace P2.DAL
{
    public partial class Answer
    {
        public int Aid { get; set; }
        public int Qid { get; set; }
        public string Aanswer { get; set; }
        public bool Correct { get; set; }

        public virtual Question QidNavigation { get; set; }
    }
}
