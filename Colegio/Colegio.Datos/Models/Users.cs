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
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string DocumentNumber { get; set; }
        public int UserStateId { get; set; }
        public int DocumentTypeId { get; set; }
        public int RoleId { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual Role Role { get; set; }
        public virtual UserState UserState { get; set; }
    }
}
