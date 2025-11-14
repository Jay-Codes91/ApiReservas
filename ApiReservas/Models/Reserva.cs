using System;
using System.Collections.Generic;

namespace ApiReservas.Models;

public partial class Reserva
{
    public int Id { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Fecha { get; set; } = null!;

    public string Inicio { get; set; } = null!;

    public string Fin { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}
