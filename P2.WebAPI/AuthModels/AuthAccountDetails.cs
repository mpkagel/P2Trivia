using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2.WebAPI.AuthModels
{
    public class AuthAccountDetails
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public bool AccountType { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
