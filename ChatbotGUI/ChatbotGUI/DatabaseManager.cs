using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChatbotGUI
{
    internal class DatabaseManager
    {
        private string connectionString = "server=localhost; user=root; port=3306; password=#Jesus@SNlord85; database=CyberSecurityDB";

        public bool TestConnecton()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        //this method adds a new task to the database with the provided title, description, and optional reminder date
        public void AddTask(string title, string description, DateTime? reminderDate)
        {
            string query = @"INSERT INTO Tasks (Title, Description, ReminderDate) 
                                 VALUES (@title, @description, @reminderDate)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open(); // Open the connection to the database

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@title", title);// Add the title parameter to the query
                cmd.Parameters.AddWithValue("@description", description);
                // Add the description and reminder date parameters to the query, handling null values for reminderDate
                cmd.Parameters.AddWithValue("@reminderDate", reminderDate.HasValue ? reminderDate : DBNull.Value);

                cmd.ExecuteNonQuery(); // Execute the query to insert the task into the database
            }
        }
        //this method retrieves all tasks from the database and returns them as a list of TaskItem objects
        public List<TaskItem> GetAllTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Tasks";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TaskItem task = new TaskItem();

                    task.TaskID = Convert.ToInt32(reader["TaskID"]);
                    task.Title = reader["Title"].ToString();
                    task.Description = reader["Description"].ToString();

                    if (reader["ReminderDate"] != DBNull.Value)
                    {
                        task.ReminderDate = Convert.ToDateTime(reader["ReminderDate"]);
                    }
                    task.Status = reader["Status"].ToString();
                    tasks.Add(task);
                }
            }
            return tasks;
        }
        //this method updates the status of a task to "Completed" based on its ID
        public void CompleteTask(int taskId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Tasks SET Status = 'Completed' WHERE TaskID = @taskId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@taskId", taskId);
                cmd.ExecuteNonQuery();
            }
        }
        //this method deletes a task from the database based on its ID
        public void DeleteTask(int taskId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Tasks WHERE TaskID = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", taskId);
                cmd.ExecuteNonQuery();
            }
        }

        //this method adds an activity log to the database with the provided action description
        public void AddActivityLog(string action)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO ActivityLog(ActionDescription)" + "VALUES (@action)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@action", action);

                cmd.ExecuteNonQuery();
            }
        }

        //this method retrieves the last 10 activity logs from the database and returns them as a list of strings
        public List<string> GetActivityLogs() 
        {
           List<string> logs = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM ActivityLog " + "ORDER BY ActionDate DESC LIMIT 10";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    logs.Add($"{reader["ActionDate"]} - {reader["ActionDescription"]}");
                }
            }
            return logs;
        }
        public void SaveBotResponse(string response)
        {
            string query = @"INSERT INTO ChatbotResponses(ResponseText) VALUES (@response)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@response", response);
                cmd.ExecuteNonQuery();
            }
        }
    }

}
