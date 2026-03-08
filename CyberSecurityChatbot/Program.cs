// See https://aka.ms/new-console-template for more information
using System;
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

        
    }
}