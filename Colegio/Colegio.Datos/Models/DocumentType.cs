﻿using System;
using System.Collections.Generic;

namespace Colegio.Datos.Models
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
