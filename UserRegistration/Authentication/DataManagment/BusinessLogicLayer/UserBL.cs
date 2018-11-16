using Microsoft.Extensions.Configuration;

namespace Authentication.DataManagment.BusinessLogicLayer
{
    public class UserBL:IUserBL
    {
        private readonly DataAccessLayer.DataAccessLayer dataAccessLayer;

        public UserBL(IConfiguration configuration)
        {
            this.dataAccessLayer = new DataManagment.DataAccessLayer.DataAccessLayer(configuration["SqlConnection:ConnectionString"]);
        }

        public UserInfo FindUserById(int id)
        {
            return this.dataAccessLayer.GetUserById(id);
        }

        public UserInfo FindUserByLogin(string login)
        {
            return this.dataAccessLayer.GetUserByLogin(login);
        }
    }
}
