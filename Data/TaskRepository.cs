using Microsoft.Data.SqlClient;
using System.Data;
using ToDo_API.Models;

namespace ToDo_API.Data

{
    public class TaskRepository
    {
        private IConfiguration configuration;
        public TaskRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        #region GetAllTask
        public List<TaskModel> GetAllTasks()
        {
            var tasks = new List<TaskModel>();
            TaskModel task = new TaskModel();

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Tasks_SelectAll";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                tasks.Add(new TaskModel()
                {
                    TaskID =  Convert.ToInt32(reader["TaskID"]) ,
                    UserID =  Convert.ToInt32(reader["UserID"]) ,
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                    Priority = Convert.ToInt32(reader["Priority"]),
                    Status = reader["Status"].ToString(),
                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])

                });

            }
            return tasks;
        }
        #endregion

        #region GetTaskByID
        public List<TaskModel> GetTaskByID(int TaskID)
        {
            TaskModel Task = new TaskModel();
            var task = new List<TaskModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Tasks_SelectByID";
            command.Parameters.AddWithValue("@TaskID", TaskID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                task.Add(new TaskModel
                {
                    TaskID = Convert.ToInt32(reader["TaskID"]),
                    UserID = Convert.ToInt32(reader["UserID"]),
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                    Priority = Convert.ToInt32(reader["Priority"]),
                    Status = reader["Status"].ToString(),
                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])


                });
            }

            return task;
        }
        #endregion

        #region GetTaskByUserID
        public List<TaskModel> GetTaskByUserID(int UserID)
        {
            TaskModel Task = new TaskModel();
            var task = new List<TaskModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Tasks_GetTasksByUser";
            command.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                task.Add(new TaskModel
                {
                    TaskID = Convert.ToInt32(reader["TaskID"]),
                    UserID =  Convert.ToInt32(reader["UserID"]) ,

                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                    Priority = Convert.ToInt32(reader["Priority"]),
                    Status = reader["Status"].ToString(),
                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])


                });
            }

            return task;
        }
        #endregion

        #region AddTask
        
        public bool AddTask(TaskModel taskModel)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Tasks_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("UserID", taskModel.UserID);
            cmd.Parameters.AddWithValue("Title", taskModel.Title);
            cmd.Parameters.AddWithValue("Description", taskModel.Description);
            cmd.Parameters.AddWithValue("DueDate", taskModel.DueDate);
            cmd.Parameters.AddWithValue("Priority", taskModel.Priority);
           



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

        #region UpdateTask
        public bool UpdateTask(TaskModel taskModel)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Tasks_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("TaskID", taskModel.TaskID);
            cmd.Parameters.AddWithValue("Title", taskModel.Title);
            cmd.Parameters.AddWithValue("Description", taskModel.Description);
            cmd.Parameters.AddWithValue("DueDate", taskModel.DueDate);
            cmd.Parameters.AddWithValue("Priority", taskModel.Priority);
            cmd.Parameters.AddWithValue("Status", taskModel.Status);
            cmd.Parameters.AddWithValue("CategoryID", taskModel.CategoryID);



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

        #region DeleteTask
        public bool DeleteTask(int TaskId)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Tasks_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("TaskID", TaskId);

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
