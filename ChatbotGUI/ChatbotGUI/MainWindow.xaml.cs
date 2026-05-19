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

        string userName = "";
        public MainWindow()
        {
            InitializeComponent();
            rtbChat.IsReadOnly = true;
            userName = Microsoft.VisualBasic.Interaction.InputBox(
            "Enter your name:",
            "Cybersecurity Chatbot",
            "User"
           );

            LoadWelcomeMessage();
        }

        //send button click event handler
        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userInput = txtInput.Text;

                if (string.IsNullOrWhiteSpace(userInput))
                    return;

                // user message
                Paragraph userParagraph = new Paragraph();

                Run userRun = new Run(userName + ": " + userInput);
                userRun.Foreground = Brushes.DeepSkyBlue;

                userParagraph.Inlines.Add(userRun);
                rtbChat.Document.Blocks.Add(userParagraph);
                rtbChat.ScrollToEnd();
                txtInput.Clear();

                //get chatbot response
                await TypeWriterEffect("...");
                await Task.Delay(800);
                string response = bot.GetResponse(userInput);

                if (chkVoice.IsChecked == true)
                {
                    speech.Speak(response);
                }
                //chatbot response
                await TypeWriterEffect("CBOT: " + response);
            }
            catch
            {
                AddMessage("CBOT: Sorry, something went wrong. Please try again."); 
            }

        }
        //add message to chat
        private void AddMessage(string message)
        {
            Paragraph paragraph = new Paragraph(new Run(message));

            rtbChat.Document.Blocks.Add(paragraph);

            rtbChat.ScrollToEnd();
        }

        //typewriter effect for chatbot responses
        private async Task TypeWriterEffect(string message)
        {
            Paragraph paragraph = new Paragraph();

            //chatbot styling
            Run run = new Run();
            paragraph.Inlines.Add(run);
            run.Foreground = Brushes.LimeGreen;
            
            
            rtbChat.Document.Blocks.Add(paragraph);

            foreach (char letter in message)
            {
                run.Text += letter;
                rtbChat.ScrollToEnd();
                await Task.Delay(30); // typing speed 
            }
           
        }

        // Handle Enter key press to send message
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
               btnSend.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
        private async void LoadWelcomeMessage()
        {
            //logo paragraph
            Paragraph logoParagraph = new Paragraph();

            //logo text
            logoParagraph.TextAlignment = TextAlignment.Center;
            logoParagraph.Margin = new Thickness(0, 10, 0, 0);

                        Run logoRun = new Run(@"
                 ██████╗ ██████╗  ██████╗ ████████╗
                ██╔════╝ ██╔══██╗██╔═══██╗╚══██╔══╝
                ██║      ██████╔╝██║   ██║   ██║   
                ██║      ██╔══██╗██║   ██║   ██║   
                ╚██████╗ ██████╔╝╚██████╔╝   ██║   
                 ╚═════╝ ╚═════╝  ╚═════╝    ╚═╝   
             ");
            //styling logo
            logoRun.FontSize = 16;
            logoRun.Foreground = Brushes.LimeGreen;
            logoRun.FontFamily = new FontFamily("Consolas");
            logoRun.FontWeight = FontWeights.Bold;

            logoParagraph.Inlines.Add(logoRun);
            rtbChat.Document.Blocks.Add(logoParagraph);

            await Task.Delay(500); // pause before welcome message

            //welcome message with user name
            string welcomeMessage = $"Welcome to the Cybersecurity Chatbot, {userName}! Ask me anything about online safety.";
           
            speech.Speak(welcomeMessage);
            await TypeWriterEffect("CBOT: " + welcomeMessage);

        }
    }
}