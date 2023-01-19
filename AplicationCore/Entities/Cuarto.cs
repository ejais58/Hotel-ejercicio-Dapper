using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Entities
{
    public class Cuarto
    {
        public int Nro_Cuarto { get; set; }
        public int Nro_Cama_Cuarto { get; set; }
        public string Cochera_Cuarto { get; set; }
        public string Tele_Cuarto { get; set; }
        public string Desayuno_Cuarto { get; set; }
        public string Servicio_Cuarto { get; set; }
        public string Hidromasaje_Cuarto { get; set; }
        public decimal Precio_Cuarto { get; set; }
        public string Estado_Cuarto { get; set; }
    }
}
