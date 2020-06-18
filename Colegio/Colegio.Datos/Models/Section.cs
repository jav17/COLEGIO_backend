using System;
using System.Collections.Generic;

namespace Colegio.Datos.Models
{
    public partial class Section
    {
        public Section()
        {
            Classrooms = new HashSet<Classrooms>();
        }

        public int Id { get; set; }
        public string Section1 { get; set; }

        public virtual ICollection<Classrooms> Classrooms { get; set; }
    }
}
