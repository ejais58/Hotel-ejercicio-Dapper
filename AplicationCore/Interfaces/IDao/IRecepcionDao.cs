using AplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Interfaces.IDao
{
    public interface IRecepcionDao
    {
        List<Cuarto> GetCuartos();
        List<Cuarto> GetCuartosDisponibles();
        Task PostClienteAsync(int dni, string nombre, string apellido, string email);
        List<Cliente> GetCliente();
        Task<int> GetClienteByDniAsync(int dni);
        Task<int> GetCuartoDisponibleByNro(int nroCuarto);
        Task GenerarReservaAsync(int numeroCuarto, int dni, DateTime fechaDesde, DateTime fechaHasta);
        Task UpdateCuartoOcupadoAsync(int numeroCuarto, DateTime fechaHasta);
        Task updateCuartoReservadoAsync(int numeroCuarto, DateTime fechaDesde);
        Task<int> getCuartosNroAsync(int nroCuarto);
        Task cambioEstadoAsync(string estado, int numeroCuarto);


    }
}
