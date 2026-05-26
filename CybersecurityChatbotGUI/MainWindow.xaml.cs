using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CybersecurityChatbotGUI
{
    public partial class MainWindow : Window
    {
        private Random random = new Random();

        // --- MEMORY SYSTEM STATES (Requirement 5) ---
        private string userName = "";
        private string lastRecognizedKeyword = "";
        private bool isFirstMessage = true;

        // Generic Dictionary for standard responses
        private Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            {
                "password", new List<string> {
                    "Make sure to use strong, unique passwords with a mix of numbers and symbols. Avoid using personal details!",
                    "Consider using a trusted Password Manager so you don't have to write down complex passwords.",
                    "A good practice is using passphrases—four or more random words combined. They are long but easy to remember!"
                }
            },
            {
                "scam", new List<string> {
                    "Always double-check urgent messages asking for money or personal details. Scammers thrive on creating panic!",
                    "If an offer looks too good to be true, it's highly likely a scam. Never click links from unverified numbers.",
                    "Scammers often impersonate banks or financial organizations. Your bank will never ask for your PIN over SMS."
                }
            },
            {
                "privacy", new List<string> {
                    "Protect your privacy by regularly auditing your social media permission settings and hiding personal contact info.",
                    "Public Wi-Fi networks can expose your browsing data. Use a VPN to protect your privacy when out and about.",
                    "Be careful what you share online! Oversharing answers to common security questions makes a hacker's job easy."
                }
            }
        };

        // --- SENTIMENT DICTIONARY (Requirement 6) ---
        private Dictionary<string, string> sentimentEmpathy = new Dictionary<string, string>()
        {
            { "worried", "Bot: It's completely understandable to feel concerned about this safety risk. Let me help ease your mind. " },
            { "curious", "Bot: I love that you're eager to learn! Staying proactive is the absolute best defense. " },
            { "frustrated", "Bot: I know cybersecurity rules can feel overwhelming or annoying, but staying alert keeps you safe! " }
        };

        public MainWindow()
        {
            InitializeComponent();
            // Start conversational loop by prompting for name immediately
            txtChatHistory.Text = "Bot: Hello! Welcome to your Cybersecurity Assistant. What is your name?\n\n";
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(userInput)) return;

            txtChatHistory.AppendText($"You: {userInput}\n");
            txtUserInput.Clear();

            // Handle greeting phase to lock down user profile memory
            if (isFirstMessage)
            {
                userName = userInput;
                isFirstMessage = false;
                txtChatHistory.AppendText($"Bot: Great to meet you, {userName}! Ask me anything about 'password' safety, online 'scam' threats, or digital 'privacy'.\n\n");
                txtChatHistory.ScrollToEnd();
                return;
            }

            // Normal processing engine conversation path
            string botResponse = GetBotResponse(userInput);
            txtChatHistory.AppendText($"{botResponse}\n\n");
            txtChatHistory.ScrollToEnd();
        }

        private string GetBotResponse(string input)
        {
            string lowerInput = input.ToLower();
            string responsePrefix = "Bot: ";

            // 1. Process Sentiment First (Requirement 6)
            foreach (var emotion in sentimentEmpathy.Keys)
            {
                if (lowerInput.Contains(emotion))
                {
                    responsePrefix = sentimentEmpathy[emotion];
                    break;
                }
            }

            // 2. Process Conversation Flow Continuity (Requirement 4)
            if (lowerInput.Contains("more") || lowerInput.Contains("explain") || lowerInput.Contains("continue"))
            {
                if (!string.IsNullOrEmpty(lastRecognizedKeyword))
                {
                    List<string> options = keywordResponses[lastRecognizedKeyword];
                    return $"{responsePrefix}Since you want to know more, here is another perspective on {lastRecognizedKeyword} safety: {options[random.Next(options.Count)]}";
                }
                return $"Bot: I'd love to explain more! What specific area are you interested in expanding on, {userName}?";
            }

            // 3. Process Standard Keywords (Requirement 2 & 5 Memory Recall)
            foreach (var key in keywordResponses.Keys)
            {
                if (lowerInput.Contains(key))
                {
                    lastRecognizedKeyword = key; // Save to long-term conversation memory
                    List<string> responses = keywordResponses[key];

                    // Personalize response by recalling their stored name
                    return $"{responsePrefix}As we look at your {key} settings, {userName}, remember: {responses[random.Next(responses.Count)]}";
                }
            }

            // 4. Fault-Tolerant Fallback Response (Requirement 7 Error Handling)
            return $"Bot: I'm not entirely sure I caught that context, {userName}. Could you rephrase it using terms like 'password', 'scam', or 'privacy'?";
        }
    }
}