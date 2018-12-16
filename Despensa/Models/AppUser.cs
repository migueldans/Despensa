using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Despensa.Models
{
    public class AppUser:IdentityUser
    {
        public AppUser() : base()
        {
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public int NovaGroup { get; set; }
        public despensa despensa { get; set; }
    }
}
