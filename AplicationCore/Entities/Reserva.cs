using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Entities
{
    public class Reserva
    {
        public int Nro_Cuarto_Reserva { get; set; }
        public int Dni_Cliente_Reserva { get; set; }
        public DateTime Fecha_Desde_Reserva { get; set; }
        public DateTime Fecha_Hasta_Reserva { get; set; }
    }
}
