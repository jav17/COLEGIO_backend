using System;
using System.Collections.Generic;

namespace Colegio.Datos.Models
{
    public partial class StudentState
    {
        public StudentState()
        {
            Students = new HashSet<Students>();
        }

        public int Id { get; set; }
        public string StudentState1 { get; set; }

        public virtual ICollection<Students> Students { get; set; }
    }
}
