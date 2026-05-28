using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot
{
    public class Chatbot
    {
        private Dictionary<string, string> userData;
        private string currentTopic;
        private Random random;
        private Dictionary<string, List<string>> keywordResponses;
        private List<string> worriedKeywords;
        private List<string> positiveKeywords;

        public Chatbot()
        {
            userData = new Dictionary<string, string>();
            random = new Random();
            currentTopic = null;
            InitializeKeywordResponses();
            InitializeSentimentKeywords();
        }

        private void InitializeKeywordResponses()
        {
            keywordResponses = new Dictionary<string, List<string>>();

            // PASSWORD
            keywordResponses["password"] = new List<string>
            {
                "Use strong passwords with at least 12 characters, mixing uppercase, lowercase, numbers, and symbols.",
                "Never reuse passwords across different accounts. Use a password manager.",
                "Enable two-factor authentication (2FA) whenever possible.",
                "Avoid using personal information like birthdays in your passwords."
            };

            // SCAM
            keywordResponses["scam"] = new List<string>
            {
                "Be cautious of unsolicited emails asking for personal information.",
                "Verify sender email addresses carefully - scammers use similar-looking addresses.",
                "Never click suspicious links. Hover first to see the actual URL.",
                "If an offer seems too good to be true, it probably is a scam."
            };

            // PHISHING
            keywordResponses["phishing"] = new List<string>
            {
                "Be cautious of emails asking for personal information.",
                "Check for spelling errors - legitimate companies proofread.",
                "Look for the padlock symbol (🔒) in the browser address bar.",
                "When in doubt, contact the company directly using their official website."
            };

            // PRIVACY
            keywordResponses["privacy"] = new List<string>
            {
                "Review privacy settings on social media accounts regularly.",
                "Use a VPN when connecting to public Wi-Fi networks.",
                "Be careful about what personal information you share online.",
                "Use private browsing mode when you don't want activity tracked."
            };

            // MALWARE
            keywordResponses["malware"] = new List<string>
            {
                "Keep antivirus software updated and run regular scans.",
                "Only download software from official sources.",
                "Be careful with email attachments - scan before opening.",
                "Keep your OS and software up to date with security patches."
            };

            // SAFE BROWSING (from Part 1)
            keywordResponses["safe browsing"] = new List<string>
            {
                "Look for 'https://' and the padlock icon (🔒) before entering personal info.",
                "Avoid clicking links in suspicious emails. Type the website address manually.",
                "Use browser extensions that warn about dangerous websites.",
                "Keep your browser updated to protect against security vulnerabilities."
            };

            // HOW ARE YOU (from Part 1)
            keywordResponses["how are you"] = new List<string>
            {
                "I'm just a bot, but my systems are running at 100% efficiency! 🔒",
                "All systems operational! Ready to help you stay cyber-safe.",
                "Functioning perfectly! What cybersecurity topic can I help you with?"
            };
        }

        private void InitializeSentimentKeywords()
        {
            worriedKeywords = new List<string> { "worried", "scared", "afraid", "concerned", "anxious", "fear", "stress", "unsafe", "danger" };
            positiveKeywords = new List<string> { "great", "good", "excellent", "happy", "confident", "safe", "secure", "thank" };
        }

        public string ProcessMessage(string userInput, out string sentiment)
        {
            userInput = userInput.ToLower().Trim();
            sentiment = DetectSentiment(userInput);

            if (IsFollowUpRequest(userInput))
                return HandleFollowUp();

            StoreUserInfo(userInput);
            string response = FindKeywordResponse(userInput);

            return response ?? "I'm not sure I understand. Try asking about passwords, scams, phishing, privacy, malware, or safe browsing.";
        }

        private string DetectSentiment(string userInput)
        {
            foreach (var word in worriedKeywords)
                if (userInput.Contains(word)) return "worried";
            foreach (var word in positiveKeywords)
                if (userInput.Contains(word)) return "positive";
            return "neutral";
        }

        private bool IsFollowUpRequest(string userInput)
        {
            string[] phrases = { "another", "more", "again", "continue", "tell me more", "explain more", "give me another" };
            return phrases.Any(p => userInput.Contains(p));
        }

        private string HandleFollowUp()
        {
            if (currentTopic != null && keywordResponses.ContainsKey(currentTopic))
                return keywordResponses[currentTopic][random.Next(keywordResponses[currentTopic].Count)];
            return "What topic would you like to know more about?";
        }

        private void StoreUserInfo(string userInput)
        {
            if (userInput.Contains("my name is") || userInput.Contains("i'm ") || userInput.Contains("i am "))
            {
                int index = Math.Max(userInput.IndexOf("my name is"), userInput.IndexOf("i'm"));
                if (index >= 0)
                {
                    string name = userInput.Substring(index + (userInput.Contains("my name is") ? 10 : 3)).Trim();
                    userData["name"] = name;
                }
            }
            foreach (var keyword in keywordResponses.Keys)
            {
                if (userInput.Contains(keyword))
                {
                    userData["favoriteTopic"] = keyword;
                    currentTopic = keyword;
                }
            }
        }

        private string FindKeywordResponse(string userInput)
        {
            // Handle "link" as synonym for "safe browsing" (from Part 1)
            if (userInput.Contains("link") && !userInput.Contains("safe browsing"))
            {
                userInput = userInput + " safe browsing";
            }

            foreach (var keyword in keywordResponses.Keys)
            {
                if (userInput.Contains(keyword))
                {
                    currentTopic = keyword;
                    var responses = keywordResponses[keyword];
                    string response = responses[random.Next(responses.Count)];

                    if (userData.ContainsKey("name"))
                        response = $"Hi {userData["name"]}, {response}";

                    string sentiment = DetectSentiment(userInput);
                    if (sentiment == "worried")
                        response = "I understand your concern. " + response + " You're taking the right step!";
                    else if (sentiment == "positive")
                        response = "Great attitude! " + response;

                    return response;
                }
            }
            return null;
        }

        public string GetPersonalizedTip()
        {
            if (userData.ContainsKey("favoriteTopic") && keywordResponses.ContainsKey(userData["favoriteTopic"]))
                return keywordResponses[userData["favoriteTopic"]][random.Next(keywordResponses[userData["favoriteTopic"]].Count)];
            return "Consider learning about password security - it's a great place to start!";
        }
    }
}