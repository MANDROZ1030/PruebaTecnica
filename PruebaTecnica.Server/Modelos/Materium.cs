using System;
using System.Collections.Generic;

namespace PruebaTecnica.Server.Modelos
{
    public partial class Materium
    {
        public Materium()
        {
            EstudianteMateria = new HashSet<EstudianteMaterium>();
            Profesors = new HashSet<Profesor>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int? Creditos { get; set; }

        public virtual ICollection<EstudianteMaterium> EstudianteMateria { get; set; }

        public virtual ICollection<Profesor> Profesors { get; set; }
    }
}
