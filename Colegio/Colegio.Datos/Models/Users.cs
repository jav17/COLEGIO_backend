using System;
using System.Collections.Generic;

namespace Colegio.Datos.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DocumentNumber { get; set; }
        public int DocumentTypeId { get; set; }
        public int UserType { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual UserType UserTypeNavigation { get; set; }
    }
}
