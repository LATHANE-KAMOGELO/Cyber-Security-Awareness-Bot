using System;
using System.IO;
using System.Media;

namespace CyberSecurityAwarenessBot
{
    public static class VoiceGreeting
    {
        public static void PlayGreeting()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

                if (File.Exists(path))
                {
                    SoundPlayer player = new SoundPlayer(path);
                    player.PlaySync();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Voice greeting file not found.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error playing voice greeting: " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}