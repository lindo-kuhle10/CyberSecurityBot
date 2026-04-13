using System;
using System.IO;
using System.Media;

namespace CyberSecurityBot
{
    public static class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                if (File.Exists("greeting.wav"))
                {
                    SoundPlayer player = new SoundPlayer("greeting.wav");
                    player.PlaySync();
                }
                else
                {
                   // Console.WriteLine("Playing greeting audio...\n");
                    //AudioPlayer.PlayGreeting();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }
    }
}