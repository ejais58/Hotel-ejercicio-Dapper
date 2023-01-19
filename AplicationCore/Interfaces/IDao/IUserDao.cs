using AplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Interfaces
{
    public interface IUserDao
    {
        Personal GetPersonal(string email, string password);
    }
}
