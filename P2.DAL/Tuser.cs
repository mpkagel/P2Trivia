using System;
using System.Collections.Generic;

#nullable disable

namespace P2.DAL
{
    public partial class Tuser
    {
        public Tuser()
        {
            Reviews = new HashSet<Review>();
            UserQuizzes = new HashSet<UserQuiz>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pw { get; set; }
        public string Username { get; set; }
        
        public int PointTotal { get; set; }
        public bool? AccountType { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<UserQuiz> UserQuizzes { get; set; }
    }
}
