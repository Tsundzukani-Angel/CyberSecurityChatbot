using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotGUI
{
    internal class TaskItem
    {
        //the get and set methods are used to access the properties of the class
        public int TaskID { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }

        public string Status { get; set; } 

        
    }
}
