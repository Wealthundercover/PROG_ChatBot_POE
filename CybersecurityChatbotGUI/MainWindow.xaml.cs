using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CybersecurityChatbotGUI
{
    // DELEGATE DEFINITION
    public delegate void BotResponseDelegate(string text, string hexColor);

    public class UI_CyberTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ReminderTime { get; set; } = string.Empty;
    }

    public partial class MainWindow : Window
    {
        // Standalone operational backend instance (Decoupled Class Layer)
        private ChatbotEngine coreEngine = new ChatbotEngine();

        // UI Delegate implementation element
        private BotResponseDelegate responseWriter;

        // Observable Binding collection framework for task data grid matrix
        public ObservableCollection<UI_CyberTask> GridCyberTasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            GridCyberTasks = new ObservableCollection<UI_CyberTask>();
            LvwTasksDisplay.ItemsSource = GridCyberTasks;

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

            // Inject an initial default baseline record to demonstrate collection architecture binding link
            GridCyberTasks.Add(new UI_CyberTask { Id = 1, Title = "Enforce Core Perimeter Multi-Factor Authentication", Description = "Establish mandatory validation layers on all active enterprise portal entry vectors.", ReminderTime = "Remind me in 7 days" });

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

            // NLP Timeframe Assignment interception layer checks
            if (coreEngine.NlpExpectingTaskConfirmation)
            {
                coreEngine.NlpExpectingTaskConfirmation = false;
                int generatedIndexId = GridCyberTasks.Count > 0 ? GridCyberTasks.Max(t => t.Id) + 1 : 1;

                GridCyberTasks.Add(new UI_CyberTask
                {
                    Id = generatedIndexId,
                    Title = coreEngine.NlpPendingTaskTitle,
                    Description = coreEngine.NlpPendingTaskDesc,
                    ReminderTime = userInput
                });

                coreEngine.LogSystemActivityEntry($"Task Committed Via Conversational Flow Pattern: '{coreEngine.NlpPendingTaskTitle}'");
                responseWriter($"Bot: ⚡ [AUTOMATED TASK INJECTION]\n>>> Status: Record generated successfully into grid dataset matrix.\n>>> Component: '{coreEngine.NlpPendingTaskTitle}'\n>>> Deadline Parameter: '{userInput}'\n\n", "#10B981");
                rtbChatHistory.ScrollToEnd();
                return;
            }

            // Let the backend class calculate the clean response string data safely
            string botResponse = coreEngine.CoreResponseEngine(userInput);

            // Handle functional execution keywords sent back from the backend class processor
            if (botResponse == "CONTAINS_TASK_INTENT")
            {
                responseWriter($"Bot: 📋 Dynamic task framework initialized: '{coreEngine.NlpPendingTaskTitle}'. Please type a reminder timeframe value into the command prompt box to append to this task (e.g. 'In 24 hours' or 'Every Friday').\n\n", "#F59E0B");
            }
            else if (botResponse == "CONTAINS_START_QUIZ_INTENT")
            {
                responseWriter($"Bot: 🎮 INTERACTIVE THREAT ASSESSMENT SYSTEM ACTIVE.\nType your response key selection (A, B, C, D or TRUE/FALSE) into the command field prompt box directly below to evaluate answers.\n\n" + coreEngine.GetFirstQuizQuestion() + "\n\n", "#A855F7");
            }
            else
            {
                // Stream the standard message content text back into our front-end view layout
                responseWriter($"{botResponse}\n\n", "#4ADE80");
            }

            // Keep user labels dynamically in sync with state changes
            lblSubHeader.Text = $"Active Session Profile: [OPERATOR: {coreEngine.UserName.ToUpper()}] • [TRACK SPECIALTY: {(string.IsNullOrEmpty(coreEngine.FavoriteTopic) ? "NONE REGISTERED" : coreEngine.FavoriteTopic.ToUpper())}]";
            rtbChatHistory.ScrollToEnd();
        }

        // CORE UI RENDERER TARGET
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

        // TAB NAVIGATION TOGGLE ACTION HANDLERS
        private void BtnShowChat_Click(object sender, RoutedEventArgs e)
        {
            PanelTasksView.Visibility = Visibility.Collapsed;
            rtbChatHistory.Visibility = Visibility.Visible;
            coreEngine.LogSystemActivityEntry("Workspace viewport flipped to Rich Chat Terminal View console pane.");
        }

        private void BtnShowTasks_Click(object sender, RoutedEventArgs e)
        {
            rtbChatHistory.Visibility = Visibility.Collapsed;
            PanelTasksView.Visibility = Visibility.Visible;
            coreEngine.LogSystemActivityEntry("Workspace viewport flipped to Relational Tasks Table Matrix panel view.");
        }

        // CRUD COLLECTION MANAGEMENT - MANUAL TASK SUBMISSION FORM
        private void BtnManualAddTask_Click(object sender, RoutedEventArgs e)
        {
            string titleStr = TxtTaskTitle.Text.Trim();
            string descStr = TxtTaskDesc.Text.Trim();
            string reminderStr = TxtTaskReminder.Text.Trim();

            if (string.IsNullOrEmpty(titleStr))
            {
                MessageBox.Show("Please populate at minimum a valid Title descriptor string value to append a row object.", "Validation Alert Deficit");
                return;
            }

            int structuralNextId = GridCyberTasks.Count > 0 ? GridCyberTasks.Max(t => t.Id) + 1 : 1;
            GridCyberTasks.Add(new UI_CyberTask { Id = structuralNextId, Title = titleStr, Description = descStr, ReminderTime = reminderStr });

            coreEngine.LogSystemActivityEntry($"Manual Action Form Execution: Added custom record index row item: '{titleStr}'");

            TxtTaskTitle.Clear();
            TxtTaskDesc.Text = "No supplementary analysis text provided.";
            TxtTaskReminder.Text = "Remind me in 3 days";
        }

        // CRUD COLLECTION MANAGEMENT - PURGE ROW TARGET BUTTON
        private void BtnDeleteSelectedTask_Click(object sender, RoutedEventArgs e)
        {
            if (LvwTasksDisplay.SelectedItem is UI_CyberTask selectedTargetObject)
            {
                coreEngine.LogSystemActivityEntry($"Manual Action Form Execution: Purged task matrix item row: '{selectedTargetObject.Title}'");
                GridCyberTasks.Remove(selectedTargetObject);
            }
            else
            {
                MessageBox.Show("Highlight or select a valid row item on the interface grid list before executing a data row deletion command run.", "Selection Empty Target");
            }
        }
    }
}