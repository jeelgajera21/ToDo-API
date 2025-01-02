using ToDo_API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ToDo_API.Data
{
    public class UserRepository
    {
        private IConfiguration configuration;
        public UserRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        #region GetAllUser

        public List<UserModel> GetAllUser()
        {
            var users = new List<UserModel>();
            UserModel user = new UserModel();


            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_SelectAll";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new UserModel()
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = (reader["UserName"].ToString()),
                    Email = (reader["Email"].ToString()),
                   // PasswordHash = (reader["PasswordHash"].ToString()),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"])

                });

            }
            return users;
            #endregion
        }
    }
}
