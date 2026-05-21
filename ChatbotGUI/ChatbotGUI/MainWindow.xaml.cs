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
                //user tesxt
                TextBlock userText = new TextBlock();

                userText.Text = userInput;
                userText.Foreground = Brushes.White;
                userText.FontWeight = FontWeights.Bold;
                userText.TextWrapping = TextWrapping.Wrap;
                userText.MaxWidth = 600;

                //user bubble
                Border userBubble = new Border();
              
                userBubble.Background = Brushes.Gray;
                userBubble.CornerRadius = new CornerRadius(15);

                userBubble.Padding = new Thickness(10);
                userBubble.Margin = new Thickness(200, 5, 10, 5);
                userBubble.Child = userText;

                //align the right container
                StackPanel container = new StackPanel();
                container.HorizontalAlignment = HorizontalAlignment.Right;
                container.Children.Add(userBubble);

                //add to chat
                BlockUIContainer userContainer = new BlockUIContainer(container);//add user message to chat
                rtbChat.Document.Blocks.Add(userContainer);
                rtbChat.ScrollToEnd();

                txtInput.Clear();
                txtInput.Focus();//focus on input after sending message

                ShowTyingIndicator();
                //get chatbot response
                await Task.Delay(1000);
                HideTypingIndicator();
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
           
            //chatbot styling
            TextBlock botText = new TextBlock();

            botText.Foreground = Brushes.DarkOrange;
            botText.FontWeight = FontWeights.Bold;
            botText.TextWrapping = TextWrapping.Wrap;
            botText.MaxWidth = 450;
              
            //bot bubble
            Border botBubble = new Border();

            botBubble.Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
            botBubble.CornerRadius = new CornerRadius(15);

            botBubble.Padding = new Thickness(10);
            botBubble.Margin = new Thickness(10, 5, 200, 5);
            botBubble.Child = botText;//add chatbot message to chat

            //align the container left
            StackPanel container = new StackPanel();

            container.HorizontalAlignment = HorizontalAlignment.Left;
            container.Children.Add(botBubble);
            
            //add to chat
            BlockUIContainer botContainer = new BlockUIContainer(container);
            rtbChat.Document.Blocks.Add(botContainer);

            foreach (char letter in message)
            {
                botText.Text += letter;
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
            logoRun.Foreground = Brushes.Aqua;
            logoRun.FontFamily = new FontFamily("Consolas");
            logoRun.FontWeight = FontWeights.Bold;

            logoParagraph.Inlines.Add(logoRun);
            rtbChat.Document.Blocks.Add(logoParagraph);


            await Task.Delay(500); // pause before welcome message

            //welcome message with user name
            string welcomeMessage = $"Welcome to the Cybersecurity Chatbot, {userName}! Ask me anything about online safety.";

            speech.Speak(welcomeMessage);
            await TypeWriterEffect("CBOT: " + welcomeMessage);

            await Task.Delay(2500);

            await TypeWriterEffect(@" Choose a Cybersecurity topic: 
    1. Password Security
    2. Phishing Awareness
    3. Malware
    4. Privacy
    5. Firewall
    6. Hacker
    7. Scam
    8. Ransomware
    9. Virus");

        }

        private Paragraph typingParagraph;
        private void ShowTyingIndicator()
        {
            typingParagraph = new Paragraph(new Run("..."));
            typingParagraph.Foreground = Brushes.Gray;
            // typingParagraph.Inlines.Add(typingParagraph);
            rtbChat.Document.Blocks.Add(typingParagraph);
            rtbChat.ScrollToEnd();
        }
        private void HideTypingIndicator()
        {
            if (typingParagraph != null)
            {
                rtbChat.Document.Blocks.Remove(typingParagraph);
                typingParagraph = null;// Remove the typing indicator from the chat
            }

        }
    }
}