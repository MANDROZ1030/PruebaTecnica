using System;
using System.Collections.Generic;

namespace PruebaTecnica.Server.Modelos
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            EstudianteMateria = new HashSet<EstudianteMaterium>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int ProgramaDeCreditos { get; set; }

        public virtual ICollection<EstudianteMaterium> EstudianteMateria { get; set; }
    }
}
