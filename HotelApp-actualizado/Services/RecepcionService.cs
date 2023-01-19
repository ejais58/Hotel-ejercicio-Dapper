using AplicationCore.Interfaces.IDao;
using AplicationCore.Interfaces.IServices;
using HotelApp_actualizado.InyeccionDependencia;
using Infrastructure.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp_actualizado.Services
{
    public class RecepcionService : IRecepcionService
    {
        IRecepcionDao recepcionDao = Injector.GetService<IRecepcionDao>();
       

        public void MostrarCuartos()
        {
            
            var cuartos = recepcionDao.GetCuartos();

            Console.WriteLine("----------------------");
            Console.WriteLine("Habitaciones normales");
            Console.WriteLine("----------------------");
            foreach (var cuarto in cuartos)
            {
                if (cuarto.Servicio_Cuarto.Equals("no") && cuarto.Hidromasaje_Cuarto.Equals("no"))
                {
                    Console.WriteLine($" Nro cuarto: {cuarto.Nro_Cuarto} \n Nro de camas: {cuarto.Nro_Cama_Cuarto} \n Cochera: {cuarto.Cochera_Cuarto} \n Televisión: {cuarto.Tele_Cuarto} \n Desayuno: {cuarto.Desayuno_Cuarto} \n Precio: {cuarto.Precio_Cuarto} \n Estado: {cuarto.Estado_Cuarto}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("------------------");
            Console.WriteLine("Habitaciones VIP");
            Console.WriteLine("------------------");
            foreach (var cuarto in cuartos)
            {
                if (cuarto.Servicio_Cuarto.Equals("si") || cuarto.Hidromasaje_Cuarto.Equals("si"))
                {
                    Console.WriteLine($" Nro cuarto: {cuarto.Nro_Cuarto} \n Nro de camas: {cuarto.Nro_Cama_Cuarto} \n Cochera: {cuarto.Cochera_Cuarto} \n Televisión: {cuarto.Tele_Cuarto} \n Desayuno: {cuarto.Desayuno_Cuarto}\n Servicio al cuarto: {cuarto.Servicio_Cuarto}\n Hidromasajes: {cuarto.Hidromasaje_Cuarto} \n Precio: {cuarto.Precio_Cuarto} \n Estado: {cuarto.Estado_Cuarto}");
                }
                Console.WriteLine();
            }
        }

        public void MostrarCuartosDisponibles()
        {
            var cuartos = recepcionDao.GetCuartosDisponibles();

            Console.WriteLine("----------------------");
            Console.WriteLine("Habitaciones normales");
            Console.WriteLine("----------------------");
            foreach (var cuarto in cuartos)
            {
                if (cuarto.Servicio_Cuarto.Equals("no") && cuarto.Hidromasaje_Cuarto.Equals("no"))
                {
                    Console.WriteLine($" Nro cuarto: {cuarto.Nro_Cuarto} \n Nro de camas: {cuarto.Nro_Cama_Cuarto} \n Cochera: {cuarto.Cochera_Cuarto} \n Televisión: {cuarto.Tele_Cuarto} \n Desayuno: {cuarto.Desayuno_Cuarto} \n Precio: {cuarto.Precio_Cuarto} \n Estado: {cuarto.Estado_Cuarto}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("------------------");
            Console.WriteLine("Habitaciones VIP");
            Console.WriteLine("------------------");
            foreach (var cuarto in cuartos)
            {
                if (cuarto.Servicio_Cuarto.Equals("si") || cuarto.Hidromasaje_Cuarto.Equals("si"))
                {
                    Console.WriteLine($" Nro cuarto: {cuarto.Nro_Cuarto} \n Nro de camas: {cuarto.Nro_Cama_Cuarto} \n Cochera: {cuarto.Cochera_Cuarto} \n Televisión: {cuarto.Tele_Cuarto} \n Desayuno: {cuarto.Desayuno_Cuarto}\n Servicio al cuarto: {cuarto.Servicio_Cuarto}\n Hidromasajes: {cuarto.Hidromasaje_Cuarto} \n Precio: {cuarto.Precio_Cuarto} \n Estado: {cuarto.Estado_Cuarto}");
                }
                Console.WriteLine();
            }
        }

        public async Task RegistrarClienteAsync()
        {
            Console.Write("Dni: ");
            int dni = int.Parse(Console.ReadLine());
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            await recepcionDao.PostClienteAsync(dni,nombre,apellido,email);
        }

        public async Task ReservarCuartoAsync()
        {
            int dniVerify;
            int nroCuartoVerify;

            MostrarCuartosDisponibles();
            //Validar número de cuarto
            do
            {
                Console.Write("Seleccione número de cuarto: ");
                int numeroCuarto = int.Parse(Console.ReadLine());
                nroCuartoVerify = await recepcionDao.GetCuartoDisponibleByNro(numeroCuarto);
                if (nroCuartoVerify == 0)
                {
                    Console.WriteLine("El número de cuarto no existe o ya fue reservado");
                }

            } while (nroCuartoVerify == 0);

            var clientes = recepcionDao.GetCliente();
            Console.WriteLine("-----------");
            Console.WriteLine("Clientes");
            Console.WriteLine("-----------");
            foreach (var cliente in clientes)
            {
                Console.WriteLine($" Dni: {cliente.Dni_Cliente} \n Nombre completo: {cliente.Nombre_Cliente} {cliente.Apellido_Cliente} \n Email: {cliente.Email_Cliente}");
                Console.WriteLine();
            }
            //validar dni de cliente
            do
            {
                Console.Write("Seleccione Dni del cliente: ");
                int dni = int.Parse(Console.ReadLine());
                dniVerify = await recepcionDao.GetClienteByDniAsync(dni);
                if (dniVerify == 0)
                {
                    Console.WriteLine("El dni no pertenece a ningun cliente");
                }

            } while (dniVerify == 0);

            Console.Write("Ingrese fecha de ingreso: ");
            DateTime fechaDesde = DateTime.Parse(Console.ReadLine());
            Console.Write("Ingrese fecha de egreso: ");
            DateTime fechaHasta = DateTime.Parse(Console.ReadLine());

            //reservar
            await recepcionDao.GenerarReservaAsync(nroCuartoVerify,dniVerify,fechaDesde,fechaHasta);
            //Cambiar estado del cuarto reservado
            if (fechaDesde.Date == DateTime.Now.Date)
            {
                await recepcionDao.UpdateCuartoOcupadoAsync(nroCuartoVerify, fechaHasta);
            }
            else
            {
                await recepcionDao.updateCuartoReservadoAsync(nroCuartoVerify, fechaDesde);
            }

            Console.WriteLine("Reserva realizada");
        }

        public async Task CambiarEstadoCuartosAsync()
        {
            int nroCuartoVerify;
            int estado;
            //Lista de cuartos
            MostrarCuartos();

            do
            {
                Console.Write("Seleccione número de cuarto: ");
                int nroCuarto = int.Parse(Console.ReadLine());
                nroCuartoVerify = await recepcionDao.getCuartosNroAsync(nroCuarto);
                if (nroCuartoVerify == 0)
                {
                    Console.WriteLine("No se puede cambiar el estado");
                }

            } while (nroCuartoVerify == 0);


            Console.WriteLine("Selecciones al estado que desee cambiar: (1 - en limpieza) (2 - disponible)");
            Console.Write("Selección: ");
            int.TryParse(Console.ReadLine(), out estado);

            if (estado == 1)
            {
                //metodo limpieza
                await recepcionDao.cambioEstadoAsync("en limpieza", nroCuartoVerify);
                Console.WriteLine("Cambios realizados");
            }
            else if (estado == 2)
            {
                //metodo disponible
                await recepcionDao.cambioEstadoAsync("disponible", nroCuartoVerify);
                Console.WriteLine("Cambios realizados");
            }
            else
            {
                Console.WriteLine("Número ingresado incorrecto");
            }
        }
    }
}
