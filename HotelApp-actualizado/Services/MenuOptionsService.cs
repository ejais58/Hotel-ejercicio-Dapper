using AplicationCore.Interfaces.IServices;
using HotelApp_actualizado.InyeccionDependencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp_actualizado.Services
{
    public class MenuOptionsService : IMenuOptionsService
    {
        IRecepcionService recepcionService = Injector.GetService<IRecepcionService>();
        public async Task MenuOpcionesAAP()
        {
            int option;
            
            do
            {
                Console.WriteLine();
                Console.WriteLine("::: Menu de opciones AAP :::");
                Console.WriteLine("1. Mostrar cuartos");
                Console.WriteLine("2. Mostrar cuartos disponibles");
                Console.WriteLine("3. Registrar cliente");
                Console.WriteLine("4. Reservar cuarto");
                Console.WriteLine("5. Cambiar estado de cuarto");
                Console.WriteLine("   Presione cualquier tecla para salir");

                Console.Write("Seleccion:");
                int.TryParse(Console.ReadLine(), out option);

                switch (option)
                {
                    case 1:
                        //Lista de cuartos
                        recepcionService.MostrarCuartos();
                        break;
                    case 2:
                        //Lista de cuartos disponibles
                        recepcionService.MostrarCuartosDisponibles();
                        break;
                    case 3:
                        //Registrar cliente
                        await recepcionService.RegistrarClienteAsync();
                        Console.WriteLine();
                        break;
                    case 4:
                        //Reservar cuarto
                        await recepcionService.ReservarCuartoAsync();
                        Console.WriteLine();
                        break;
                    case 5:
                        //Cambiar estado
                        await recepcionService.CambiarEstadoCuartosAsync();
                        break;
                }

            } while (option != 0);
        }

        public async Task MenuOpcionesAdmin()
        {
            int option;
            IAdminService adminService = Injector.GetService<IAdminService>();
            do
            {
                Console.WriteLine();
                Console.WriteLine("::: Menu de opciones Administrador :::");
                Console.WriteLine("1. Mostrar cuartos");
                Console.WriteLine("2. Agregar nuevo cuarto");
                Console.WriteLine("3. Cambiar estado de cuarto");
                Console.WriteLine("   Presione cualquier tecla para salir");

                Console.Write("Seleccion:");
                int.TryParse(Console.ReadLine(), out option);

                switch (option)
                {
                    case 1:
                        //Lista de cuartos
                        recepcionService.MostrarCuartos();
                        break;
                    case 2:
                        //Agregar nuevo cuarto
                        await adminService.PostCuarto();
                        break;
                    case 3:
                        //Cambiar estado
                        await adminService.CambiarEstadoAsyncAdmin();
                        break;
                }


            } while (option != 0);
        }
    }
}
