using System;
using System.Collections.Generic;

namespace Colegio.Datos.Models
{
    public partial class Students
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int StudentStateId { get; set; }
        public string DocumentNumber { get; set; }
        public int DocumentTypeId { get; set; }

        public virtual StudentState StudentState { get; set; }
    }
}
