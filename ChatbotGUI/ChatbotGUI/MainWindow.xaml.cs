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
using System.Collections.Generic;
using System.Speech.Synthesis;

namespace ChatbotGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChatbotEngine bot = new ChatbotEngine();
        GreetingVoiceSpeech speech = new GreetingVoiceSpeech();

        public MainWindow()
        {
            InitializeComponent();
            userName = Microsoft.VisualBasic.Interaction.InputBox(
            "Enter your name:",
            "Cybersecurity Chatbot",
            "User"
           );

            LoadWelcomeMessage();
        }


        string userName = "";

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtInput.Text;


            if (string.IsNullOrWhiteSpace(userInput))
                return;

            rtbChat.Document.Blocks.Add(new Paragraph(new Run(userName + ": " + userInput)));
            txtInput.Clear();
            await TypeWriterEffect("Chatbot is typing...");

            string response = bot.GetResponse(userInput);


            await TypeWriterEffect("CBOT: " + response);

        }
        //typewriter effect for chatbot responses
        private async Task TypeWriterEffect(string message)
        {
            Paragraph paragraph = new Paragraph();
            Run run = new Run();
            paragraph.Inlines.Add(run);
            rtbChat.Document.Blocks.Add(paragraph);

            foreach (char letter in message)
            {
                run.Text += letter;
                await Task.Delay(50); // typing speed 
            }
        }
        // Handle Enter key press to send message
        //"enter" key doesn't work
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSend.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
        private async void LoadWelcomeMessage()
        {
            
            string welcomeMessage = $"Welcome to the Cybersecurity Chatbot, {userName}! Ask me anything about online safety.";
            speech.Speak(welcomeMessage);
            await TypeWriterEffect(welcomeMessage);
            

        }
    }
}