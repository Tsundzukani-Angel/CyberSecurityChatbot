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
                 @"Weak passwords are one of the biggest cybersecurity risks online.
Hackers use automated tools to guess simple passwords within seconds.
Using uppercase letters, symbols, and numbers makes passwords much harder to crack.
Simple passwords like:
 a) 123456
 b) password
 c) qwerty

can be cracked within seconds.

A strong password should include:
 a) Uppercase letters
 b) Lowercase letters
 c) Numbers
 d) Symbols

Example:
Coffee!Rain$Tiger2025

Strong passwords make it much harder for cybercriminals to access your accounts.",

                @"Think of your password like your toothbrush — don’t share it and change it regularly.
Reusing passwords across multiple accounts increases your risk if one account becomes compromised.
Many users reuse the same password across multiple websites because it is easier to remember.
However, if one website gets hacked, attackers may try the same password on your:
 a) Email
 b) Banking apps
 c) Social media
 d) Gaming accounts

Using different passwords for different accounts improves cybersecurity significantly.",

                @"Password managers help generate and securely store strong passwords.
This reduces the temptation to reuse simple passwords across websites.
Instead of memorizing dozens of passwords, a password manager securely stores them for you.
This helps users avoid weak or repeated passwords.

Password managers can also:
 a) Suggest stronger passwords
 b) Auto-fill login details
 c) Warn users about compromised passwords

This improves both convenience and security.",

                @"Multi-factor authentication adds another layer of security.
Even if hackers steal your password, they may still need:
 a) A fingerprint
 b) A phone verification code
 c) Facial recognition

Many banks and social media platforms now recommend enabling multi-factor authentication for better security.
This makes unauthorized access far more difficult."
            };

            //firewall responses
            responses["firewall"] = new List<string>()
            {
                @"A firewall acts like a security guard between your device and the internet.

It monitors incoming and outgoing network traffic and blocks suspicious activity before it reaches your system.

Firewalls help:
 a) Prevent unauthorized access
 b) Block harmful traffic
 c) Protect sensitive information

Without a firewall, hackers may have an easier time accessing vulnerable devices.",

                @"There are different types of firewalls used in cybersecurity.

Examples include:
 a) Software firewalls
 b) Hardware firewalls
 c) Cloud firewalls

Software firewalls protect individual computers while hardware firewalls protect entire networks.

Businesses often combine multiple firewalls for stronger protection.",

               @"Turning off your firewall can expose your device to online threats.

Hackers constantly scan networks searching for weak or unprotected systems.

A firewall helps reduce risks by:
 a) Blocking unknown connections
 b) Monitoring traffic
 c) Filtering suspicious requests

Keeping your firewall enabled improves your overall cybersecurity.",
               @"Modern firewalls can do more than just block hackers.

Advanced firewalls may:
 a) Filter websites
 b) Detect malware activity
 c) Monitor applications
 d) Prevent suspicious downloads

This creates multiple layers of protection against cyberattacks."
            };

            //malware responses
            responses["malware"] = new List<string>()
            {
               @"Malware is harmful software designed to damage systems or steal information.

Common types of malware include:
 a) Viruses
 b) Trojans
 c) Spyware
 d) Ransomware
 e) Worms

Cybercriminals use malware to gain unauthorized access to devices and sensitive data.",

               @"Malware infections often happen through unsafe downloads and suspicious websites.

Attackers may disguise malware as:
 a) Free games
 b) Cracked software
 c) Fake applications
 d) Email attachments

Once installed, malware may operate silently in the background without the user noticing.",

               @"Some malware is designed to spy on users and collect personal information.

This information may include:
 a) Passwords
 b) Banking details
 c) Emails
 d) Browsing activity

Cybercriminals may use stolen information for fraud or identity theft.",

                @"Signs of malware infection may include:
 a) Slow computer performance
 b) Random popups
 c) Frequent crashes
 d) Unknown applications
 e) High internet usage

Running antivirus scans regularly can help detect and remove malware threats."
        };

            //ransomware responses
            responses["ransomware"] = new List<string>()
            {
                @"Ransomware is a type of malware that locks or encrypts files until money is paid.

Victims may lose access to:
 a) Documents
 b) Photos
 c) Business files
 d) Entire systems

Cybercriminals demand payment in exchange for restoring access.",

                @"Phishing emails are one of the most common ways ransomware spreads.

Attackers trick users into:
 a) Opening infected attachments
 b) Downloading fake files
 c) Clicking malicious links

One careless click can infect an entire network.",

                @"Backing up important files is one of the best defenses against ransomware.

Offline backups are especially useful because ransomware may also target connected drives.

Regular backups help users recover data without paying attackers.",

               @"Paying ransomware attackers does not guarantee file recovery.

Some victims never regain access to their files even after payment.

Cybersecurity experts usually recommend:
 a) Disconnecting infected systems
 b) Reporting attacks
 c) Restoring backups
 d) Improving security measures"
            };

            //hacker responses
            responses["hacker"] = new List<string>()
            {
               @"Hackers search for weaknesses in systems, networks, and human behavior.

Common weaknesses include:
 a) Weak passwords
 b) Outdated software
 c) Unsafe websites
 d) Human mistakes

Cybercriminals exploit these weaknesses to gain unauthorized access.",

               @"Not all hackers are criminals.

Different types of hackers include:
 a) White-hat hackers
 b) Black-hat hackers
 c) Grey-hat hackers

Ethical hackers help organizations improve security by testing systems legally and identifying vulnerabilities.",

                @"Social engineering is a technique hackers use to manipulate people instead of attacking computers directly.

Hackers may pretend to be:
 a) Bank employees
 b) Technical support agents
 c) Trusted companies

Their goal is to trick users into revealing sensitive information.",

               @"Public Wi-Fi networks can expose users to hacking risks.

Hackers on unsecured networks may intercept:
 a) Passwords
 b) Emails
 c) Banking information

Using secure networks and VPNs improves online safety.",

   @"Keeping software updated helps protect systems against hackers.

Software updates often include:
 a) Security patches
 b) Bug fixes
 c) Improved protection

Ignoring updates leaves devices vulnerable to known attacks."

            };

            //phishing responses
            responses ["phishing"] = new List<string>()
            {
                @"Phishing attacks are designed to trick users into revealing sensitive information.

Attackers often pretend to represent:
 a) Banks
 b) Online stores
 c) Social media platforms
 d) Government organizations

Their goal is to steal passwords, banking details, or personal information.",

    @"Phishing messages usually create panic or urgency.

Examples include:
 a) 'Your account will be locked!'
 b) 'Verify your banking details now!'
 c) 'You have won a prize!'

Cybercriminals pressure victims into acting quickly without thinking carefully.",

    @"Fake websites are commonly used during phishing attacks.

These websites may look identical to legitimate platforms but are designed to steal:
 a) Login credentials
 b) Credit card information
 c) Personal data

Always check website URLs carefully before entering information.",

    @"Phishing attacks can happen through:
 a) Emails
 b) SMS messages
 c) Phone calls
 d) Social media

Cybercriminals use multiple communication methods to target victims and spread scams."
            };

            //scam responses
            responses["scam"] = new List<string>()
            {
               @"Online scams are designed to trick users into giving away money or sensitive information.

Scammers often create fake situations involving:
 a) Emergencies
 b) Giveaways
 c) Investments
 d) Technical support

Their goal is to manipulate victims emotionally.",

   @"Many scams create a sense of urgency to pressure victims.

Messages may claim:
 a) Your account is compromised
 b) Payment is required immediately
 c) A prize is waiting

Scammers want users to react quickly instead of thinking carefully.",

   @"Fake shopping websites are common online scams.

These websites may:
 a) Offer unrealistic discounts
 b) Copy legitimate brands
 c) Collect payment information

Always verify websites before making purchases online.",

   @"Scammers sometimes impersonate trusted organizations.

Examples include:
 a) Banks
 b) Delivery companies
 c) Government agencies
 d) Technical support teams

Verifying suspicious communication independently helps reduce scam risks."
            };

            //privacy responses
            responses["privacy"] = new List<string>()
            {
                @"Personal information is valuable online and should be protected carefully.

Cybercriminals may use stolen information for:
 a) Identity theft
 b) Fraud
 c) Scams
 d) Account hijacking

Protecting your privacy reduces these cybersecurity risks.",

    @"Oversharing on social media can expose sensitive information.

Details such as:
 a) Your location
 b) School
 c) Workplace
 d) Daily routines

may help cybercriminals target users more effectively.",

    @"Privacy settings help control who can view your personal information online.

Reviewing settings regularly improves security on:
 a) Social media
 b) Mobile apps
 c) Websites

Limiting public access helps protect your digital identity.",

    @"Some applications collect more data than users realize.

Examples of collected data may include:
 a) Location history
 b) Contacts
 c) Browsing activity
 d) Device information

Checking app permissions helps users protect their privacy."
            };

            //Virus responses
            responses["virus"] = new List<string>()
            {
                @"Computer viruses are harmful programs designed to spread between devices and damage systems.

Viruses may:
 a) Corrupt files
 b) Slow down computers
 c) Delete information
 d) Disrupt system performance

Some viruses are also designed to steal personal information.",

    @"Viruses often spread through unsafe downloads and infected attachments.

Common infection sources include:
 a) Suspicious email attachments
 b) Pirated software
 c) Fake applications
 d) Infected USB devices

Avoiding unknown files helps reduce infection risks.",

    @"Antivirus software helps detect and remove computer viruses.

Antivirus programs can:
 a) Scan files
 b) Block threats
 c) Quarantine infected programs
 d) Protect systems in real time

Keeping antivirus software updated improves protection.",

    @"Signs of a virus infection may include:
 a) Slow performance
 b) Frequent crashes
 c) Strange popups
 d) Missing files
 e) Unusual activity

Running security scans regularly helps identify threats early."
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
            //task management recognition
            if (userInput.Contains("view tasks") ||
               userInput.Contains("show tasks") ||
               userInput.Contains("my tasks"))
            {
                return "SHOW_TASKS";
            }
            if(userInput.Contains("complete task"))
            {
                return userInput; //return the user input to be handled in MainWindow.xaml.cs
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
