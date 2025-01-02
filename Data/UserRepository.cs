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

            
        }
        #endregion
        #region AddUser
        public bool AddUsers(UserModel userModel)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_User_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
           
            cmd.Parameters.AddWithValue("UserName", userModel.UserName);
            cmd.Parameters.AddWithValue("Email", userModel.Email);
            cmd.Parameters.AddWithValue("PasswordHash", userModel.PasswordHash);



            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region UpdateUser
        public bool UpdateUser(UserModel userModel)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_User_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("UserID", userModel.UserID);
            cmd.Parameters.AddWithValue("UserName", userModel.UserName);
            cmd.Parameters.AddWithValue("Email", userModel.Email);/*
            cmd.Parameters.AddWithValue("PasswordHash", userModel.PasswordHash);*/

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        #endregion
    }
}
