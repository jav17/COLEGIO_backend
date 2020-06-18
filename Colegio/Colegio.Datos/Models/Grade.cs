using System;
using System.Collections.Generic;

namespace Colegio.Datos.Models
{
    public partial class Grade
    {
        public Grade()
        {
            Classrooms = new HashSet<Classrooms>();
        }

        public int Id { get; set; }
        public string Grade1 { get; set; }

        public virtual ICollection<Classrooms> Classrooms { get; set; }
    }
}
