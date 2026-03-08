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
        TypeText("I can help you stay safe online.");
        TypeText("Type 'exit' anytime to close the chatbot.\n");
        Console.ResetColor();

       //while loop to keep the chatbot running until the user types exit
        while (true)
        {

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"\n{name}: ");
            Console.ResetColor();

            string userInput = Console.ReadLine();

            //user validation
            if (string.IsNullOrWhiteSpace(userInput))
            {
                TypeText("Please type something so i can help you.");
                continue;
            }
            userInput = userInput.ToLower();

            if (userInput == "exit")
            {
                TypeText($"Stay safe online! Goodbye {name}");
                break;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Chatbot: ");
            Console.ResetColor();

            if (userInput.Contains("cybersecurity")) 
            {
                TypeText("Cybersecurity is the practice of protecting computers, servers, mobile devices, electronic systems, networks,\n and data from malicious attacks.");
                TypeText("It involves implementing security measures to prevent unauthorized access and ensure the confidentiality, integrity, and availability of information.");
            }
            else if(userInput.Contains("password"))
            {
                TypeText("Password safety is crucial for protecting your online accounts. ");
                TypeText("For a strong password use a mix of letters, numbers and symbols.");
                TypeText("Don't reuse passwords across multiple sites.");
                TypeText("Avoid using easily guessable information like birthdays or pet names.");
            }
            else if(userInput.Contains("phishing"))
            {
                TypeText("Phishing is a type of cyber attack where attackers impersonate legitimate organizations to trick individuals into providing sensitive information such as passwords, credit card numbers, or personal details.");
                TypeText("Be cautious of unsolicited emails or messages asking for personal information.");
                TypeText("Verify the sender's email address and look for signs of phishing, such as poor grammar or urgent requests.");
            }
            else if(userInput.Contains("virus"))
            {
                TypeText("Computer viruses are malicious software programs that can infect your computer and cause harm.");
                TypeText("They can spread through email attachments, infected websites, or by downloading files from untrusted sources.");
            }
            else if(userInput.Contains("browsing"))
            {
                TypeText("Safe browsing practices include avoiding suspicious websites, not clicking on unknown links, and keeping your web browser and security software up to date.");
                TypeText("Be cautious when downloading files or software from the internet, and always verify the source before proceeding.");
                TypeText("Use secure connections (HTTPS) when entering sensitive information online.");
                TypeText("Regularly clear your browser cache and cookies to protect your privacy.");
            }
            else
            {
                TypeText("I'm not sure about the topic yet.");
                TypeText("Try asking about passwords, phishing, viruses, or safe browsing");
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