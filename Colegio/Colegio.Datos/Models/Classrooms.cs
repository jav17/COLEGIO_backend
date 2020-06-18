using System;
using System.Collections.Generic;

namespace Colegio.Datos.Models
{
    public partial class Classrooms
    {
        public int Id { get; set; }
        public int GradeId { get; set; }
        public int SectionId { get; set; }
        public int Vacancies { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Section Section { get; set; }
    }
}
