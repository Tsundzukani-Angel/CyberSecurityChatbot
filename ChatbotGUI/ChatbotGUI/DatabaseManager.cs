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
        public void AddTask(string title, string description, DateTime? reminderDate)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open(); // Open the connection to the database

                string query = @"INSERT INTO Tasks (Title, Description, ReminderDate) 
                                 VALUES (@title, @description, @reminderDate)";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@title", title);// Add the title parameter to the query
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@reminderDate", reminderDate);

                cmd.ExecuteNonQuery(); // Execute the query to insert the task into the database
            }
        }
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
    }
}
