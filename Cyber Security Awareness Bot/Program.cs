using CyberSecurityAwarenessBot;
using System;

namespace CyberSecurityAwarenessBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";

            AsciiArt.ShowCentered();
            VoiceGreeting.PlayGreeting();

            ChatBot bot = new ChatBot();
            bot.StartChat();
        }
    }
}