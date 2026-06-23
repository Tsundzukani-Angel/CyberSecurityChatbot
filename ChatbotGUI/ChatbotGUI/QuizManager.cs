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

                 "What makes a strong password?\n\nA) 12345678\nB) A combination of letters, numbers, and symbols\nC) Your pet's name\nD) Your birth year",
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
            
                "What is malware?\n\nA) A type of software designed to protect your computer\nB) A type of software that can harm your computer\nC) A type of hardware\nD) A type of network",
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
            
                "Which cybersecurity threat locks files and demands payment?\n\nA) Virus\nB) Ransomware\nC) Firewall\nD) Privacy",
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
            
                "What is the main purpose of a firewall?\n\nA) To speed up your internet connection\nB) To block unauthorized access to your network\nC) To store your files\nD) To create a backup of your data",
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
            
                "What should you check before clicking a link?\n\nA) URL\nB) Wallpaper\nC) Volume\nD) Brightness",
                "A",
                "Always verify URLs before clicking. Make sure the URL looks legitimate and matches the expected destination."
              )
            );

            Questions.Add(new Quiz(

                "You use the same password for your email, banking app, and social media account. A hacker obtains that password from a data breach on a shopping website. What cybersecurity risk does this create?\n\nA) Firewall bypass\nB) Password reuse attack\nC) Malware infection\nD) Ransomware attack",
                "B",
                "This is known as a password reuse attack. If one website is breached, attackers often try the same password on other accounts such as email, banking, and social media."

               )
            );

            Questions.Add(new Quiz(

                "True or False: Software updates can help protect against security vulnerabilities.",
                "true",
                "Software updates often include patches for security vulnerabilities, so keeping your software up to date is important for protecting your devices."
              )
            );

            Questions.Add(new Quiz(

                "What is two-factor authentication (2FA)?\n\nA) A method of logging in with just a password\nB) A method of logging in with a password and a second form of verification\nC) A method of logging in with just a fingerprint\nD) A method of logging in with just a username",
                "B",
                "Two-factor authentication (2FA) adds an extra layer of security by requiring not only a password but also a second form of verification, such as a code sent to your phone or a fingerprint scan."
              )
            );

            Questions.Add(new Quiz(

               "True or False: Two-factor authentication adds extra security to an account.",
               "true",
               "Two-factor authentication requires an extra verification step, making accounts harder to hack. "
               )
             );

            Questions.Add(new Quiz(

                "What should you do if you receive a suspicious email?\n\nA) Open it immediately\nB) Ignore all emails forever\nC) Verify the sender before clicking links\nD) Forward it to everyone",
                "C",
                "Always verify the sender and avoid clicking suspicious links. "
               )
            );

            Questions.Add(new Quiz(

                "True or False: Antivirus software should be updated regularly.",
                "true",
                "Updates help antivirus software detect the latest threats. This helps protect your device from new and emerging malware."
                )
            );

            Questions.Add(new Quiz(

                "Which of these is a social engineering attack?\n\nA) Phishing\nB) Firewall\nC) Encryption\nD) VPN",
                "A",
                "Phishing tricks people into revealing sensitive information. Phishing emails often appear to be from legitimate sources."
                )
            );

            Questions.Add(new Quiz(

                "True or False: Software updates often contain security fixes.",
                "true",
                "Updates patch vulnerabilities that attackers may exploit. Keeping your software up to date is crucial for maintaining security."
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
            string answer = userAnswer.Trim().ToLower();

            //user doesn't know the answer
            if(answer.Contains("don't know") ||
                answer.Contains("idk") ||
                answer.Contains("not sure") ||
                answer.Contains("no idea") ||
                answer.Contains("dont know"))
            {
                CurrentQuestion++;
                return $"📚 That's okay! Learning is part of cybersecurity.\n\n" +
                       $"Correct Answer: {currentQuiz.Answer}\n\n" +
                       $"{currentQuiz.Explanation}";
            }

            //user skips question
            if(answer.Contains("pass") || answer.Contains("skip"))
            {
                CurrentQuestion++;

                return $"⏭ Question skipped.\n\n" +
                       $"Correct Answer: {currentQuiz.Answer}\n\n" +
                       $"{currentQuiz.Explanation}";
            }

            //correct answer
            if (answer == currentQuiz.Answer.Trim().ToLower())
            {
                Score++;
                CurrentQuestion++;
                return $"✅Correct!\n\n{currentQuiz.Explanation}";
            }

            CurrentQuestion++;
            return $"❌Incorrect.\n\n" +
                   $"Correct Answer: {currentQuiz.Answer}\n\n" +
                   $"{currentQuiz.Explanation}";
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

            double percentage = ((double)Score / Questions.Count) * 100;

            return $"Your final score is: {Score}/{Questions.Count}\n" +
                   $"Percentage: {percentage:F0}%\n\n" +
                   feedback;
        }
    }
}
