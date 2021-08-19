using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Library.Models
{
    public class UserQuizzesModel
    {
        public int UserQuizId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int QuizId { get; set; }
        public int QuizMaxScore { get; set; }
        public int QuizActualScore { get; set; }
        public string QuizDate { get; set; }
    }
}
