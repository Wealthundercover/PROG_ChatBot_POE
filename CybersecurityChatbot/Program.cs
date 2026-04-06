using CybersecurityChatbot;

// Multimedia Setup
UserInterface.PlayVoiceGreeting();
UserInterface.DisplayHeader();

// Initialize the "Brain"
ChatbotEngine bot = new ChatbotEngine();

// Interaction
UserInterface.GreetUser();
bot.GetUserName();

// Loop to keep the chat going
bool isRunning = true;
while (isRunning)
{
    Console.Write($"{bot.UserName} > ");
    string userRequest = Console.ReadLine();

    if (userRequest?.ToLower() == "exit") break;

    bot.ProcessUserQuery(userRequest);

}

Console.WriteLine("Connection Terminated. Stay safe.");
Console.ReadKey();
