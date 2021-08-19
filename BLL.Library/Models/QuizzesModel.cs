using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BLL.Library.Models
{
    public class QuizzesModel
    {
        [BindNever]
        public int Id { get; set; }
        public int MaxScore { get; set; }
        public int Difficulty { get; set; }
        public string Category { get; set; }
    }
}
