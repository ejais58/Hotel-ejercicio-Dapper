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
    public class AdminDao : IAdminDao
    {
        string conn = ConnectionString.MiCadena();
        public async Task PostCuartoNormalAsync(int nroCuarto, int nroCama, string cochera, string tele, string desayuno, decimal precio, string estado)
        {
            string sqlQuery = "INSERT INTO Cuartos(Nro_Cuarto, Nro_Cama_Cuarto, Cochera_Cuarto, Tele_Cuarto, Desayuno_Cuarto, Precio_Cuarto, Estado_Cuarto)" +
                "VALUES (@nroCuarto,@nroCama,@cochera,@tele,@desayuno,@precio,@estado)";

            using (var db = new SqlConnection(conn))
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@nroCuarto", nroCuarto);
                parametros.Add("@nroCama", nroCama);
                parametros.Add("@cochera", cochera);
                parametros.Add("@tele", tele);
                parametros.Add("@desayuno", desayuno);
                parametros.Add("@precio", precio);
                parametros.Add("@estado", estado);

                await db.ExecuteAsync(sqlQuery, parametros, commandType: CommandType.Text);
            }
        }

        public async Task PostCuartoVipAsync(int nroCuarto, int nroCama, string cochera, string tele, string desayuno, string servicio, string hidromasaje, decimal precio, string estado)
        {
            string sqlQuery = "INSERT INTO Cuartos(Nro_Cuarto, Nro_Cama_Cuarto, Cochera_Cuarto, Tele_Cuarto, Desayuno_Cuarto, Servicio_Cuarto, Hidromasaje_Cuarto, Precio_Cuarto, Estado_Cuarto)" +
                "VALUES (@nroCuarto,@nroCama,@cochera,@tele,@desayuno,@servicio,@hidromasaje,@precio,@estado)";

            using (var db = new SqlConnection(conn))
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@nroCuarto", nroCuarto);
                parametros.Add("@nroCama", nroCama);
                parametros.Add("@cochera", cochera);
                parametros.Add("@tele", tele);
                parametros.Add("@desayuno", desayuno);
                parametros.Add("@servicio", servicio);
                parametros.Add("@hidromasaje", hidromasaje);
                parametros.Add("@precio", precio);
                parametros.Add("@estado", estado);

                await db.ExecuteAsync(sqlQuery, parametros, commandType: CommandType.Text);
            }
        }

        public async Task DeleteAuxTableItemAsync(int nroCuarto)
        {
            string sqlQuery = "DELETE AuxTable WHERE Nro_Cuarto_AuxTable = @numeroCuarto";

            using (var db = new SqlConnection(conn))
            {
                await db.ExecuteAsync(sqlQuery, new { numeroCuarto = nroCuarto });
            }
        }

        public async Task DeleteReservaAsync(int nroCuarto)
        {
            string sqlQuery = "DELETE Reserva WHERE Nro_Cuarto_Reserva = @numeroCuarto";

            using (var db = new SqlConnection(conn))
            {
                await db.ExecuteAsync(sqlQuery, new { numeroCuarto = nroCuarto });
            }
        }

        public async Task EnLimpiezaAsync(int nroCuarto)
        {
            string sqlQuery = "UPDATE Cuartos SET Estado_Cuarto = 'en limpieza' WHERE Nro_Cuarto = @numeroCuarto";

            using (var db = new SqlConnection(conn))
            {
                await db.ExecuteAsync(sqlQuery, new { numeroCuarto = nroCuarto });
            }
        }

        public async Task EnRenovacionAsync(int nroCuarto)
        {
            string sqlQuery = "UPDATE Cuartos SET Estado_Cuarto = 'en renovación' WHERE Nro_Cuarto = @numeroCuarto";
            using (var db = new SqlConnection(conn))
            {
                await db.ExecuteAsync(sqlQuery, new { numeroCuarto = nroCuarto });
            }
        }

        public async Task EnviarEstadoAsync(string estado, int numeroCuarto)
        {
            string sqlQuery = "INSERT INTO AuxTable(Nro_Cuarto_AuxTable, Estado_Cuarto_AuxTable) VALUES (@nroCuarto,@estadoCuarto)";

            using (var db = new SqlConnection(conn))
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@nroCuarto", numeroCuarto);
                parametros.Add("@estadoCuarto", estado);

                await db.ExecuteAsync(sqlQuery, parametros, commandType: CommandType.Text);
            }
        }

        public async Task<int> GetCuartoDisponibleByNro(int nroCuarto)
        {
            string sqlQuery = "SELECT * FROM Cuartos WHERE Nro_Cuarto = @numeroCuarto";

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

        public async Task<int> GetCuartoExiste(int nroCuarto)
        {
            string sqlQuery = "SELECT * FROM Cuartos WHERE Nro_Cuarto = @numeroCuarto";

            using (var db = new SqlConnection(conn))
            {
                var cuarto = await db.QueryFirstOrDefaultAsync<Cuarto>(sqlQuery, new { numeroCuarto = nroCuarto });
                if (cuarto == null)
                {
                    return nroCuarto;
                }
                return 0;
            }
        }

        public async Task<int> GetReservaNroCuarto(int nroCuarto)
        {
            string sqlQuery = "SELECT Id_Reserva FROM Reserva WHERE Nro_Cuarto_Reserva = @numeroCuarto";

            using (var db = new SqlConnection(conn))
            {
                var reserva = await db.QueryFirstOrDefaultAsync<Reserva>(sqlQuery, new { numeroCuarto = nroCuarto });
                if (reserva == null)
                {
                    return 0;
                }

                return nroCuarto;

            }
        }

        public async Task<string> GuardarEstadoAuxiliarAsync(int nroCuarto)
        {
            string anteriorEstado;

            string sqlQuery = "SELECT Estado_Cuarto_AuxTable FROM AuxTable WHERE Nro_Cuarto_AuxTable = @numeroCuarto";

            using (var db = new SqlConnection(conn))
            {
                var auxiliar = await db.QueryFirstOrDefaultAsync<Aux_Table>(sqlQuery, new { numeroCuarto = nroCuarto });
                return anteriorEstado = auxiliar.Estado_Cuarto_AuxTable;

            }
        }

        public async Task<string> GuardarEstadoCuartosAsync(int nroCuarto)
        {
            string anteriorEstado;

            string sqlQuery = "SELECT Estado_Cuarto FROM Cuartos WHERE Nro_Cuarto = @numeroCuarto";

            using (var db = new SqlConnection(conn))
            {
                var cuarto = await db.QueryFirstOrDefaultAsync<Cuarto>(sqlQuery, new { numeroCuarto = nroCuarto });
                return anteriorEstado = cuarto.Estado_Cuarto;

            }
        }
        public async Task VolverEstadoAnteriorAsync(string estado, int nroCuarto)
        {
            string sqlQuery = "UPDATE Cuartos SET Estado_Cuarto = @estado WHERE Nro_Cuarto = @numeroCuarto";

            using (var db = new SqlConnection(conn))
            {
                await db.ExecuteAsync(sqlQuery, new { estado = estado, numeroCuarto = nroCuarto });
            }
        }
    }
}
