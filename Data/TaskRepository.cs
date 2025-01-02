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


            return tasks;
        }
    }
}
