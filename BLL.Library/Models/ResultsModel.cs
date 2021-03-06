using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL.Library.Models
{
    public class ResultsModel
    {
        public int ResultId { get; set; }
        public int UserQuizId { get; set; }
        public int Qid { get; set; }

        [Required]
        [MaxLength(500)]
        public string UserAnswer { get; set; }

        [Required]
        public bool Correct { get; set; }
    }
}
