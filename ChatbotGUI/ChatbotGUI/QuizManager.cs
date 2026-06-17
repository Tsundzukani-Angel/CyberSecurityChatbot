using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotGUI
{
    internal class QuizManager
    {
        public List<Quiz> Questions { get; set; }
        public int CurrentQuestion { get; set; }
        public int Score { get; set; }
        public bool QuizMode { get; set; }

        public QuizManager()
        {
            Questions = new List<Quiz>();
            LoadQuestions();
        }
        private void LoadQuestions()
        {
            Questions.Add(new Quiz(

                 "True or False: Phishing attacks try to steal your personal information.",
                 "True",
                 "Phishing attacks often use fake emails or websites to trick you into giving away sensitive information like passwords or credit card numbers."
               )
            );

            Questions.Add(new Quiz(

                 "What makes a strong password?\n A) 12345678\n B) A combination of letters, numbers, and symbols\n C) Your pet's name\n D) Your birth year",
                 "B",
                 "A strong password should be a combination of letters, numbers, and symbols to make it more difficult to guess or crack."
                )
            );

            Questions.Add(new Quiz(

                "True or False: Firewalls help block unauthorized access.",
                "true",
                "Firewalls monitor and filter network traffic."
              )
            );

            Questions.Add(new Quiz(
            
                "What is malware?\n A) A type of software designed to protect your computer\n B) A type of software that can harm your computer\n C) A type of hardware\n D) A type of network",
                "B",
                "Malware is malicious software that can damage your computer, steal your information, or allow unauthorized access to your system."
              )
            );

            Questions.Add(new Quiz(
            
                "True or False: Public Wi-Fi is always completely safe.",
                "false",
                "Public Wi-Fi can expose users to cyber threats."
              )
            );

            Questions.Add(new Quiz(
            
                "Which cybersecurity threat locks files and demands payment?\nA) Virus\nB) Ransomware\nC) Firewall\nD) Privacy",
                "B",
                "Ransomware encrypts files and demands money."
              )
            );

            Questions.Add(new Quiz(
            
                "True or False: A VPN can help protect your online privacy.",
                "true",
                "A VPN encrypts your internet connection, making it more secure and private."
              )
            );

            Questions.Add(new Quiz(
            
                "What is the main purpose of a firewall?\nA) To speed up your internet connection\nB) To block unauthorized access to your network\nC) To store your files\nD) To create a backup of your data",
                "B",
                "A firewall monitors and filters network traffic to block unauthorized access. It acts as a barrier between your internal network and external networks, such as the internet."
              )
            );

            Questions.Add(new Quiz(
            
                "True or False: You should use the same password for multiple accounts.",
                "false",
                "Using the same password for multiple accounts increases the risk of unauthorized access if one account is compromised."
              )
            );

            Questions.Add(new Quiz(
            
                "What should you check before clicking a link?\nA) URL\nB) Wallpaper\nC) Volume\nD) Brightness",
                "a",
                "Always verify URLs before clicking. Make sure the URL looks legitimate and matches the expected destination."
              )
            );
        }

        public Quiz GetCurrentQuestion()
        {
            if(CurrentQuestion < Questions.Count)
            {
                return Questions[CurrentQuestion];
            }
                return null;
        }

        public string CheckAnswer(string userAnswer)
        {
            Quiz currentQuiz = Questions[CurrentQuestion];
            if(userAnswer.Trim().ToLower() == currentQuiz.Answer.Trim().ToLower())
            {
                Score++;
                CurrentQuestion++;
                return "✅Correct!\n " + currentQuiz.Explanation;
            }

            CurrentQuestion++;
            return "❌Incorrect.\n " + currentQuiz.Explanation;
            
        }
        
        public bool IsQuizFinished()
        {
            return CurrentQuestion >= Questions.Count;
        }

        public string GetFinalResult()
        {
            string feedback;
            if (Score >= 8)
            {
                feedback = "Excellent work! You have a strong understanding of cybersecurity concepts.";

            }
            else if (Score >= 5)
            {
                feedback = "Good job! You have a decent understanding of cybersecurity concepts, but there's room for improvement.";
            }
            else
            {
                feedback = "You might want to review some cybersecurity concepts. Keep learning and practicing!";
            }
            return $"Your final score is: {Score}/{Questions.Count}.\n\n{feedback}";
        }
    }
}
