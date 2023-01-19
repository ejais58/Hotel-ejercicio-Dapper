using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Entities
{
    public class Cliente
    {
        public int Dni_Cliente { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Apellido_Cliente { get; set; }
        public string Email_Cliente { get; set; }
    }
}
