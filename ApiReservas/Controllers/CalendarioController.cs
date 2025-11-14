using ApiReservas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiReservas.Controllers
{
    [ApiController]
    public class CalendarioController : ControllerBase
    {
        private readonly BdcalendarioContext _context;

        public CalendarioController(BdcalendarioContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/Calendario/Completo")]
        public ActionResult GetReserva()
        {
            try
            {
                //var caledario = await _context.Reservas.ToListAsync();
                var calendario = _context.Reservas.Select(s => new
                {
                    id = s.Id,
                    cedula = s.Cedula,
                    nombre = s.Nombre,
                    apellido = s.Apellido,
                    title = s.Title,
                    fecha = s.Fecha,
                    inicio = s.Inicio,
                    fin = s.Fin,

                }).ToList();
                return Ok(calendario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Calendario/Fullcalendar")]
        public ActionResult GetCalendario()
        {
            try
            {
                var caledario = _context.Reservas.Select(s => new
                {
                    id = s.Id,
                    title = s.Title,
                    start = s.Start,
                    end = s.End
                });

                return Ok(caledario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Calendario/reserva/{id}")]
        public async Task<ActionResult> GetReservaId(int id)
        {
            try
            {
                var reserva = await _context.Reservas.Where(s => s.Id == id).FirstOrDefaultAsync();
                if (reserva == null)
                {
                    return NotFound(new { msj = "Reserva no encontrada" });
                }
                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Calendario/addReservas")]
        public async Task<IActionResult> PostReservas([FromBody] Reserva reserva)
        {
            var fecha = reserva.Fecha;
            var inicio = reserva.Inicio;
            var fin = reserva.Fin;

            try
            {
                //var validacion = _context.Reservas.FromSqlInterpolated($"select * from Reservas where fecha = {fecha} AND ( ({inicio} > inicio and {inicio} < fin) or ({fin} < fin and {fin} > inicio) or ({inicio} < inicio and {fin} > fin))");
                var validacion = _context.Reservas.FromSqlInterpolated($"EXEC validacionFechas @fechan = {fecha}, @horaInicio = {inicio}, @horaFin = {fin}").AsEnumerable();
                if (validacion.Count() > 0)
                {
                    return NotFound(new { msj = "No puedes hacer esta reserva" });
                }
                else
                {
                    await _context.AddAsync(reserva);
                    await _context.SaveChangesAsync();
                    return Ok(new { msj = "Añadido con éxito" });
                }
                    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/Calendario/deleteReserva/{id}")]
        public async Task<ActionResult> DeleteReserva(int id)
        {
            try
            {
                var reserva = await _context.Reservas.Where(s => s.Id == id).FirstOrDefaultAsync();
                if (reserva == null)
                {
                    return NotFound(new { msj = "Reserva no encontrada" });
                }

                _context.Remove(reserva);
                await _context.SaveChangesAsync();
                return Ok(new { msj = "Reserva eliminada con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
