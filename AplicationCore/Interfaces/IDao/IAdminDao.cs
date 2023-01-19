using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Interfaces.IDao
{
    public interface IAdminDao
    {
        Task PostCuartoNormalAsync(int nroCuarto, int nroCama, string cochera, string tele, string desayuno, decimal precio, string estado);
        Task PostCuartoVipAsync(int nroCuarto, int nroCama, string cochera, string tele, string desayuno, string servicio, string hidromasaje, decimal precio, string estado);
        Task<int> GetCuartoExiste(int nroCuarto);
        Task<int> GetCuartoDisponibleByNro(int nroCuarto);
        Task<string> GuardarEstadoCuartosAsync(int nroCuarto);
        Task EnviarEstadoAsync(string estado, int numeroCuarto);
        Task EnLimpiezaAsync(int nroCuarto);
        Task<string> GuardarEstadoAuxiliarAsync(int nroCuarto);
        Task VolverEstadoAnteriorAsync(string estado, int nroCuarto);
        Task DeleteAuxTableItemAsync(int nroCuarto);
        Task EnRenovacionAsync(int nroCuarto);
        Task DeleteReservaAsync(int nroCuarto);
        Task<int> GetReservaNroCuarto(int nroCuarto);
    }
}
