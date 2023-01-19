using AplicationCore.Interfaces.IDao;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DataAccess;
using AplicationCore.Entities;

namespace Infrastructure.Dao
{
    public class RecepcionDao : IRecepcionDao
    {
        string conn = ConnectionString.MiCadena();
        public async Task cambioEstadoAsync(string estado, int numeroCuarto)
        {
            string sqlQuery = "UPDATE Cuartos SET Estado_Cuarto = @estado WHERE Nro_Cuarto = @numeroCuarto";
            using (var db = new SqlConnection(conn))
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@estado", estado);
                parametros.Add("@numeroCuarto", numeroCuarto);

                await db.ExecuteAsync(sqlQuery, parametros, commandType: CommandType.Text);
            }
        }

        public async Task GenerarReservaAsync(int numeroCuarto, int dni, DateTime fechaDesde, DateTime fechaHasta)
        {
            string sqlQuery = "INSERT INTO Reserva(Nro_Cuarto_Reserva,Dni_Cliente_Reserva,Fecha_Desde_Reserva,Fecha_Hasta_Reserva) VALUES (@numeroCuarto,@dni,@fechaDesde, @fechaHasta)";

            using (var db = new SqlConnection(conn))
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@numeroCuarto", numeroCuarto);
                parametros.Add("@dni", dni);
                parametros.Add("@fechaDesde", fechaDesde);
                parametros.Add("@fechaHasta", fechaHasta);

                await db.ExecuteAsync(sqlQuery, parametros, commandType: CommandType.Text);
            }
        }

        public List<Cliente> GetCliente()
        {
            string sqlQuery = "SELECT * FROM Clientes";
            using (var db = new SqlConnection(conn))
            {
                var clientes = db.Query<Cliente>(sqlQuery).ToList();

                return clientes;

            }
        }

        public async Task<int> GetClienteByDniAsync(int dni)
        {
            string sqlQuery = "SELECT * FROM Clientes WHERE Dni_Cliente = @dniCliente";

            using (var db = new SqlConnection(conn))
            {
                var cliente = await db.QueryFirstOrDefaultAsync<Cliente>(sqlQuery, new { dniCliente = dni });
                if (cliente == null)
                {
                    return 0;
                }
                return dni;
            }
        }

        public async Task<int> GetCuartoDisponibleByNro(int nroCuarto)
        {
            string sqlQuery = "SELECT * FROM Cuartos WHERE Nro_Cuarto = @numeroCuarto AND Estado_Cuarto = 'disponible'";

            using (var db = new SqlConnection(conn))
            {
                var cuarto = await db.QueryFirstOrDefaultAsync<Cuarto>(sqlQuery, new { numeroCuarto = nroCuarto });
                if (cuarto == null)
                {
                    return 0;
                }
                return nroCuarto;
            }
        }

        public List<Cuarto> GetCuartos()
        {
            string sqlQuery = "SELECT * FROM Cuartos";

            using (var db = new SqlConnection(conn))
            {
                var cuartos = db.Query<Cuarto>(sqlQuery).ToList();

                return cuartos;

            }
        }

        public List<Cuarto> GetCuartosDisponibles()
        {
            string sqlQuery = "SELECT * FROM Cuartos WHERE Estado_Cuarto = 'disponible'";
            using (var db = new SqlConnection(conn))
            {
                var cuartos = db.Query<Cuarto>(sqlQuery).ToList();

                return cuartos;
            }
        }

        public async Task<int> getCuartosNroAsync(int nroCuarto)
        {
            string sqlQuery = "SELECT Estado_Cuarto FROM Cuartos WHERE Nro_Cuarto = @nroCuarto";
            using (var db = new SqlConnection(conn))
            {
                var cuarto = await db.QueryFirstOrDefaultAsync<Cuarto>(sqlQuery, new { nroCuarto = nroCuarto });
                if (cuarto.Estado_Cuarto.Equals("en limpieza") || cuarto.Estado_Cuarto.Equals("disponible"))
                {
                    return nroCuarto;
                }
                return 0;

            }
        }

        public async Task PostClienteAsync(int dni, string nombre, string apellido, string email)
        {
            string sqlQuery = "INSERT INTO Clientes(Dni_Cliente,Nombre_Cliente,Apellido_Cliente,Email_Cliente) VALUES (@dni,@nombre,@apellido,@email)";
            using (var db = new SqlConnection(conn))
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@dni", dni);
                parametros.Add("@nombre", nombre);
                parametros.Add("@apellido", apellido);
                parametros.Add("@email", email);

                await db.ExecuteAsync(sqlQuery, parametros, commandType: CommandType.Text);

                Console.WriteLine();
                Console.WriteLine("Cliente registrado...");
            }
        }

        public async Task UpdateCuartoOcupadoAsync(int numeroCuarto, DateTime fechaHasta)
        {
            string sqlQuery = "UPDATE Cuartos SET Estado_Cuarto = @estado WHERE Nro_Cuarto = @numeroCuarto";
            using (var db = new SqlConnection(conn))
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@estado", $"ocupado hasta el {fechaHasta}");
                parametros.Add("@numeroCuarto", numeroCuarto);

                await db.ExecuteAsync(sqlQuery, parametros, commandType: CommandType.Text);
            }
        }

        public async Task updateCuartoReservadoAsync(int numeroCuarto, DateTime fechaDesde)
        {
            string sqlQuery = "UPDATE Cuartos SET Estado_Cuarto = @estado WHERE Nro_Cuarto = @numeroCuarto";
            using (var db = new SqlConnection(conn))
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@estado", $"reservado para el dia {fechaDesde}");
                parametros.Add("@numeroCuarto", numeroCuarto);

                await db.ExecuteAsync(sqlQuery, parametros, commandType: CommandType.Text);
            }
        }
    }
}
