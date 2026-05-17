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

namespace ChatbotGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            userName = Microsoft.VisualBasic.Interaction.InputBox(
            "Enter your name:",
            "Cybersecurity Chatbot",
            "User"
);
        }
        ChatbotEngine bot = new ChatbotEngine();
        string userName = "";
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtInput.Text;
            string response = bot.GetResponse(userInput);

            if (string.IsNullOrWhiteSpace(userInput))
                return;

            rtbChat.Document.Blocks.Add(new Paragraph(new Run(userName + ": " + userInput)));
            
            rtbChat.Document.Blocks.Add(new Paragraph(new Run("Chatbot: " + response)));
            txtInput.Clear();
        }
        // Handle Enter key press to send message
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {

        }
        

    }
}