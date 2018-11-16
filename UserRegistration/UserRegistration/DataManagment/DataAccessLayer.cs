using System.Collections.Generic;
using UserRegistration.DataModel;
using UserRegistration.DataManagment;
using System.Data.SqlClient;

namespace UserRegistration
{
    /// <summary>
    /// Data access layer of UserRegistration project.
    /// </summary>
    public class DataAccessLayer
    {
        /// <summary>
        /// Connection string to connect sql server.
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        public DataAccessLayer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Method to insert user to database.
        /// </summary>
        /// <param name="user"></param>
        public void InsertUser(User user)
        {
            string hashedPassword = PasswordSecurity.HashSHA1(user.Password);
            using (var connection = new SqlConnection(this.connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "InsertUser",
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@login", user.Login);
                command.Parameters.AddWithValue("@password", hashedPassword);
                connection.Open();
                command.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Method to get user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            User user = null;
            using(var connection=new SqlConnection(this.connectionString))
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
                    user = new User()
                    {
                        UserId = id,
                        FirstName = (string)dataReader["FirstName"],
                        LastName = (string)dataReader["LastName"],
                        Login = (string)dataReader["Login"]
                    };
                }
            }
            return user;
        }

        /// <summary>
        /// Method to get user by login.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <returns></returns>
        public User GetUserByLogin(string login)
        {
            User user = null;
            using (var connection = new SqlConnection(this.connectionString))
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
                    user = new User()
                    {
                        UserId = (int)dataReader["Id"],
                        FirstName = (string)dataReader["FirstName"],
                        LastName = (string)dataReader["LastName"],
                        Login = login
                    };
                }
            }
            return user;
        }

        /// <summary>
        /// Method to get all users from database.It must be authorized.
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            var users = new List<User>();
            using(var connection=new SqlConnection(this.connectionString))
            {
                var command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText = "GetUsers",
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                connection.Open();
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    users.Add(new User()
                    {
                        UserId = (int)dataReader["Id"],
                        FirstName = (string)dataReader["FirstName"],
                        LastName = (string)dataReader["LastName"],
                        Login = (string)dataReader["Login"]
                    });
                }
            }
            return users;
        }

        /// <summary>
        /// Method to delete user.
        /// </summary>
        /// <param name="id">User Id.</param>
        public void DeleteUser(int id)
        {
            using(var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText = "DeleteUser",
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@userId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
