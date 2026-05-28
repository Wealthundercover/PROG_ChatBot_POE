using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CybersecurityChatbotGUI
{
    // === MANDATORY PART 2 LEARNING UNIT: DELEGATE DEFINITION ===
    public delegate void BotResponseDelegate(string text, string hexColor);

    public partial class MainWindow : Window
    {
        // Standalone operational backend instance (Decoupled Class Layer)
        private ChatbotEngine coreEngine = new ChatbotEngine();

        // UI Delegate implementation element
        private BotResponseDelegate responseWriter;

        public MainWindow()
        {
            InitializeComponent();

            // Wire up the delegate instance to point to our UI writing method
            responseWriter = new BotResponseDelegate(AppendColorText);

            // FORCE MONOSPACE FONT SO THE MULTI-LINE ASCII ALIGNS PERFECTLY
            rtbChatHistory.FontFamily = new System.Windows.Media.FontFamily("Consolas");

            // --- CHOPPED COMPACT CORE SIGNATURE LOGO ---
            string asciiArtBanner = @"
 ██████╗██╗   ██╗██████╗ ███████╗██████╗     ███████╗███████╗ ██████╗██╗   ██╗██████╗ ██╗████████╗██╗   ██╗     
██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗    ██╔════╝██╔════╝██╔════╝██║   ██║██╔══██╗██║╚══██╔══╝╚██╗ ██╔╝     
██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝    ███████╗█████╗  ██║     ██║   ██║██████╔╝██║   ██║    ╚████╔╝      
██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗    ╚════██║██╔══╝  ██║     ██║   ██║██╔══██╗██║   ██║     ╚██╔╝       
╚██████╗   ██║   ██████╔╝███████╗██║  ██║    ███████║███████╗╚██████╗╚██████╔╝██║  ██║██║   ██║      ██║        
 ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝    ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚═╝   ╚═╝      ╚═╝        
" + "\n";

            // Prevent text-wrapping glitches on load up by scaling down font size temporarily
            rtbChatHistory.FontSize = 8;
            rtbChatHistory.Document.PageWidth = 2500;

            responseWriter(asciiArtBanner, "#A855F7"); // Neon Purple for ASCII Graphic

            // Reset UI layout metrics back to standard viewport scaling
            rtbChatHistory.Document.PageWidth = double.NaN;
            rtbChatHistory.FontSize = 13;

            responseWriter("System: Secure Link Initialized. Welcome to the Cybersecurity Awareness Command Center.\n", "#94A3B8");
            responseWriter("Bot: Before parsing advanced threat vectors, what is your name operator?\n\n", "#4ADE80");
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string input = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            if (coreEngine.IsFirstMessage)
            {
                ProcessMessage(input);
            }
            else
            {
                responseWriter($"{coreEngine.UserName}: {input}\n", "#38BDF8");
                ProcessMessage(input);
            }
        }

        private void TopicButton_Click(object sender, RoutedEventArgs e)
        {
            if (coreEngine.IsFirstMessage)
            {
                responseWriter("Bot: Please establish authentication by inputting your profile name first.\n\n", "#EF4444");
                return;
            }

            Button clickedButton = (Button)sender;
            string topicTag = clickedButton.Tag.ToString();

            responseWriter($"{coreEngine.UserName} [Sidebar]: Tell me about {topicTag}.\n", "#38BDF8");
            ProcessMessage(topicTag);
        }

        private void ProcessMessage(string userInput)
        {
            txtUserInput.Clear();

            // Handling the login state context logic
            if (coreEngine.IsFirstMessage)
            {
                coreEngine.UserName = userInput;
                coreEngine.IsFirstMessage = false;

                responseWriter($"{coreEngine.UserName}: [Authenticated]\n", "#38BDF8");
                responseWriter($"Bot: Identity Confirmed. Welcome Operator {coreEngine.UserName}.\n", "#4ADE80");
                responseWriter("Bot: Select a tactical awareness framework module from the left panel menu dashboard, or type natural language inquiries below.\n\n", "#4ADE80");
                rtbChatHistory.ScrollToEnd();
                return;
            }

            // Let the backend class calculate the clean response string data safely
            string botResponse = coreEngine.CoreResponseEngine(userInput);

            // Stream the string result back into our front-end view layout
            responseWriter($"{botResponse}\n\n", "#4ADE80");
            rtbChatHistory.ScrollToEnd();
        }

        // --- CORE UI RENDERER TARGET ---
        private void AppendColorText(string text, string hexColor)
        {
            if (flowDoc == null) return;

            TextRange tr = new TextRange(flowDoc.ContentEnd, flowDoc.ContentEnd);
            tr.Text = text;
            try
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, new BrushConverter().ConvertFromString(hexColor));
            }
            catch
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.White);
            }
        }
    }
}