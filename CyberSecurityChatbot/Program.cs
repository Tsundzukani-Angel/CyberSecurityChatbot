// See https://aka.ms/new-console-template for more information
using CyberSecurityChatbot;
using System;
using System.Threading;
internal class Program
{
    public static void Main(string[] args)
    {
        GreetingVoiceSpeech.PlayGreeting();
        UIHelper.DisplayLogo();

        Chatbot bot = new Chatbot();
        bot.Start();


    }
}