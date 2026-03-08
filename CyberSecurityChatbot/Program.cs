// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;
using NAudio.Wave;
internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            using (var audioFile = new AudioFileReader("greeting.wav"))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                // Wait until playback finishes
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error playing audio: " + ex.Message);
        }
        //ASCII logo    
        Console.ForegroundColor = ConsoleColor.Cyan;//sets color for the ASCII
        Console.WriteLine("****************************************");
        Console.WriteLine("  ______   ______     ____   _______    ");
        Console.WriteLine(" /  ____| |  __  \\   / __ \\ |__   __| ");
        Console.WriteLine("|  /      | |__) /  | /  \\ |   | |     ");
        Console.WriteLine("| |       |  ---    | |  | |   | |");
        Console.WriteLine("| |       | |  \\ \\  | |  | |   | |  ");
        Console.WriteLine("|  \\____  | |__/  | | \\__/ |   | |     ");
        Console.WriteLine(" \\______| |______/   \\____/    |_|     ");
        Console.WriteLine("\n  CYBERSECURITY AWARENESS CHATBOT     ");
        Console.WriteLine("****************************************");
        Console.ResetColor();

        //ask for user's name
        Console.ForegroundColor = ConsoleColor.Green;
        TypeText("\nChatbot: What's your name? ");
        Console.ResetColor();

        Console.Write("You: ");
        string name = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Green;
        TypeText($"\nChatbot: Hello, {name}! Welcome to the Cybersecurity Awareness Chatbot.");
        Console.ResetColor();

        bool running = true;

        //while loop to keep the chatbot running until the user chooses the exit option
        while (running)
        {
            //menu options
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n********** Menu **********");
            Console.WriteLine("1. What is cybersecurity?\n2. Password Safety\n3. Phishing Awareness\n4. Computer Viruses\n5. Safe Browsing\n6. Exit");
            Console.ResetColor();

            int choice;

            //Input validation loop
            while (true) 
            {
                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 6)
                {
                    break;//valid input, exit loop
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nChatbot: ");
            Console.ResetColor();

            switch (choice) 
            {
                case 1: 
                    TypeText("Cybersecurity is the practice of protecting computers, servers, mobile devices, electronic systems, networks,\n and data from malicious attacks.");
                    TypeText("It involves implementing security measures to prevent unauthorized access and ensure the confidentiality, integrity, and availability of information.");
                    break;
                case 2:
                    TypeText("Password safety is crucial for protecting your online accounts. ");
                    TypeText("Use strong, a mix of letters, numbers and symbols.");
                    TypeText("Don't reuse passwords across multiple sites.");
                    TypeText("Avoid using easily guessable information like birthdays or pet names.");
                    break;
                case 3:
                    TypeText("Phishing is a type of cyber attack where attackers impersonate legitimate organizations to trick individuals into providing sensitive information such as passwords, credit card numbers, or personal details.");
                    TypeText("Be cautious of unsolicited emails or messages asking for personal information.");
                    TypeText("Verify the sender's email address and look for signs of phishing, such as poor grammar or urgent requests.");
                    break;
                case 4:
                    TypeText("Computer viruses are malicious software programs that can infect your computer and cause harm.");
                    TypeText("They can spread through email attachments, infected websites, or by downloading files from untrusted sources.");
                    break;
                case 5: 
                    TypeText("Safe browsing practices include avoiding suspicious websites, not clicking on unknown links, and keeping your web browser and security software up to date.");
                    TypeText("Be cautious when downloading files or software from the internet, and always verify the source before proceeding.");
                    TypeText("Use secure connections (HTTPS) when entering sensitive information online.");
                    TypeText("Regularly clear your browser cache and cookies to protect your privacy.");
                    break;
                case 6: 
                    TypeText($"Goodbye, {name}! Stay safe online!");
                    running = false;
                    break;
                default:
                    TypeText("Invalid option. Please select a number from the menu.");
                    break;

            }


        }
    }
        //Typewriter effect
        static void TypeText(String msg, int delay = 30)
    {
               foreach (char c in msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(c);
            Thread.Sleep(delay);
            Console.ResetColor();

        }
        Console.WriteLine();

    }
}