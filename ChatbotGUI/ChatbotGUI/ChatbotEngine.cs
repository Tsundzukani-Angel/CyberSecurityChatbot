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
        //storage for the chatbot's knowledge(responses) base
        private Dictionary<string, List<string>> responses;

        //object for random replies
        private Random random = new Random();

        //stored previous topic
        private string lastTpc = "";

        public ChatbotEngine()
        {
            responses = new Dictionary<string, List<string>>();
            
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
        }

        //the main chatbots response method
        public string GetResponse(string userInput)
        {
            userInput = userInput.ToLower();

            //follow up convesation flow
            if (userInput.Contains("tell me more") ||
                 userInput.Contains("explain more") ||
                 userInput.Contains("another tip"))
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

            if (userInput.Contains("curious"))
            {
                return "Curiosity is actually one of the best traits in cybersecurity.";
            }

            //keyword recognition
            foreach (var keyword in responses.Keys) {
                if (userInput.Contains(keyword)) {
                    lastTpc = keyword;
                    return GetRandomResponse(keyword);

                }
            }
            //default response
            return "I'm not sure i quite understand. Can you try rephrasing or ask about a specific cybersecurity topic like password safety, phishing, scams, or privacy?";
            
        }

        //returns random response
        private string GetRandomResponse(string keyword) 
        { 
            List<string> possibleResponses = responses[keyword];
            int index = random.Next(possibleResponses.Count);
            return possibleResponses[index];
        }
    }
}
