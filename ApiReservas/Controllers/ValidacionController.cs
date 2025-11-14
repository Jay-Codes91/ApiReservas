using ApiReservas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidacionController : ControllerBase
    {
        private readonly BdcalendarioContext _context;

        public ValidacionController(BdcalendarioContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Reserva reserva)
        {
            var fecha = reserva.Fecha;
            var inicio = reserva.Inicio;
            var fin = reserva.Fin;

            try
            {
                //var validacion = _context.Reservas.FromSqlInterpolated($"select * from Reservas where fecha = {fecha} AND ( ({inicio} > inicio and {inicio} < fin) or ({fin} < fin and {fin} > inicio) or ({inicio} < inicio and {fin} > fin))");
                var validacion = _context.Reservas.FromSqlInterpolated($"EXEC validacionFechas @fechan = {fecha}, @horaInicio = {inicio}, @horaFin = {fin}").AsEnumerable();
                if ( validacion.Count() > 0 )
                {
                    return NotFound(new {msj = "No puedes"});
                }
                else
                {
                    return Ok(new {msj = "Si puedes"});
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
