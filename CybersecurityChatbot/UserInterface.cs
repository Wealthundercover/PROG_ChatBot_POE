using System;
using System.Media; // <--- This allows sound
using System.IO;    // <--- This helps find the file folder

namespace CybersecurityChatbot
{
    public class UserInterface
    {
        // ADDED: Method to play the audio 
        public static void PlayVoiceGreeting()
        {
            try
            {
                // This creates a reliable path to the folder where the app is running
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "greeting.wav");

                if (File.Exists(audioPath))
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.Load();
                        player.PlaySync(); // PlaySync waits for audio to finish before showing the logo
                    }
                }
                else
                {
                    Console.WriteLine($"\n[DEBUG]: Audio file not found at: {audioPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[SYSTEM ERROR]: Audio failed. " + ex.Message);
            }
        }

        //  logo method
        public static void DisplayHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string logo = @"
  _______________________________________________________________
 /                                                               \
|    ______     __             _    _                            |
|   / ____/_  _/ /_  ___  ____| |  / /_  ______  ____            |
|  / /   / / / / __ \/ _ \/ ___/ | / / / / / __ \/ _ \           |
| / /___/ /_/ / /_/ /  __/ /   | |/ / /_/ / /_/ / / / /           |
| \____/\__, /_.___/\___/_/    |___/\__,_/\____/_/ /_/            |
|      /____/                                                     |
 \_______________________________________________________________/
            ";
            Console.WriteLine(logo);
            Console.WriteLine("          --- CYBER-SEC ADVISOR v1.0 ---");
            Console.WriteLine("===============================================================");
            Console.ResetColor();
        }

        //  greeting method
        public static void GreetUser()
        {
            Console.WriteLine("\n[SYSTEM]: Initializing secure connection...");
            Console.WriteLine("[SYSTEM]: Welcome, Agent. I am your Cybersecurity Assistant.");
            Console.WriteLine("---------------------------------------------------------------\n");
        }
    }
}