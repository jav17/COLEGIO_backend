using System;
using System.Collections.Generic;

namespace Colegio.Datos.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string UserType1 { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
