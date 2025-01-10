using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Scholarly.Models;

namespace Scholarly.Models
{
 
        public class Users
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Roles { get; set; }
        }





    
}
