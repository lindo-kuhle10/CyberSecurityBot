using System;
using System.Threading; // Required for the typing effect delay

namespace CyberSecurityBot
{
    public static class UserInterface
    {
        // This method creates a nice header with borders
        public static void PrintHeader(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine($"   {text}");
            Console.WriteLine(new string('=', 50));
            Console.ResetColor();
        }

        // This displays the ASCII Art logo
        public static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            // This is the ASCII art logo
            Console.WriteLine(@"
      ____  _           _  __ _   _                 
     / ___|| |__   __ _| |/ _| | | |_   _ _ __  ___ 
    | |    | '_ \ / _` | | |_| | | | | | | '_ \/ __|
    | |___ | | | | (_| | |  _| |_| | |_| | | | \__ \
     \____||_| |_|\__,_|_|_|  \___/ \__,_|_| |_|___/
            ");
            Console.ResetColor();
        }

        // This method simulates a typewriter effect
        public static void TypeWriter(string text, int delayMs = 30)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delayMs); // Pauses briefly between letters
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}