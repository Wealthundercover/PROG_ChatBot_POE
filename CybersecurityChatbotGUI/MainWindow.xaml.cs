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

            // 3. Scroll down to the latest message automatically
            txtChatHistory.ScrollToEnd();

            // TODO: We will process the chatbot's response here next!
        }
    }
}