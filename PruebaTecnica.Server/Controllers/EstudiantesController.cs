using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Server.Modelos;

namespace PruebaTecnica.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly PruebaTecnicaContext _context;

        public EstudiantesController(PruebaTecnicaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes()
        {
            return await _context.Estudiantes.Include(e => e.EstudianteMateria).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Estudiante>> CrearEstudiante(Estudiante estudiante)
        {
            if (estudiante.EstudianteMateria.Count != 3)
            {
                return BadRequest("Debe seleccionar exactamente 3 materias.");
            }

            var profesores = estudiante.EstudianteMateria
                                .Select(m => m.Materia.Profesors)
                                .Distinct();
            if (profesores.Count() != 3)
            {
                return BadRequest("No puede seleccionar materias con el mismo profesor.");
            }

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEstudiantes), new { id = estudiante.Id }, estudiante);
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
