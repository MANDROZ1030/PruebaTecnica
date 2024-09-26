using System;
using System.Collections.Generic;

namespace PruebaTecnica.Server.Modelos
{
    public partial class EstudianteMaterium
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int MateriaId { get; set; }

        public virtual Estudiante Estudiante { get; set; } = null!;
        public virtual Materium Materia { get; set; } = null!;
    }
}
