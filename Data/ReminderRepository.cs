using Microsoft.Data.SqlClient;
using System.Data;
using ToDo_API.Models;

namespace ToDo_API.Data
{
    public class ReminderRepository
    {
        private IConfiguration configuration;
        public ReminderRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        #region GetAllReminder
        public List<ReminderModel> GetAllReminder()
        {
            var reminders = new List<ReminderModel>();
            ReminderModel reminder = new ReminderModel();

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Reminders_SelectAll";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                reminders.Add(new ReminderModel()
                {
                    ReminderID = Convert.ToInt32(reader["ReminderID"]),
                    TaskID = Convert.ToInt32(reader["TaskID"]),
                    ReminderTime = Convert.ToDateTime(reader["ReminderTime"]),
                    IsSent = Convert.ToBoolean(reader["IsSent"])
                });

            }
            return reminders;
        }
        #endregion

        #region GetReminderByID
        public List<ReminderModel> GetReminderByID(int ReminderID)
        {
            ReminderModel Reminder = new ReminderModel();
            var reminder = new List<ReminderModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Reminders_SelectByID";
            command.Parameters.AddWithValue("@ReminderID", ReminderID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reminder.Add(new ReminderModel
                {
                    ReminderID = Convert.ToInt32(reader["TaskID"]),
                    TaskID = Convert.ToInt32(reader["UserID"]),
                    ReminderTime = Convert.ToDateTime(reader["ReminderTime"]),
                    IsSent = Convert.ToBoolean(reader["IsSent"])


                });
            }

            return reminder;
        }
        #endregion

        #region GetReminderByTaskID
        public List<ReminderModel> GetReminderByTaskID(int TaskID)
        {
            ReminderModel Reminder = new ReminderModel();
            var reminder = new List<ReminderModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Reminders_GetRemindersByTask";
            command.Parameters.AddWithValue("@TaskID", TaskID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                reminder.Add(new ReminderModel
                {
                    ReminderID = Convert.ToInt32(reader["ReminderID"]),
                    ReminderTime = Convert.ToDateTime(reader["ReminderTime"]),
                    IsSent = Convert.ToBoolean(reader["IsSent"])


                });
            }

            return reminder;
        }
        #endregion

        #region AddReminder

        public bool AddReminder(ReminderModel reminderModel)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Reminders_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

           
            cmd.Parameters.AddWithValue("TaskID", reminderModel.TaskID);
            cmd.Parameters.AddWithValue("ReminderTime", reminderModel.ReminderTime);
          


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

        #region UpdateReminder
        public bool UpdateReminder(ReminderModel reminderModel)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Reminders_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("ReminderID", reminderModel.ReminderID);
            cmd.Parameters.AddWithValue("ReminderTime", reminderModel.ReminderTime);
            cmd.Parameters.AddWithValue("IsSent", reminderModel.IsSent);


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

        #region DeleteReminder
        public bool DeleteReminder(int ReminderID)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Reminders_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("ReminderID", ReminderID);

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

        #region Sent
        public bool ReminderSent(int ReminderID)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Reminders_Sent", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ReminderID", ReminderID);


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
