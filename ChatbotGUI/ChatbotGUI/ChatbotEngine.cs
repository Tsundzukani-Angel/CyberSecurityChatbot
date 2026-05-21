using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
namespace ChatbotGUI
{
    class ChatbotEngine
    {
        //storage for the chatbot's knowledge base
        private Dictionary<string, List<string>> responses;

        //object for random replies
        private Random random = new Random();

        //stored previous topic
        private string lastTpc = "";

        //remembers favorite topic
        private string favoriteTpc = "";

        //memory for last response indexes to avoid repetition
        private Dictionary<string, int> lastResponsesIndexes;
        public ChatbotEngine()
        {
            responses = new Dictionary<string, List<string>>();
            lastResponsesIndexes = new Dictionary<string, int>();

            //password safety responses
            responses ["password"] = new List<string>()
            {
                "A weak password is basically an open door for hackers. Try using symbols, numbers, and uppercase letters.",
                "Think of your password like your toothbrush — don’t share it and change it regularly.",
                "Using '123456' as a password is like locking your house with paper.",
                "Strong passwords protect your accounts from brute-force attacks and hackers.",
                "A password manager can remember strong passwords for you so you don’t have to stress.",
                "Try using passphrases instead of single words. Example: Coffee!Rain$Tiger2025"
            };

            //firewall responses
            responses["firewall"] = new List<string>()
            {
               "A firewall acts like a security guard between your device and the internet.",
               "Firewalls help block unauthorized access to your network.",
               "Both hardware and software firewalls improve cybersecurity.",
               "A firewall monitors incoming and outgoing network traffic.",
               "Firewalls can prevent hackers from accessing private systems.",
               "Turning off your firewall can make your device more vulnerable.",
               "Most operating systems include built-in firewall protection.",
               "Businesses use advanced firewalls to protect sensitive data.",
               "Firewalls help stop suspicious connections before they become threats.",
               "Combining a firewall with antivirus software improves protection."
            };

            //malware responses
            responses["malware"] = new List<string>()
            {
               "Malware is harmful software designed to damage systems or steal data.",
               "Examples of malware include spyware, ransomware, and trojans.",
               "Avoid suspicious downloads to reduce malware infections.",
               "Malware can secretly collect personal information from your device.",
               "Some malware slows down computers and causes crashes.",
               "Antivirus software helps detect and remove malware threats.",
               "Clicking unknown email attachments can install malware.",
               "Keeping your system updated reduces malware vulnerabilities.",
               "Free software from untrusted websites may contain malware.",
               "Hackers often spread malware through fake advertisements online."
            };

            //ransomware responses
            responses["ransomware"] = new List<string>()
            {
               "Ransomware locks your files until money is paid.",
               "Always back up important files to protect against ransomware.",
               "Never open suspicious email attachments.",
               "Ransomware attacks can affect businesses, schools, and hospitals.",
               "Paying ransomware attackers does not guarantee your files will return.",
               "Cybercriminals often spread ransomware through phishing emails.",
               "Keeping backups offline helps protect your data from ransomware.",
               "Antivirus software can help detect ransomware threats early.",
               "Some ransomware encrypts files within minutes after infection.",
               "Regular software updates reduce ransomware vulnerabilities."
            };

            //hacker responses
            responses["hacker"] = new List<string>()
            {
               "Hackers try to exploit weaknesses in systems.",
               "Ethical hackers help improve cybersecurity by testing security.",
               "Strong passwords reduce the risk of hacking.",
               "Hackers often target people through phishing emails and fake websites.",
               "Not all hackers are criminals — ethical hackers help companies find security flaws.",
               "Cybercriminals look for weak passwords because they are easy to break.",
               "Hackers sometimes use social engineering instead of advanced technology.",
               "Keeping software updated helps protect against hacking attempts.",
               "Public Wi-Fi networks can sometimes be targeted by hackers.",
               "Multi-factor authentication makes it harder for hackers to access accounts."

            };

            //phishing responses
            responses ["phishing"] = new List<string>()
            {
                "Not every email that says 'URGENT' is actually urgent.",
                "Phishing attacks usually try to scare or pressure you into acting quickly.",
                "If an email asks for your banking details, pause and verify first.",
                "Hackers LOVE fake links. Always hover over links before clicking.",
                "A message saying 'you won money' is probably the internet trying to humble you.",
                "Legitimate companies rarely ask for passwords through email."
            };

            //scam responses
            responses["scam"] = new List<string>()
            {
               "Scammers are professional manipulators. Stay calm before reacting.",
               "If something online feels suspicious, trust your instincts.",
               "Nobody randomly gives away free iPhones every day.",
               "Online scams often create fake urgency to pressure victims.",
               "Always verify websites before entering sensitive information.",
               "A secure website should usually start with HTTPS."
            };

            //privacy responses
            responses["privacy"] = new List<string>()
            {
                "Your personal data is valuable. Protect it like money.",
                "Oversharing online can expose you to cybercriminals.",
                "Location sharing on social media can become a privacy risk.",
                "Review your privacy settings regularly on apps and websites.",
                "Some apps collect WAY more data than people realize.",
                "Think carefully before posting personal information publicly."
            };

            //Virus responses
            responses["virus"] = new List<string>()
            {
                "Computer viruses can damage files and slow down systems.",
                "Avoid downloading files from unknown websites.",
                "Antivirus software helps protect your computer.",
                "Malware spreads through infected attachments and downloads.",
                "Keep your operating system updated for better protection.",
                "USB devices can also spread malware."
            };
        }

        //the main chatbots response method
        public string GetResponse(string userInput)
        {
            userInput = userInput.ToLower();
            //menu number recognition
            if (userInput == "1")
            {
                userInput = "password";
            }
            else if(userInput == "2") 
            {
                userInput = "phishing";
            } 
            else if(userInput == "3") 
            {
                userInput = "malware";
            }
            else if (userInput == "4")
            {
                userInput = "privacy";
            }
            else if (userInput == "5")
            {
                userInput = "firewall";
            }
            else if (userInput == "6")
            {
                userInput = "hacker";
            }
            else if (userInput == "7")
            {
                userInput = "scam";
            }
            else if (userInput == "8")
            {
                userInput = "ransomware";
            }
            else if (userInput == "9")
            {
                userInput = "virus";
            }

            //memory feature
            if (userInput.Contains("favourite topic"))
            {
                foreach(var keyword in responses.Keys)
                {
                    if(userInput.Contains(keyword))
                    {
                        favoriteTpc = keyword;
                        return $"Awesome! I'll remember that your favorite cybersecurity topic is {favoriteTpc}.";
                    }
                }
                //return "I couldn't identify a specific topic in your response, but I'll remember that you have a favorite topic. You can tell me more about it later!";
            }

            if (userInput.Contains("what is my favourite topic"))
            {
                if (favoriteTpc != "")
                {
                    return $"Your favorite cybersecurity topic is {favoriteTpc}.";
                }

                return "You haven't told me your favorite topic yet.";
            }

            //follow up convesation flow
            if (userInput.Contains("tell me more") ||
                 userInput.Contains("explain more") ||
                 userInput.Contains("another tip") ||
                 userInput.Contains("more") ||
                 userInput.Contains("continue") ||
                 userInput.Contains("another one"))
            {
                if (lastTpc != "")
                {
                    return GetRandomResponse(lastTpc);
                }

                return "Please specify a cybersecurity topic first.";

            }

           //sentiment detection 
            if (userInput.Contains("worried") || userInput.Contains("scared"))
            {
                return "It’s completely okay to feel worried about cybersecurity. The good thing is that learning safe habits already puts you ahead of many people.";
            }

            if (userInput.Contains("confused") || userInput.Contains("lost"))
            {
                return "No worries cybersecurity can feel overwhelming at first, but I’ll explain things step by step.";
            }
            if (userInput.Contains("frustrated") ||
                userInput.Contains("angry"))
            {
                return "Cybersecurity can definitely feel frustrating sometimes, but learning step by step makes it easier.";
            }

            if (userInput.Contains("nervous"))
            {
                return "It’s normal to feel nervous about online threats. Awareness is the first step toward staying safe.";
            }

            if (userInput.Contains("curious"))
            {
                return "Curiosity is actually one of the best traits in cybersecurity.";
            }
            if (userInput.Contains("bye") ||
                userInput.Contains("goodbye") ||
                userInput.Contains("exit"))
            {
                return "Goodbye! Stay safe online.";
            }
            //keyword recognition
            foreach (var keyword in responses.Keys)
            {
                if (userInput.Contains(keyword)) 
                {
                    lastTpc = keyword;
                    return GetRandomResponse(keyword);

                }
            }

            //default response
            return "I'm not sure i quite understand. Can you try rephrasing or ask about a specific cybersecurity topic like password safety, phishing, scams, or privacy?";
            
        }

        //Random response
        private string GetRandomResponse(string keyword) 
        { 
            List<string> possibleResponses = responses[keyword];
            int newIndex;

            //ensure we don't repeat the same response twice in a row for the same topic
            do
            {
                newIndex = random.Next(possibleResponses.Count);
            } while (
                   lastResponsesIndexes.ContainsKey(keyword) &&
                   lastResponsesIndexes[keyword] == newIndex &&
                   possibleResponses.Count > 1 );//if there's only one response, we have to repeat it, so we check for that

            //store the index of the last response given for this topic
            lastResponsesIndexes[keyword] = newIndex;
            return possibleResponses[newIndex];
        }
    }
}
