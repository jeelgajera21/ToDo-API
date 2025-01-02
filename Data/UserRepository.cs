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

        #region GetUserByID
        public List<UserModel> GetUserByID(int UserID)
        {
            UserModel User = new UserModel();
            var user = new List<UserModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_SelectByID";
            command.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                user.Add(new UserModel
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = (reader["UserName"].ToString()),
                    Email = (reader["Email"].ToString()),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"])


                });
            }

            return user;
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

        #region Login
        public List<UserLoginResponse> Login(UserLoginRequest userLoginRequest)
        {
            UserLoginRequest User = new UserLoginRequest();
            var user = new List<UserLoginResponse>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_Login";
            command.Parameters.AddWithValue("@UsernameOrEmail", userLoginRequest.UsernameOrEmail);
            command.Parameters.AddWithValue("@PasswordHash", userLoginRequest.PasswordHash);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                user.Add(new UserLoginResponse
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = (reader["UserName"].ToString()),
                    Email = (reader["Email"].ToString()),

                    IsActive = Convert.ToBoolean(reader["IsActive"])


                });
            }

            return user;
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

        #region DeleteUser
        public bool DeleteUser(int UserID)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_User_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("UserID", UserID);

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

        #region Active
        public bool UserActive(int UserID)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_User_Active", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", UserID);
           

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

        #region NotActive
        public bool UserInActive(int UserID)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_User_NotActive", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", UserID);


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
