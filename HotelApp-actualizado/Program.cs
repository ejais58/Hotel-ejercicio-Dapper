using AplicationCore.Entities;
using AplicationCore.Interfaces;
using AplicationCore.Interfaces.IDao;
using AplicationCore.Interfaces.IServices;
using HotelApp_actualizado.InyeccionDependencia;
using HotelApp_actualizado.Services;
using Infrastructure.Dao;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp_actualizado
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Inyeccion();
            await StartApp();
        }

        private static void Inyeccion()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<IUserDao, UserDao>();
            services.AddSingleton<IMenuOptionsService, MenuOptionsService>();
            services.AddSingleton<IRecepcionDao, RecepcionDao>();
            services.AddSingleton<IRecepcionService, RecepcionService>();
            services.AddSingleton<IAdminDao, AdminDao>();
            services.AddSingleton<IAdminService, AdminService>();
            Injector.GenerarProveedor(services);
        }

        private static async Task StartApp()
        {
            ILoginService loginService = Injector.GetService<ILoginService>();
            await loginService.login();
        }
    }
}
