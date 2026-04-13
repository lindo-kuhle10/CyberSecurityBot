using System;

namespace CyberSecurityBot
{
    public class ChatBot
    {
        private string userName;

        // This method asks for the user's name
        public void StartConversation()
        {
            UserInterface.PrintHeader("USER REGISTRATION");
            Console.Write("Please enter your name: ");
            userName = Console.ReadLine();

            // Input Validation: If they don't type a name, call them "Guest"
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "Guest";
                Console.WriteLine("No name provided? I'll call you Guest.");
            }

            // Use the TypeWriter effect for the welcome message
            UserInterface.TypeWriter($"\nWelcome, {userName}! I am your Cybersecurity Awareness Assistant.");
        }

        // This is the main loop that keeps the chat running
        public void Run()
        {
            while (true)
            {
                UserInterface.PrintHeader("HOW CAN I HELP?");
                Console.WriteLine("Type 'quit' to exit, or 'help' to see topics.");
                Console.Write("You: ");
                string input = Console.ReadLine();

                // Input Validation: Check if input is empty
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("I didn't hear you. Please type a message.");
                    Console.ResetColor();
                    continue; // Restarts the loop
                }

                // Check if user wants to quit
                if (input.ToLower() == "quit")
                {
                    UserInterface.TypeWriter("Staying safe! Goodbye for now.");
                    break; // Exits the loop
                }

                ProcessInput(input.ToLower());
            }
        }

        // This method decides what the bot says based on keywords
        private void ProcessInput(string input)
        {
            if (input.Contains("help") || input.Contains("topics"))
            {
                Console.WriteLine("\nI can help you with:");
                Console.WriteLine(" - Password Safety");
                Console.WriteLine(" - Phishing Scams");
                Console.WriteLine(" - Safe Browsing");
            }
            else if (input.Contains("password"))
            {
                UserInterface.TypeWriter("Passwords should be at least 12 characters long and include symbols, numbers, and letters. Never share them!");
            }
            else if (input.Contains("phishing"))
            {
                UserInterface.TypeWriter("Phishing is when scammers pretend to be a company to steal your data. Always check the sender's email address.");
            }
            else if (input.Contains("safe browsing") || input.Contains("link"))
            {
                UserInterface.TypeWriter("Look for 'https://' and the padlock icon in your browser before entering personal info.");
            }
            else if (input.Contains("how are you"))
            {
                UserInterface.TypeWriter("I'm just a bot, but my systems are running at 100% efficiency!");
            }
            else
            {
                // Default response if the bot doesn't understand
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("I'm not sure I understand. Try asking about 'passwords', 'phishing', or type 'help'.");
                Console.ResetColor();
            }
        }
    }
}