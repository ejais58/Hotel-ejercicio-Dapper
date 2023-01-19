using AplicationCore.Entities;
using AplicationCore.Interfaces;
using Dapper;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dao
{
    public class UserDao : IUserDao
    {
        public Personal GetPersonal(string email, string password)
        {
            string conn = ConnectionString.MiCadena();

            string sqlQuery = "SELECT Roll_Personal FROM Personal WHERE Email_Personal = @email AND Pass_Personal = @password";

            using (var db = new SqlConnection(conn))
            {
                var persona = db.Query<Personal>(sqlQuery, new { email = email, password = password }).FirstOrDefault();

                return persona;
            }
        }
    }
}
