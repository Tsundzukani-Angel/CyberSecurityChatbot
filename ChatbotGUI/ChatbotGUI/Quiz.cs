using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotGUI
{
    internal class Quiz
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Explanation { get; set; }

        public Quiz(string question, string answer, string explanation)
        {
            Question = question;
            Answer = answer;
            Explanation = explanation;
        }
    }
}
