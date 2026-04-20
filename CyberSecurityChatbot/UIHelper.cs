using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatbot
{
    internal class UIHelper
    {
        public static void DisplayLogo()
        {
            //ASCII logo    
            //Used ChatGpt
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║            CYBER BOT SECURITY SYSTEM         ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;//sets font color

            Console.WriteLine(@"
       ██████╗ ██████╗  ██████╗ ████████╗
      ██╔════╝ ██╔══██╗██╔═══██╗╚══██╔══╝
      ██║      ██████╔╝██║   ██║   ██║   
      ██║      ██╔══██╗██║   ██║   ██║   
      ╚██████╗ ██████╔╝╚██████╔╝   ██║   
       ╚═════╝ ╚═════╝  ╚═════╝    ╚═╝   
");

            Console.WriteLine("       CYBERSECURITY AWARENESS CHATBOT      ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n══════════════════════════════════════════════");
            Console.ResetColor();
        }
        //Typewriter effect
        public static void TypeText(String msg, int delay = 30)
        {
            foreach (char c in msg)
            { 
                Console.Write(c);
                Thread.Sleep(delay);

            }
            
            Console.WriteLine();

        }
        //delay feature
        public static void ShowTyping(int delay = 1000)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(300);
                Console.Write(".");
            }

            Thread.Sleep(delay);
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
