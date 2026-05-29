using PROG6221POE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PROG6221POE
{
    public delegate string BotResponseDelegate(string input);

    public class ChatbotEngine
    {
        private string userName;
        private string favouriteTopic = "";
        private string activeTopic = "";
        private string recentMood = "";

        private readonly Random random = new Random();

        private readonly Dictionary<string, string> generalResponses;
        private readonly Dictionary<string, string> phishingResponses;
        private readonly Dictionary<string, string> passwordResponses;
        private readonly Dictionary<string, string> safeBrowsingResponses;

        private readonly Dictionary<string, List<string>> topicTips;
        private readonly Dictionary<string, List<string>> deeperTips;
        private readonly Dictionary<string, List<string>> moodReplies;
        private readonly Dictionary<string, List<string>> keywordMap;

        public ChatbotEngine(string userName)
        {
            this.userName = string.IsNullOrWhiteSpace(userName) ? "Friend" : userName;

            generalResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "how are you", $"I am running smoothly, {this.userName}. Ready to help with online safety." },
                { "what is your purpose", $"My purpose is to teach cybersecurity awareness in a simple, practical way, {this.userName}." },
                { "what can i ask you about", "You can ask about phishing emails, scams, password safety, safe browsing, privacy, suspicious links, public Wi-Fi, and 2FA." },
                { "who created you", "I was created as part of a cybersecurity awareness chatbot project." },
                { "why is cybersecurity important", "Cybersecurity is important because it protects your accounts, identity, money, private data, and devices from online threats." },
                { "hello", $"Hello {this.userName}. What cybersecurity topic would you like help with?" },
                { "help", "Ask me about phishing, passwords, safe browsing, scams, privacy, public Wi-Fi, suspicious links, or two-factor authentication." }
            };

            phishingResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "what is phishing", "Phishing is when attackers pretend to be trusted people, companies, or services to trick you into giving away sensitive information." },
                { "how to spot phishing email", "Look for urgent wording, generic greetings, spelling mistakes, strange sender addresses, and links that do not match official websites." },
                { "what to do if i clicked a phishing link", "Disconnect if necessary, scan your device, change affected passwords, enable 2FA, and monitor your accounts for unusual activity." },
                { "examples of phishing", "Examples include fake banking emails, fake delivery messages, fake prize notifications, fake login pages, and fake support messages." },
                { "what is smishing", "Smishing is phishing through SMS messages, usually using fake alerts, fake delivery links, or account warnings." },
                { "what is vishing", "Vishing is voice phishing, where scammers call pretending to be from banks, companies, or official organisations." },
                { "how to report phishing", "Report phishing to the organisation being impersonated, your bank if money is involved, or the relevant cybercrime reporting channel." },
                { "what are phishing red flags", "Red flags include urgency, threats, poor grammar, mismatched links, unknown attachments, and requests for passwords or OTPs." }
            };

            passwordResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "how to create strong password", "Use a long password with uppercase letters, lowercase letters, numbers, and symbols. Avoid names, birthdays, and obvious words." },
                { "what is two factor authentication", "Two-factor authentication adds a second verification step after your password, such as a code, authenticator app, or security prompt." },
                { "how often to change passwords", "Change passwords immediately after a breach or suspicious activity. For sensitive accounts, update them regularly and keep them unique." },
                { "what is password manager", "A password manager helps generate, store, and organise strong unique passwords securely." },
                { "should i reuse passwords", "No. Reusing passwords is risky because one leaked password can expose many accounts." },
                { "how to remember strong passwords", "Use a password manager or create a long passphrase that is easy for you to remember but hard for others to guess." },
                { "what is multi factor authentication", "Multi-factor authentication uses more than one proof of identity, such as a password, device, fingerprint, or security key." },
                { "common password mistakes", "Common mistakes include using password123, qwerty, birthdays, names, sticky notes, and sharing passwords with others." }
            };

            safeBrowsingResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "how to identify safe websites", "Check for HTTPS, correct domain spelling, a professional layout, and avoid websites filled with suspicious pop-ups." },
                { "what is https", "HTTPS encrypts communication between your browser and the website, making it harder for attackers to intercept your data." },
                { "how to avoid fake websites", "Type URLs manually, use bookmarks for important websites, avoid suspicious adverts, and double-check the domain before logging in." },
                { "what are cookies safe", "Cookies are usually normal website data, but tracking cookies can monitor browsing behaviour. Review browser settings and clear cookies when needed." },
                { "how to browse safely on public wifi", "Avoid sensitive logins on public Wi-Fi. Use a VPN, turn off sharing, and avoid banking or private accounts on unknown networks." },
                { "what is incognito mode", "Incognito mode hides local browsing history on your device, but it does not make you anonymous to websites, networks, or your internet provider." },
                { "how to check if link is safe", "Hover over links, inspect the full URL, avoid shortened links from unknown sources, and use link scanners when unsure." },
                { "what is browser security", "Browser security includes updates, pop-up blocking, anti-tracking settings, safe downloads, and avoiding suspicious extensions." }
            };

            topicTips = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "phishing", new List<string> { phishingResponses["what is phishing"], phishingResponses["how to spot phishing email"], phishingResponses["what are phishing red flags"] } },
                { "password", new List<string> { passwordResponses["how to create strong password"], passwordResponses["what is password manager"], passwordResponses["should i reuse passwords"] } },
                { "safe browsing", new List<string> { safeBrowsingResponses["how to identify safe websites"], safeBrowsingResponses["how to avoid fake websites"], safeBrowsingResponses["how to browse safely on public wifi"] } },
                { "scam", new List<string> { "Scams often use fear, urgency, fake rewards, or fake authority to make people act quickly.", "Never share OTPs, banking details, passwords, or personal documents with strangers online.", "If an offer sounds too good or too urgent, verify it using official sources first." } },
                { "privacy", new List<string> { "Privacy means controlling what personal information you share and who can access it.", "Review app permissions, social media visibility, and location-sharing settings regularly.", "Avoid posting your ID number, address, live location, or daily routine publicly." } },
                { "2fa", new List<string> { passwordResponses["what is two factor authentication"], "2FA helps protect your account even if someone discovers your password.", "Authenticator apps are generally safer than SMS codes because SMS can be affected by SIM-swap fraud." } }
            };

            deeperTips = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "phishing", new List<string> { "A strong phishing defence is to pause before reacting. Scammers rely on panic, urgency, and careless clicking.", "If an email claims your account will close, visit the official website manually instead of using the link inside the email.", "If you entered details on a suspicious page, change the password immediately, enable 2FA, and monitor the account." } },
                { "password", new List<string> { "A passphrase can be stronger than a short complex password because length makes it harder to crack.", "Protect your email password carefully because many accounts can be reset through your email.", "Do not save passwords in screenshots, notes apps, or chats because those can be exposed if your device or account is compromised." } },
                { "safe browsing", new List<string> { "Avoid cracked software, unknown APK files, and random browser extensions because they commonly carry malware.", "Public Wi-Fi is risky for banking or sensitive logins unless extra protection is used.", "Browser updates matter because they often patch security weaknesses attackers could exploit." } },
                { "scam", new List<string> { "Common scams include fake jobs, fake investments, romance scams, courier scams, prize scams, and fake account verification messages.", "Treat OTPs like passwords. If someone asks for your OTP, assume they are trying to access your account.", "Scammers often copy company logos and wording, so always verify through official channels." } },
                { "privacy", new List<string> { "Privacy is not about hiding everything. It is about choosing what information belongs online and what should stay private.", "Small details like birthdays, schools, pet names, and routines can be used to guess security questions.", "Check app permissions for camera, microphone, contacts, files, and location access." } },
                { "2fa", new List<string> { "2FA is strongest when your email account is protected too, because email controls many password resets.", "Backup codes should be stored safely in case you lose access to your phone.", "SMS 2FA is better than no 2FA, but authenticator apps are usually stronger." } }
            };

            moodReplies = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "worried", new List<string> { "That worry makes sense. Online threats can feel heavy, but practical habits reduce most risk.", "Take it step by step: strong passwords, 2FA, and careful link checking already improve safety a lot." } },
                { "frustrated", new List<string> { "I get it. Security can feel like extra admin, but it is easier than recovering a stolen account.", "Let us keep it simple and focus on one useful safety action at a time." } },
                { "curious", new List<string> { "That curiosity is useful because attackers rely on people not understanding their tricks.", "Good mindset. The more patterns you recognise, the harder you are to fool." } },
                { "confused", new List<string> { "No problem. Cybersecurity terms can be annoying at first, but examples make them easier.", "Tell me what part is confusing and I will break it down more simply." } }
            };

            keywordMap = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "phishing", new List<string> { "phishing", "fake email", "suspicious email", "smishing", "vishing", "email scam" } },
                { "password", new List<string> { "password", "passwords", "passcode", "credentials", "login details", "strong password" } },
                { "safe browsing", new List<string> { "safe browsing", "browser", "website", "https", "link", "download", "public wifi", "public wi-fi" } },
                { "privacy", new List<string> { "privacy", "private data", "personal info", "personal information", "permissions", "tracking" } },
                { "scam", new List<string> { "scam", "scams", "fraud", "otp", "fake offer", "banking details" } },
                { "2fa", new List<string> { "2fa", "two factor", "two-factor", "mfa", "authenticator", "verification code" } }
            };
        }

        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Please enter a message first.";
            }

            input = input.ToLower().Trim();

            BotResponseDelegate moodDetector = DetectMood;
            string moodReply = moodDetector(input);

            string memoryReply = HandleMemory(input);
            if (!string.IsNullOrWhiteSpace(memoryReply))
            {
                return memoryReply;
            }

            if (IsFollowUp(input))
            {
                return ContinueCurrentTopic();
            }

            string partOneReply = DetectPartOneReply(input);
            string topicReply = DetectTopic(input);

            if (!string.IsNullOrWhiteSpace(moodReply) &&
                !string.IsNullOrWhiteSpace(topicReply))
            {
                return moodReply + Environment.NewLine + Environment.NewLine + topicReply;
            }

            if (!string.IsNullOrWhiteSpace(partOneReply)) return partOneReply;
            if (!string.IsNullOrWhiteSpace(moodReply)) return moodReply;
            if (!string.IsNullOrWhiteSpace(topicReply)) return topicReply;

            if (!string.IsNullOrWhiteSpace(favouriteTopic))
            {
                return "I did not fully catch that, but I remember you are interested in "
                       + favouriteTopic + ". Ask me for another tip on that topic.";
            }

            return "I could not understand that clearly. Try asking about phishing, passwords, scams, privacy, safe browsing, or 2FA.";
        }

        private string DetectPartOneReply(string input)
        {
            string reply;

            reply = MatchDictionary(input, generalResponses);
            if (reply != null) return reply;

            reply = MatchDictionary(input, phishingResponses);
            if (reply != null) { activeTopic = "phishing"; return reply; }

            reply = MatchDictionary(input, passwordResponses);
            if (reply != null) { activeTopic = "password"; return reply; }

            reply = MatchDictionary(input, safeBrowsingResponses);
            if (reply != null) { activeTopic = "safe browsing"; return reply; }

            return "";
        }

        private string DetectTopic(string input)
        {
            string topic = FindTopic(input);

            if (string.IsNullOrWhiteSpace(topic))
            {
                return "";
            }

            activeTopic = topic;

            if (input.Contains("explain") ||
                input.Contains("details") ||
                input.Contains("deep") ||
                input.Contains("more about"))
            {
                return GetRandomItem(deeperTips[topic]);
            }

            return GetRandomItem(topicTips[topic]);
        }

        private string DetectMood(string input)
        {
            foreach (string mood in moodReplies.Keys)
            {
                if (input.Contains(mood))
                {
                    recentMood = mood;
                    return GetRandomItem(moodReplies[mood]);
                }
            }

            return "";
        }

        private string HandleMemory(string input)
        {
            if (input.Contains("my name is"))
            {
                userName = input.Replace("my name is", "").Trim();

                if (string.IsNullOrWhiteSpace(userName))
                {
                    userName = "Friend";
                }

                return "Noted. I will remember your name as " + userName + ".";
            }

            if (input.Contains("interested in") ||
                input.Contains("i like") ||
                input.Contains("i care about"))
            {
                string topic = FindTopic(input);

                if (!string.IsNullOrWhiteSpace(topic))
                {
                    favouriteTopic = topic;
                    activeTopic = topic;

                    return "Got it, " + userName + ". I will remember that you are interested in "
                           + favouriteTopic + ".";
                }
            }

            if (input.Contains("remember") ||
                input.Contains("what do you know about me"))
            {
                if (!string.IsNullOrWhiteSpace(favouriteTopic) &&
                    !string.IsNullOrWhiteSpace(recentMood))
                {
                    return "I remember your name is " + userName +
                           ", you are interested in " + favouriteTopic +
                           ", and you recently sounded " + recentMood + ".";
                }

                if (!string.IsNullOrWhiteSpace(favouriteTopic))
                {
                    return "I remember your name is " + userName +
                           " and your main interest is " + favouriteTopic + ".";
                }

                return "I remember your name is " + userName + ".";
            }

            return "";
        }

        private bool IsFollowUp(string input)
        {
            return input.Contains("tell me more")
                   || input.Contains("another tip")
                   || input.Contains("explain more")
                   || input.Contains("go deeper")
                   || input.Contains("more detail")
                   || input.Contains("continue");
        }

        private string ContinueCurrentTopic()
        {
            if (string.IsNullOrWhiteSpace(activeTopic))
            {
                return "Choose a topic first: phishing, passwords, scams, privacy, safe browsing, or 2FA.";
            }

            if (deeperTips.ContainsKey(activeTopic))
            {
                return GetRandomItem(deeperTips[activeTopic]);
            }

            return "Ask me about phishing, passwords, scams, privacy, safe browsing, or 2FA.";
        }

        private string FindTopic(string input)
        {
            foreach (var topicGroup in keywordMap)
            {
                foreach (string keyword in topicGroup.Value)
                {
                    if (input.Contains(keyword))
                    {
                        return topicGroup.Key;
                    }
                }
            }

            return "";
        }

        private string MatchDictionary(string input, Dictionary<string, string> responses)
        {
            if (responses.ContainsKey(input))
            {
                return responses[input];
            }

            foreach (string key in responses.Keys)
            {
                if (input.Contains(key))
                {
                    return responses[key];
                }
            }

            return null;
        }

        private string GetRandomItem(List<string> items)
        {
            int index = random.Next(items.Count);
            return items[index];
        }

        public string GetAsciiArt()
        {
            return AsciiArt.GetArt();
        }
    }
}