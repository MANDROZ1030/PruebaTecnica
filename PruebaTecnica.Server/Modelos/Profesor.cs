using System;
using System.Collections.Generic;

namespace PruebaTecnica.Server.Modelos
{
    public partial class Profesor
    {
        public Profesor()
        {
            Materia = new HashSet<Materium>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Materium> Materia { get; set; }
    }
}
