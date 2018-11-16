using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.DataManagment.BusinessLogicLayer
{
    public interface IUserBL
    {
        UserInfo FindUserById(int id);
        UserInfo FindUserByLogin(string login);
    }
}
