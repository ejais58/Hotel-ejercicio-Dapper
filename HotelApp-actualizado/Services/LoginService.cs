using AplicationCore.Entities;
using AplicationCore.Interfaces;
using AplicationCore.Interfaces.IServices;
using HotelApp_actualizado.InyeccionDependencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp_actualizado.Services
{
    public class LoginService : ILoginService
    {
        public async Task login()
        {
            IMenuOptionsService menuOptionsService = Injector.GetService<IMenuOptionsService>();
            Personal persona = new Personal();
            do
            {
                Console.WriteLine("::Bienvenido::");
                Console.WriteLine("::Login::");
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Contraseña: ");
                string password = Console.ReadLine();

                IUserDao userDao = Injector.GetService<IUserDao>();
                persona = userDao.GetPersonal(email, password);

                if (persona == null)
                {
                    Console.WriteLine("Usuario no encontrado");
                }
                else if (persona.Roll_Personal.Equals("aap"))
                {
                    await menuOptionsService.MenuOpcionesAAP();
                }
                else if (persona.Roll_Personal.Equals("admin"))
                {
                    await menuOptionsService.MenuOpcionesAdmin();
                }
            } while (persona == null);
           
        }
    }
}
