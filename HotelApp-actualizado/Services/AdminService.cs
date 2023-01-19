using AplicationCore.Interfaces.IDao;
using AplicationCore.Interfaces.IServices;
using HotelApp_actualizado.InyeccionDependencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp_actualizado.Services
{
    public class AdminService : IAdminService
    {
        IAdminDao adminDao = Injector.GetService<IAdminDao>();
        IRecepcionService recepcionService = Injector.GetService<IRecepcionService>();
        public async Task PostCuarto()
        {
            int option;
            int nroCuartoValid;
            int nroCuarto;
            Console.WriteLine("Selecciones tipo de habitacion: (1 - Normal) (2 - VIP)");
            Console.Write("Seleccion: ");
            option = int.Parse(Console.ReadLine());
            if (option == 1)
            {
                //metodo normal
                //validar nro cuarto
                do
                {
                    Console.Write("Seleccione número de cuarto: ");
                    nroCuarto = int.Parse(Console.ReadLine());
                    nroCuartoValid = await adminDao.GetCuartoExiste(nroCuarto);
                    if (nroCuartoValid == 0)
                    {
                        Console.WriteLine("El número de cuarto ya existe");
                    }

                } while (nroCuartoValid == 0);
                Console.Write("Nro de camas: ");
                int nroCama = int.Parse(Console.ReadLine());
                Console.Write("Incluye cochera: ");
                string cochera = Console.ReadLine();
                Console.Write("Incluye tele: ");
                string tele = Console.ReadLine();
                Console.Write("Incluye desayuno: ");
                string desayuno = Console.ReadLine();
                Console.Write("Precio: ");
                decimal precio = decimal.Parse(Console.ReadLine());
                Console.Write("Estado: ");
                string estado = Console.ReadLine();

                await adminDao.PostCuartoNormalAsync(nroCuarto, nroCama, cochera, tele, desayuno, precio, estado);

                Console.WriteLine("Cuarto normal creado");
            }
            else if (option == 2)
            {

                //metodo vip
                //validar nro cuarto
                do
                {
                    Console.Write("Seleccione número de cuarto: ");
                    nroCuarto = int.Parse(Console.ReadLine());
                    nroCuartoValid =await adminDao.GetCuartoExiste(nroCuarto);
                    if (nroCuartoValid == 0)
                    {
                        Console.WriteLine("El número de cuarto ya existe");
                    }

                } while (nroCuartoValid == 0);

                Console.Write("Nro de camas: ");
                int nroCama = int.Parse(Console.ReadLine());
                Console.Write("Incluye cochera: ");
                string cochera = Console.ReadLine();
                Console.Write("Incluye tele: ");
                string tele = Console.ReadLine();
                Console.Write("Incluye desayuno: ");
                string desayuno = Console.ReadLine();
                Console.Write("Incluye servicio al cuarto: ");
                string servicio = Console.ReadLine();
                Console.Write("Incluye hidromasajes: ");
                string hidromasaje = Console.ReadLine();
                Console.Write("Precio: ");
                decimal precio = decimal.Parse(Console.ReadLine());
                Console.Write("Estado: ");
                string estado = Console.ReadLine();

                await adminDao.PostCuartoVipAsync(nroCuarto, nroCama, cochera, tele, desayuno, servicio, hidromasaje, precio, estado);

                Console.WriteLine("Cuarto VIP creado");
            }
            else
            {
                Console.WriteLine("Valor ingresado incorrecto");
            }
        }

        public async Task CambiarEstadoAsyncAdmin()
        {
            
            int nroCuartoVerify;

            //mostrar cuartos
            recepcionService.MostrarCuartos();

            do
            {
                Console.Write("Seleccione número de cuarto: ");
                int numeroCuarto = int.Parse(Console.ReadLine());
                nroCuartoVerify = await adminDao.GetCuartoDisponibleByNro(numeroCuarto);
                if (nroCuartoVerify == 0)
                {
                    Console.WriteLine("El número de cuarto no existe");
                }

            } while (nroCuartoVerify == 0);


            Console.WriteLine("Selecciones al estado que desee cambiar: (1 - en limpieza) (2 - renovación) (3 - volver al estado anterior)");
            Console.Write("Selección: ");
            int estado = int.Parse(Console.ReadLine());


            if (estado == 1)
            {
                //metodo en limpieza
                //guardar estado de la tabla cuartos a la tabla auxiliar
                string estadoAnterior = await adminDao.GuardarEstadoCuartosAsync(nroCuartoVerify);
                await adminDao.EnviarEstadoAsync(estadoAnterior, nroCuartoVerify);
                await adminDao.EnLimpiezaAsync(nroCuartoVerify);
                Console.WriteLine("Cambios realizados");
            }
            else if (estado == 2)
            {
                //metodo renovacion
                await adminDao.EnRenovacionAsync(nroCuartoVerify);
                await adminDao.EnviarEstadoAsync("disponible", nroCuartoVerify);
                int reserva = await adminDao.GetReservaNroCuarto(nroCuartoVerify);
                if (reserva != 0)
                {
                    await adminDao.DeleteReservaAsync(nroCuartoVerify);
                }

                Console.WriteLine("Cambios realizados");


            }
            else if (estado == 3)
            {
                //Devolver el estado de la tabla auxiliar a la tabla cuartos y eliminar los datos de la tabla auxiliar
                string volverEstado = await adminDao.GuardarEstadoAuxiliarAsync(nroCuartoVerify);
                await adminDao.VolverEstadoAnteriorAsync(volverEstado, nroCuartoVerify);
                await adminDao.DeleteAuxTableItemAsync(nroCuartoVerify);
                Console.WriteLine("Cambios realizados");
            }
            else
            {
                Console.WriteLine("Número ingresado incorrecto");
            }
        }
    }
}
