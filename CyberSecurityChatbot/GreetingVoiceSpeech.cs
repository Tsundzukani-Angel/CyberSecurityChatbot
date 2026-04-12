// See https://aka.ms/new-console-template for more information
using System;
using System.Media;

namespace CyberSecurityChatbot
{
    internal class GreetingVoiceSpeech
    {
        public static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("greeting.wav");
                player.PlaySync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error playing greeting audio: " + ex.Message);
            }
        }
    }
}
