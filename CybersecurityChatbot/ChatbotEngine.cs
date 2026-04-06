using System;
using System.Threading;

namespace CybersecurityChatbot
{
    public class ChatbotEngine
    {
        // automatic properties to solve a programming problem
        public string UserName { get; set; } = "Agent";

        // Ask for name and personalize
        public void GetUserName()
        {
            Console.Write("[SYSTEM]: Please enter your authorization name: ");
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                UserName = input;
            }

            Console.WriteLine($"\n[BOT]: Access granted. Hello, {UserName}. How can I help you today?");
            Console.WriteLine("(You can ask about 'phishing', 'passwords', or 'safe browsing')\n");
        }

        //  Basic Response System & Input Validation
        public void ProcessUserQuery(string? input)
        {
            // Handle empty entries gracefully
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("[BOT]: I didn't quite catch that. Could you please type a question?");
                return;
            }

            string query = input.ToLower();

            // Simulate a "typing" feel with slight delays
            Console.Write("[BOT]: Thinking");
            for (int i = 0; i < 3; i++) { Thread.Sleep(300); Console.Write("."); }
            Console.WriteLine("\n");

            // Responses for specific cybersecurity topics
            if (query.Contains("phishing"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[ADVICE]: Phishing is when attackers send fake emails to steal passwords.");
                Console.WriteLine("Rule: Never click links from unknown senders!");
            }
            else if (query.Contains("password"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[ADVICE]: Use a mix of upper, lower, numbers, and symbols.");
                Console.WriteLine("Rule: Use a Password Manager to stay secure.");
            }
            else if (query.Contains("safe browsing") || query.Contains("browsing"))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("[ADVICE]: Only visit websites with 'HTTPS' in the URL.");
                Console.WriteLine("Rule: Avoid public Wi-Fi for banking or sensitive logins.");
            }
            else if (query.Contains("how are you"))
            {
                Console.WriteLine("[BOT]: I am functioning within normal parameters. Ready to secure your data.");
            }
            else
            {
                // Default response for unsupported queries
                Console.WriteLine("[BOT]: I didn't quite understand that. Could you rephrase or ask about 'phishing'?");
            }

            Console.ResetColor();
            Console.WriteLine("\n---------------------------------------------------------------");
        }
    }
}