using System;
using System.Buffers;
using CyberSecurityBot.Core;
using CybersecurityChatbot;

namespace CyberSecurityBot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize shared components
            var sentimentAnalyzer = new SentimentAnalyzer();
            var memoryManager = new MemoryManager();
            var responseManager = new ResponseManager();
            var chatBot = new ChatBot(responseManager, sentimentAnalyzer, memoryManager);

            // Display welcome
            UserInterface.DisplayAsciiArt();
            AudioPlayer.PlayGreeting();

            Console.WriteLine("\nWelcome to the Cybersecurity Awareness Bot!\n");
            Console.Write("What's your name? ");
            string userName = Console.ReadLine();
            memoryManager.Store("UserName", userName);

            Console.WriteLine($"\nNice to meet you, {userName}! I'm here to help you stay safe online.\n");
            Console.WriteLine("Type 'help' to see available topics, or 'quit' to exit.\n");

            string lastTopic = "";

            while (true)
            {
                Console.Write("You: ");
                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput)) continue;

                if (userInput.ToLower() == "quit")
                {
                    Console.WriteLine("\nGoodbye! Stay safe online! 🔐");
                    break;
                }

                // Detect sentiment
                string sentiment = sentimentAnalyzer.DetectSentiment(userInput);

                // Process input
                string response = chatBot.ProcessInput(userInput, sentiment, out lastTopic);

                // Add empathetic response if needed
                if (sentiment != "neutral")
                {
                    string empatheticResponse = sentimentAnalyzer.GetEmpatheticResponse(sentiment);
                    Console.WriteLine($"\nBot: {empatheticResponse}");
                    Console.WriteLine($"      {response}\n");
                }
                else
                {
                    Console.WriteLine($"\nBot: {response}\n");
                }
            }
        }
    }
}