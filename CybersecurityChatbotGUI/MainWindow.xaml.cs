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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Random generator for picking varied responses
        private Random random = new Random();

        // Generic Dictionary holding Lists of responses for specific keywords (Requirement 3 & 8)
        private Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            {
                "password", new List<string> {
                    "Bot: Make sure to use strong, unique passwords with a mix of numbers and symbols. Avoid using personal details!",
                    "Bot: Consider using a trusted Password Manager so you don't have to write down complex passwords.",
                    "Bot: A good practice is using passphrases—four or more random words combined. They are long but easy to remember!"
                }
            },
            {
                "scam", new List<string> {
                    "Bot: Always double-check urgent messages asking for money or personal details. Scammers thrive on creating panic!",
                    "Bot: If an offer looks too good to be true, it's highly likely a scam. Never click links from unverified numbers.",
                    "Bot: Scammers often impersonate banks or government entities. Your bank will never ask for your PIN or password over SMS."
                }
            },
            {
                "privacy", new List<string> {
                    "Bot: Protect your privacy by regularly auditing your social media permission settings and hiding personal contact info.",
                    "Bot: Public Wi-Fi networks can expose your browsing data. Use a VPN to protect your privacy when out and about.",
                    "Bot: Be careful what you share online! Oversharing answers to common security questions makes a hacker's job easy."
                }
            }
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();

            // Don't do anything if the user submitted blank text
            if (string.IsNullOrEmpty(userInput)) return;

            // 1. Show user message in the chat history
            txtChatHistory.AppendText($"You: {userInput}\n");

            // 2. Clear the input text box for the next message
            txtUserInput.Clear();

            // 3. Process the chatbot's response using the engine
            string botResponse = GetBotResponse(userInput);
            txtChatHistory.AppendText($"{botResponse}\n\n");

            // 4. Scroll down to the latest message automatically
            txtChatHistory.ScrollToEnd();
        }

        // Response Engine Method parsing input (Requirement 2 & 7)
        private string GetBotResponse(string input)
        {
            string lowerInput = input.ToLower();

            // Loop through our generic dictionary keys to detect keywords
            foreach (var key in keywordResponses.Keys)
            {
                if (lowerInput.Contains(key))
                {
                    // Pick a random response from the matching list
                    List<string> responses = keywordResponses[key];
                    int index = random.Next(responses.Count);
                    return responses[index];
                }
            }

            // Default fallback error handling response if no keyword matches
            return "Bot: I'm not quite sure I understand. Could you please try rephrasing your question using keywords like 'password', 'scam', or 'privacy'?";
        }
    }
}