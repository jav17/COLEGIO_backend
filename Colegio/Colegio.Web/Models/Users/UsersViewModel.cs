using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colegio.Web.Models.Users
{
    public class UsersViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public string UserType { get; set; }
    }
}
