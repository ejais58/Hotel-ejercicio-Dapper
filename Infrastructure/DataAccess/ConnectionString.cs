using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public class ConnectionString
    {
        public static string MiCadena()
        {
            return @"Data Source=DESKTOP-SM5HPM1\MSSQLSERVER1;Initial Catalog=Hotel;Persist Security Info=True;User ID=sa;Password=123456";
        }
    }
}
