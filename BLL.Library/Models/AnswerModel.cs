using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL.Library.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }

        public string QuestionText { get; set; } = "";


        [Required]
        [MaxLength(500)]
        public string Answer { get; set; }

        [Required]
        public bool Correct { get; set; }
    }
}
