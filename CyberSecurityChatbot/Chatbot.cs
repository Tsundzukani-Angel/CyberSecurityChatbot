using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CyberSecurityChatbot
{
    internal class Chatbot
    {
        string name;
        public void Start()
        {
            try
            {
                //displays a welcome message and prompt user for their name
                

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Chatbot: ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                UIHelper.TypeText("Welcome to the Cybersecurity Awareness Chatbot!");
                UIHelper.TypeText("Before we get started, what's your name? ");
                Console.ResetColor();

                Console.Write("You: ");
                name = Console.ReadLine();

                //Validates that the name is not empty or whitespace.
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    UIHelper.TypeText("Name cannot be empty. Please enter your name.");
                    Console.ResetColor();

                    Console.Write("You: ");
                    name = Console.ReadLine();
                }

                //Personalizes the welcome message using the user's name and introduces the chatbot's purpose
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Chatbot: ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                UIHelper.TypeText($"Hello, {name}! Welcome to the Cybersecurity Awareness Chatbot.");
                UIHelper.TypeText("I can help you stay safe online.");
                UIHelper.TypeText("\nSelect a topic from the menu below");
                Console.ResetColor();

                //controls the main interaction loop of the chatbot, allowing the user to select different cybersecurity topics until they choose to exit
                bool valid = true;
                while (valid)
                {
                    try
                    {
                        //display the chatbot menu options and prompt the user to select one
                        DisplayMenu();

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"\n{name}: ");
                        Console.ResetColor();

                        string userInput = Console.ReadLine().ToLower();

                        //Validates user input to ensure it's not empty and is a valid number corresponding to the menu options
                        if (string.IsNullOrWhiteSpace(userInput))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            UIHelper.TypeText("Input cannot be empty.");
                            continue;
                            Console.ResetColor();
                        }

                        //handle keyword based input using string manipulation
                        if (userInput.Contains("password"))
                        {
                            PasswordSafetyMenu();
                            continue;
                        }
                        else if (userInput.Contains("phishing"))
                        {
                            PhishingMenu();
                            continue;
                        }
                        else if (userInput.Contains("virus"))
                        {
                            ComputerVirusMenu();
                            continue;
                        }
                        else if (userInput.Contains("browsing"))
                        {
                            SafeBrowsingMenu();
                            continue;
                        }
                        else if (userInput.Contains("exit"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            UIHelper.TypeText($"Stay safe online! Goodbye {name}");
                            Console.ResetColor();
                            break;
                        }

                        if (!int.TryParse(userInput, out int choice))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            UIHelper.TypeText("I didn't quite get that, you can choose from the menu below.");
                            continue;
                            Console.ResetColor();
                        }

                        //display chatbot response based on the user's menu selection, providing information on various cybersecurity topics and
                       //allowing the user to exit when they choose to do so

                        switch (choice)
                        {
                            case 1:
                                UIHelper.ShowTyping();
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("Chatbot: ");
                                Console.ResetColor();

                                Console.ForegroundColor = ConsoleColor.Green;
                                UIHelper.TypeText("Cybersecurity is the practice of protecting computers, servers, mobile devices, electronic systems, networks,\n and data from malicious attacks.");
                                UIHelper.TypeText("It involves implementing security measures to prevent unauthorized access and ensure the confidentiality, integrity, and availability of information.");
                                Console.ResetColor();
                                break;

                            case 2:

                                PasswordSafetyMenu();
                                
                                break;
                            case 3:
                                PhishingMenu();
                                    
                                break;
                            case 4:
                                ComputerVirusMenu();

                                break;
                            case 5:
                                SafeBrowsingMenu();
                                
                                break;
                            case 6:
                                Console.ForegroundColor = ConsoleColor.Green;
                                UIHelper.TypeText("Remember, cybersecurity is an ongoing effort. Stay informed about the latest threats and best practices to keep yourself and your information safe online.");
                                UIHelper.TypeText($"Stay safe online! Goodbye {name}");
                                Console.ResetColor();
                                valid = false;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                UIHelper.TypeText("Invalid choice. Please select a valid choice from the menu.");
                                Console.ResetColor();
                                break;

                        }
                    }
                    //handles any unexpected errors that may occur during the chatbot interaction
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        UIHelper.TypeText($"An error occurred: {ex.Message}");
                        Console.ResetColor();

                    }
                }
            }
            
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                UIHelper.TypeText($"An error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }
        //displays the chatbot menu options
        public void DisplayMenu()
        {
           Console.ForegroundColor = ConsoleColor.Cyan;
           Console.WriteLine("**************************************");
           Console.WriteLine("          CYBERSECURITY MENU          ");
           Console.WriteLine("**************************************");
           Console.WriteLine("1. What is cybersecurity?");
           Console.WriteLine("2. Password safety");
           Console.WriteLine("3. Phishing Awareness");
           Console.WriteLine("4. Computer viruses");
           Console.WriteLine("5. Safe browsing");
           Console.WriteLine("6. Exit");
           Console.ResetColor();

        }
        //Password safety menu
        public void PasswordSafetyMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\n********************* PASSWORD SAFETY *********************\n\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("1. What is meant by a strong Password?");
                Console.WriteLine("2. How to create a strong password.");
                Console.WriteLine("3. Common password mistsakes.");
                Console.WriteLine("4. Why not reuse password?");
                Console.WriteLine("5. Password manager benefits.");
                Console.WriteLine("6. Back to main menu.");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Chatbot: ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                UIHelper.TypeText($"Choose an option {name}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"\n{name}: ");
                Console.ResetColor();
                string option = Console.ReadLine();

                if (!int.TryParse(option, out int choice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    UIHelper.TypeText("Invalid input.");
                    Console.ResetColor();

                    continue;
                }

                
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Chatbot: ");
                Console.ResetColor();
                
                UIHelper.ShowTyping();

                Console.ForegroundColor = ConsoleColor.Green;
                switch (choice)
                {
                    case 1:
                        UIHelper.TypeText("A strong password is long, random, and unique.");
                        UIHelper.TypeText("It should be at least 8–12 characters long.");
                        UIHelper.TypeText("It must include uppercase letters, lowercase letters, numbers, and special characters.");
                        UIHelper.TypeText("Example: P@ssw0rd!2012");
                        break;

                    case 2:
                        UIHelper.TypeText("For a strong password use a mix of letters, numbers and symbols.");
                        UIHelper.TypeText("Combine random words and symbols.");
                        UIHelper.TypeText("Example: Blue!Tiger#91");
                        UIHelper.TypeText("The more random it is, the harder it is to guess.");
                        break;

                    case 3:
                        UIHelper.TypeText("Common mistakes include using names, birthdays, or simple patterns.");
                        UIHelper.TypeText("Passwords like 123456 or password are very easy to hack.");
                        UIHelper.TypeText("Avoid saving passwords in plain text."); 
                        break;

                    case 4:
                        UIHelper.TypeText("Reusing passwords is dangerous.");
                        UIHelper.TypeText("If one account is hacked, all your accounts become vulnerable.");
                        UIHelper.TypeText("Always use different passwords for different platforms.");
                        break;

                    case 5:
                        UIHelper.TypeText("Password managers help you store and generate strong passwords.");
                        UIHelper.TypeText("They encrypt your passwords and keep them secure.");
                        UIHelper.TypeText("You only need to remember one master password.");
                        break;

                    case 6:
                        back = true;
                        break;

                    default:
                        UIHelper.TypeText("Invalid option.");
                        break;
                }
                Console.ResetColor();
            }
        }

        //Phishing menu
        public void PhishingMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\n********************* PHISHING AWARENESS *********************");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("1. What is phishing?");
                Console.WriteLine("2. Types of phishing");
                Console.WriteLine("3. Signs of phishing");
                Console.WriteLine("4. Email phishing example");
                Console.WriteLine("5. How to protect yourself");
                Console.WriteLine("6. Back");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Chatbot: ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                UIHelper.TypeText($"Choose an option {name}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"\n{name}: ");
                Console.ResetColor();
                string option = Console.ReadLine();
                
                if (!int.TryParse(option, out int choice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    UIHelper.TypeText("Invalid input.");
                    Console.ResetColor();
                    continue;
                }


                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Chatbot: ");
                Console.ResetColor();
                UIHelper.ShowTyping();

                Console.ForegroundColor = ConsoleColor.Green;
                switch (choice)
                {
                    case 1:
                        UIHelper.TypeText("Phishing is a cyber attack used to steal personal information.");
                        UIHelper.TypeText("Attackers pretend to be trusted companies or people.");
                        UIHelper.TypeText("They trick users into clicking links or entering passwords.");
                        break;

                    case 2:
                        UIHelper.TypeText("Types of phishing include:");
                        UIHelper.TypeText("- Email phishing");
                        UIHelper.TypeText("- SMS phishing (Smishing)");
                        UIHelper.TypeText("- Spear phishing (targeted attacks)");
                        break;

                    case 3:
                        UIHelper.TypeText("Signs of phishing include:");
                        UIHelper.TypeText("- Urgent or threatening messages");
                        UIHelper.TypeText("- Poor spelling and grammar");
                        UIHelper.TypeText("- Suspicious links");
                        break;

                    case 4:
                        UIHelper.TypeText("Example: 'Your bank account is locked. Click here to verify.'");
                        UIHelper.TypeText("These messages create panic to trick users.");
                        UIHelper.TypeText("Always verify before clicking.");
                        break;

                    case 5:
                        UIHelper.TypeText("To protect yourself:");
                        UIHelper.TypeText("- Do not click unknown links");
                        UIHelper.TypeText("- Check email addresses carefully");
                        UIHelper.TypeText("- Use security software");
                        break;

                    case 6:
                        back = true;
                        break;
                }
                Console.ResetColor();
            }
        }

        //Computer viruses
        public void ComputerVirusMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\n********************* COMPUTER VIRUSES *********************");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("1. What is a virus?");
                Console.WriteLine("2. How viruses spread");
                Console.WriteLine("3. Signs of infection");
                Console.WriteLine("4. Types of malware");
                Console.WriteLine("5. Prevention methods");
                Console.WriteLine("6. Back");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Chatbot: ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                UIHelper.TypeText($"Choose an option {name}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"\n{name}: ");
                Console.ResetColor();
                string option = Console.ReadLine();


                if (!int.TryParse(option, out int choice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    UIHelper.TypeText("Invalid input.");
                    Console.ResetColor();
                    continue;
                }

                
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Chatbot: ");
                Console.ResetColor();
                UIHelper.ShowTyping();

                Console.ForegroundColor = ConsoleColor.Green;
                switch (choice)
                {
                    case 1:
                        UIHelper.TypeText("A computer virus is a malicious program that spreads between systems.");
                        UIHelper.TypeText("It can damage files, steal data, or slow down your computer.");
                        break;

                    case 2:
                        UIHelper.TypeText("Viruses spread through:");
                        UIHelper.TypeText("- Email attachments");
                        UIHelper.TypeText("- Infected downloads");
                        UIHelper.TypeText("- USB devices");
                        break;

                    case 3:
                        UIHelper.TypeText("Signs of infection include:");
                        UIHelper.TypeText("- Slow performance");
                        UIHelper.TypeText("- Frequent crashes");
                        UIHelper.TypeText("- Pop-up ads");
                        break;

                    case 4:
                        UIHelper.TypeText("Types of malware include:");
                        UIHelper.TypeText("- Ransomware");
                        UIHelper.TypeText("- Spyware");
                        UIHelper.TypeText("- Trojans");
                        break;

                    case 5:
                        UIHelper.TypeText("To prevent viruses:");
                        UIHelper.TypeText("- Install antivirus software");
                        UIHelper.TypeText("- Avoid suspicious downloads");
                        UIHelper.TypeText("- Keep your system updated");
                        break;

                    case 6:
                        back = true;
                        break;
                }
                Console.ResetColor();
            }

        }

        //Safe browsing
        public void SafeBrowsingMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\n********************* SAFE BROWSING *********************");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("1. What is safe browsing?");
                Console.WriteLine("2. HTTPS importance");
                Console.WriteLine("3. Avoiding unsafe sites");
                Console.WriteLine("4. Download safety");
                Console.WriteLine("5. Public Wi-Fi risks");
                Console.WriteLine("6. Back");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Chatbot: ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                UIHelper.TypeText($"Choose an option {name}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"\n{name}: ");
                Console.ResetColor();
                string option = Console.ReadLine();

                if (!int.TryParse(option, out int choice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    UIHelper.TypeText("Invalid input.");
                    Console.ResetColor();
                    continue;
                }

                
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Chatbot: ");
                Console.ResetColor();
                UIHelper.ShowTyping();

                Console.ForegroundColor = ConsoleColor.Green;
                switch (choice)
                {
                    case 1:
                        UIHelper.TypeText("Safe browsing means using the internet securely.");
                        UIHelper.TypeText("It involves avoiding harmful websites and protecting your data.");
                        break;

                    case 2:
                        UIHelper.TypeText("HTTPS encrypts your data and protects it.");
                        UIHelper.TypeText("Always check for a padlock icon in the browser.");
                        break;

                    case 3:
                        UIHelper.TypeText("Avoid unsafe websites with too many ads or pop-ups.");
                        UIHelper.TypeText("Do not click on unknown links.");
                        break;

                    case 4:
                        UIHelper.TypeText("Avoid unsafe websites with too many ads or pop-ups.");
                        UIHelper.TypeText("Do not click on unknown links.");
                        break;

                    case 5:
                        UIHelper.TypeText("Public Wi-Fi is risky.");
                        UIHelper.TypeText("Hackers can intercept your data.");
                        UIHelper.TypeText("Avoid logging into important accounts on public networks.");
                        break;

                    case 6:
                        back = true;
                        break;
                }
                Console.ResetColor();
            }
            
        }
    }
    
}
