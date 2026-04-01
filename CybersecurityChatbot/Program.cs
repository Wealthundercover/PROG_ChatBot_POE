using CybersecurityChatbot;

// 1. Call the audio method first!
UserInterface.PlayVoiceGreeting();

// 2. Then show the visuals
UserInterface.DisplayHeader();
UserInterface.GreetUser();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();