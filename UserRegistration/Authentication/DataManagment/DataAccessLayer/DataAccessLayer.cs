using System.Data.SqlClient;

namespace Authentication.DataManagment.DataAccessLayer
{
    /// <summary>
    /// Data access layer of Authentication service.
    /// </summary>
    public class DataAccessLayer
    {
        private string connectionString;

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="connection">Sql connection string.</param>
        public  DataAccessLayer(string connection)
        {
            this.connectionString = connection;
        }

        /// <summary>
        /// Method to get user by login.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <returns></returns>
        public  UserInfo GetUserByLogin(string login)
        {
            UserInfo user = null;
            using(var connection=new SqlConnection(connectionString))
            {
                var command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText = "GetUserByLogin",
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@login", login);
                connection.Open();
                var dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    user = new UserInfo()
                    {
                        UserId = (int)dataReader["Id"],
                        FirstName = (string)dataReader["FirstName"],
                        LastName = (string)dataReader["LastName"],
                        Login = login,
                        Password = (string)dataReader["Password"]
                    };
                }
            }
            return user;
        }

        /// <summary>
        /// Method to get user by Id.
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        public UserInfo GetUserById(int id)
        {
            UserInfo user = null;
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText = "GetUserById",
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@userId", id);
                connection.Open();
                var dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    user = new UserInfo()
                    {
                        UserId = id,
                        FirstName = (string)dataReader["FirstName"],
                        LastName = (string)dataReader["LastName"],
                        Login = (string)dataReader["Login"],
                        Password = (string)dataReader["Password"]
                    };
                }
            }
            return user;
        }
    }
}
