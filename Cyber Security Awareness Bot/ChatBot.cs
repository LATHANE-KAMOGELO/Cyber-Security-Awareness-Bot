using System;
using System.Threading;

namespace CyberSecurityAwarenessBot
{
    public class ChatBot
    {
        public string UserName { get; set; }

        private const int ChatStartLine = 12;

        public void StartChat()
        {
            Console.Clear();
            ShowHeader();

            Console.SetCursorPosition(0, ChatStartLine);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Please enter your name: ");

            UserName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(UserName))
            {
                Console.Write("Name cannot be empty. Enter your name: ");
                UserName = Console.ReadLine();
            }

            Console.WriteLine();

            TypeEffect($"Hello {UserName.ToUpper()}! Welcome to the Cybersecurity Awareness Bot.\n");
            TypeEffect("You can ask me about passwords, phishing, safe browsing, malware, scams, or cybersecurity.\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Type 'exit', 'quit', or 'bye' anytime to close the program.\n");

            ChatLoop();
        }

        private void ShowHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, 0);

            Console.WriteLine("+--------------------------------------------------------------------------+");
            Console.WriteLine("|                                                                          |");
            Console.WriteLine("|  _____       _                 _____                      _ _            |");
            Console.WriteLine("| /  __ \\     | |               /  ___|                    (_) |           |");
            Console.WriteLine("| | /  \\/_   _| |__   ___ _ __  \\ `--.  ___  ___ _   _ _ __ _| |_ _   _    |");
            Console.WriteLine("| | |   | | | | '_ \\ / _ \\ '__|  `--. \\/ _ \\/ __| | | | '__| | __| | | |   |");
            Console.WriteLine("| | \\__/\\ |_| | |_) |  __/ |    /\\__/ /  __/ (__| |_| | |  | | |_| |_| |   |");
            Console.WriteLine("|  \\____/\\__, |_.__/ \\___|_|    \\____/ \\___|\\___|\\__,_|_|  |_|\\__|\\__, |   |");
            Console.WriteLine("|         __/ |                                                     __/ |   |");
            Console.WriteLine("|        |___/                                                     |___/    |");
            Console.WriteLine("|                                                                          |");
            Console.WriteLine("+--------------------------------------------------------------------------+");

            Console.ResetColor();
        }

        private void ChatLoop()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\n{UserName.ToUpper()}: ");

                string input = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("I didn't quite understand that. Could you rephrase?");
                    continue;
                }

                // Exit words anywhere in the sentence
                if (ContainsAny(input, "exit", "quit", "bye"))
                {
                    ExitProgram();
                }

                Respond(input);
            }
        }

        private void Respond(string input)
        {
            if (ContainsAny(input, "how are you", "how are u"))
            {
                TypeEffect("I'm doing great! Thanks for asking. I'm here to help you stay safe online.");
            }
            else if (ContainsAny(input, "purpose", "what do you do", "why are you here"))
            {
                TypeEffect("My purpose is to educate people about cybersecurity awareness and help them stay safe online.");
            }
            else if (ContainsAny(input, "what can i ask", "what can you do", "help"))
            {
                TypeEffect("You can ask me about passwords, phishing, malware, scams, safe browsing, and cybersecurity.");
            }
            else if (ContainsAny(input, "password", "passwords"))
            {
                TypeEffect("Use strong passwords with uppercase letters, lowercase letters, numbers, and symbols. Never reuse passwords on multiple accounts.");
            }
            else if (ContainsAny(input, "phishing", "phish"))
            {
                TypeEffect("Phishing is when attackers trick you into revealing personal information through fake emails, messages, or websites.");
            }
            else if (ContainsAny(input, "safe browsing", "browsing", "browse safely"))
            {
                TypeEffect("Always check for HTTPS, avoid suspicious links, and do not download files from untrusted websites.");
            }
            else if (ContainsAny(input, "malware", "virus", "trojan", "ransomware"))
            {
                TypeEffect("Malware is harmful software designed to damage devices, steal data, or spy on users.");
            }
            else if (ContainsAny(input, "scam", "scams", "fraud"))
            {
                TypeEffect("Online scams try to trick people into giving away money, passwords, or personal information.");
            }
            else if (ContainsAny(input, "cybersecurity", "security", "cyber security"))
            {
                TypeEffect("Cybersecurity is the practice of protecting computers, networks, and data from cyber threats.");
            }
            else
            {
                TypeEffect("Try asking about passwords, phishing, malware, scams, safe browsing, or cybersecurity.");
            }
        }

        private bool ContainsAny(string input, params string[] keywords)
        {
            foreach (string keyword in keywords)
            {
                if (input.Contains(keyword))
                {
                    return true;
                }
            }

            return false;
        }

        private void ExitProgram()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nStay safe online! Goodbye.");

            Thread.Sleep(2000);
            Environment.Exit(0);
        }

        private void TypeEffect(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;

            foreach (char character in message)
            {
                Console.Write(character);
                Thread.Sleep(15);
            }

            Console.WriteLine();
        }
    }
}