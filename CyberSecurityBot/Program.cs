using System;

namespace CyberSecurityBot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Clear the console screen
            Console.Clear();

            // Display ASCII Art
            UserInterface.DisplayAsciiArt();

            // Play Voice Greeting
            Console.WriteLine("Playing greeting audio...\n");
            AudioPlayer.PlayGreeting();

            // Wait for user to press Enter
            Console.WriteLine("\nPress Enter to start chatting...");
            Console.ReadLine();

            // Start the chatbot
            ChatBot bot = new ChatBot();
            bot.StartConversation();
            bot.Run();
        }
    }
}