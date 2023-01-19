using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Interfaces.IServices
{
    public interface IRecepcionService
    {
        void MostrarCuartos();
        void MostrarCuartosDisponibles();
        Task RegistrarClienteAsync();
        Task ReservarCuartoAsync();
        Task CambiarEstadoCuartosAsync();
    }
}
