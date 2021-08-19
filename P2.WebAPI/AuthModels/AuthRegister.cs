using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace P2.WebAPI.AuthModels
{
    public class AuthRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long? CreditCardNumber { get; set; }
        public bool AccountType { get; set; }
    }
}
