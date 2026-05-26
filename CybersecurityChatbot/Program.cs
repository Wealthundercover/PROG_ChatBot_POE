using CybersecurityChatbot;


UserInterface.PlayVoiceGreeting();
UserInterface.DisplayHeader();


ChatbotEngine bot = new ChatbotEngine();


UserInterface.GreetUser();
bot.GetUserName();

// Loop 
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
