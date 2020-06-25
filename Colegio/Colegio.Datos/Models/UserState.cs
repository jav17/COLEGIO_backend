using System;
using System.Collections.Generic;

namespace Colegio.Datos.Models
{
    public partial class UserState
    {
        public UserState()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
