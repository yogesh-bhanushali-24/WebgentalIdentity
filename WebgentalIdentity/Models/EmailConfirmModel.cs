using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebgentalIdentity.Models
{
    public class EmailConfirmModel
    {
        public string Email { get; set; }

        public bool IsConfirmed { get; set; }

        public bool EmailSent { get; set; }
        public bool Emailverified { get; set; }
    
    }
}
