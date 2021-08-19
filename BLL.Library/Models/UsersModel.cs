using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Library.Models
{
    public class UsersModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PW { get; set; }
        public string Username { get; set; }
        
        public int PointTotal { get; set; }
        public bool? AccountType { get; set; }
    }
}
