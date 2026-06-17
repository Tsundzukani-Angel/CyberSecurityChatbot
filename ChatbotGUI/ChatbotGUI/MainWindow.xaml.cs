using System.Collections.Generic;
using System.Net.Quic;
using System.Runtime.Intrinsics.X86;
using System.Speech.Synthesis;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        DatabaseManager db = new DatabaseManager();
        ChatbotEngine bot = new ChatbotEngine();

        GreetingVoiceSpeech speech = new GreetingVoiceSpeech();
        QuizManager quizManager = new QuizManager();

        bool addingTask = false;
        bool waitingForDescription = false;
        bool waitingForReminder = false;
        string taskTitle = "";
        string taskDescription = "";
        private List<string> activityLog = new List<string>();


        string userName = "";
        public MainWindow()
        {
            InitializeComponent();

           rtbChat.IsReadOnly = true;
            //used ChatGPT
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

                string input = userInput.ToLower();
                //ask for task title
                if (input.Contains("add task") ||
                    input.Contains("create task") ||
                    input.Contains("new task") ||
                    input.Contains("remind me to") ||
                    input.Contains("set a reminder") ||
                    input.Contains("create reminder"))
                {
                    LogActivity("NLP recognized user intent to add a task.");

                    addingTask = true;
                    await BotReply("Please enter the task title.");
                    txtInput.Clear();
                    return;
                }

                //ask for description
                if (addingTask)
                {
                    taskTitle = userInput;

                    addingTask = false;
                    waitingForDescription = true;

                    await BotReply("Please enter the task description.");

                    txtInput.Clear();
                    return;
                }

                //ask for reminder date
                if (waitingForDescription)
                {
                    taskDescription = userInput;
                    waitingForDescription = false;
                    waitingForReminder = true;

                    await BotReply("Please enter a reminder date ( format: yyyy-MM-dd) or type NONE.");
                    txtInput.Clear();
                    return;
                }

                //add task to database
                if (waitingForReminder)
                {
                    DateTime? reminderDate = null;
                    if (userInput.ToUpper() != "NONE")
                    {
                        if (DateTime.TryParse(userInput, out DateTime date))//parse the date
                        {
                            reminderDate = date;
                        }
                    }
                    db.AddTask(taskTitle, taskDescription, reminderDate);
                    await BotReply("Task added successfully!");
                    await Task.Delay(1500);

                    LogActivity($"Added task: {taskTitle}");

                    //log reminder if date was set
                    if (reminderDate.HasValue)
                    {
                        LogActivity($"Set reminder for task: {taskTitle} on {reminderDate.Value:yyyy-MM-dd}");
                    }
                    waitingForReminder = false;
                    txtInput.Clear();
                    return;
                }

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

                userBubble.Background = Brushes.DeepSkyBlue;
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

                txtInput.Clear();//clear input after sending message
                txtInput.Focus();//focus on input after sending message

                //check for exit commands
                if (userInput.Trim().ToLower().Contains("exit") || userInput.Trim().ToLower().Contains("bye") ||
                    userInput.Trim().ToLower().Contains("goodbye"))
                {
                    string exitMessage = $"Goodbye {userName}! Stay safe online!";
                    speech.Speak(exitMessage);
                    await TypeWriterEffect(exitMessage);
                    await Task.Delay(2000);//close application after goodbye message
                    Application.Current.Shutdown();
                    return;
                }

                //check for activity log command
                if (input.Contains("show activity log") ||
                    input.Contains("activity log") ||
                    input.Contains("show history") ||
                    input.Contains("recent activity") ||
                    input.Contains("what have i done"))
                {
                    LogActivity("NLP recognized user intent to view activity log.");

                    if (activityLog.Count == 0)//check if log is empty
                    {
                        await BotReply("Your activity log is empty.");
                        return;
                    }
                    //display last 10 entries in activity log
                    string logOutput = "Activity Log:\n\n";

                    List<string> logs = db.GetActivityLogs();
                    foreach (string entry in logs)
                    {
                        logOutput += entry + "\n";
                    }
                    await BotReply(logOutput);
                    return;
                }

                //check for quiz command
                if (input.Contains("start quiz") ||
                    input.Contains("play quiz") ||
                    input.Contains("quiz") ||
                    input.Contains("begin quiz"))
                {
                    LogActivity("NLP recognized user intent to start a cybersecurity quiz.");

                    quizManager.QuizMode = true;
                    quizManager.CurrentQuestion = 0;
                    quizManager.Score = 0;

                    LogActivity("Started a cybersecurity quiz.");
                    await BotReply("Cybersecurity Quiz Started!\n\nYou will be asked 10 questions. Type your answer and press Enter.\n\nLet's begin!\n\n" + quizManager.GetCurrentQuestion().Question);
                    return;
                }

                if (quizManager.QuizMode)
                {
                   CheckQuizAnswer(userInput);
                    return;
                }

                ShowTyingIndicator();
                //get chatbot response
                await Task.Delay(1000);
                HideTypingIndicator();

                string response = bot.GetResponse(userInput);

                if (input.Contains("help"))
                {
                    DisplayInstantMessage(@"AVAILABLE CYBERSECURITY TOPICS AND FEATURES

    CYBERSECURITY TOPICS
    1. Password Security
    2. Phishing Awareness
    3. Malware
    4. Privacy
    5. Firewall
    6. Hacker
    7. Scam
    8. Ransomware
    9. Virus
   

    TASK ASSISTANT
    1. Show tasks
    2. Complete task
    3. Delete task

    ACTIVITY LOG
    • Show activity log

    OTHER HELP COMMANDS 
    • Help
    • Bye / Exit

    Type Help anytime to see this menu again.");
                    return;

                }

                if (input.Contains("show tasks") ||
                    input.Contains("view tasks") ||
                    input.Contains("what are my tasks") ||
                    input.Contains("my tasks") ||
                    input.Contains("display tasks"))
                {
                    LogActivity("NLP recognized user intent to view all tasks.");
                    List<TaskItem> tasks = db.GetAllTasks();

                    if (tasks.Count == 0)
                    {
                        await BotReply("You have no tasks at the moment.");
                    }

                    string taskList = "Your Tasks:\n\n";
                    foreach (TaskItem task in tasks)
                    {
                        taskList += $"ID: {task.TaskID}\n" +
                            $"Title: {task.Title}\n" +
                            $"Description: {task.Description}\n" +
                            $"Reminder: {(task.ReminderDate.HasValue ? task.ReminderDate.Value.ToString("yyyy-MM-dd") : "None")}\n" +
                            $"Status: {task.Status}\n";
                    }
                    await TypeWriterEffect(taskList);
                    return;
                }

                //complete task command
                if (input.StartsWith("complete task") ||
                    input.StartsWith("finish task") ||
                    input.StartsWith("mark task as complete"))
                {
                    LogActivity("NLP recognized user intent to complete a task.");
                    try
                    {
                        string[] parts = userInput.Split(' ');
                        int taskId = int.Parse(parts[2]);
                        db.CompleteTask(taskId);

                        await TypeWriterEffect($"Task {taskId} marked as completed.");
                        await Task.Delay(1500);
                        LogActivity($"Completed task: {taskId}");
                        return;
                    }
                    catch
                    {
                        await BotReply("Please use the format: complete task 1");
                        return;
                    }
                }

                //delete task command
                if (input.StartsWith("delete task") ||
                    input.StartsWith("remove task") ||
                    input.StartsWith("erase task"))
                {
                    LogActivity("NLP recognized user intent to delete a task.");
                    try
                    {
                        string[] parts = userInput.Split(' ');
                        int taskId = int.Parse(parts[2]);
                        db.DeleteTask(taskId);
                        await TypeWriterEffect($"Task {taskId} has been deleted.");
                        await Task.Delay(1500);
                        LogActivity($"Deleted task: {taskId}");
                        return;
                    }
                    catch
                    {
                        await BotReply("Please use the format: delete task 1");
                        return;
                    }
                }

                if (chkVoice.IsChecked == true)
                {
                    speech.Speak(response);
                }
                //chatbot response
                await TypeWriterEffect(response);
            }
            catch
            {
                AddMessage("Sorry, something went wrong. Please try again.");
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

            botText.Foreground = Brushes.WhiteSmoke;
            botText.FontWeight = FontWeights.Bold;
            botText.TextWrapping = TextWrapping.Wrap;
            botText.MaxWidth = 450;

            //bot bubble
            Border botBubble = new Border();

            botBubble.Background = new SolidColorBrush(Color.FromRgb(0, 100, 0));
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
                await Task.Delay(55); // typing speed 
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

            logoParagraph.Margin = new Thickness(10, 10, 0, 0);

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
            logoRun.Foreground = Brushes.Navy;
            logoRun.FontFamily = new FontFamily("Consolas");
            logoRun.FontWeight = FontWeights.Bold;

            logoParagraph.Inlines.Add(logoRun);
            rtbChat.Document.Blocks.Add(logoParagraph);


            await Task.Delay(500); // pause before welcome message

            //welcome message with user name
            string welcomeMessage = $"Welcome to the Cybersecurity Chatbot, {userName}! Ask me anything about online safety.";

            speech.Speak(welcomeMessage);
            await TypeWriterEffect(welcomeMessage);

            await Task.Delay(2500);

            DisplayInstantMessage(@"
CYBERSECURITY TOPICS

  1. Password Security
  2. Phishing Awareness
  3. Malware
  4. Privacy
  5. Firewall
  6. Hacker
  7. Scam
  8. Ransomware
  9. Virus

Use the menu above for:
  • Tasks
  • Quiz
  • Activity Log
  • Exit

Type Help anytime to see this menu again.");
            return;

        }

        private Paragraph typingParagraph;
        private void ShowTyingIndicator()
        {
            typingParagraph = new Paragraph(new Run("..."));
            typingParagraph.Foreground = Brushes.Gray;
            rtbChat.Document.Blocks.Add(typingParagraph);
            rtbChat.ScrollToEnd();
        }

        // Remove the typing indicator from the chat
        private void HideTypingIndicator()
        {
            if (typingParagraph != null)
            {
                rtbChat.Document.Blocks.Remove(typingParagraph);
                typingParagraph = null;// Remove the typing indicator from the chat
            }

        }
        private void LogActivity(string action)
        {
            // Create a log entry with the current timestamp and action description
            string logEntry = $"{DateTime.Now: yyyy-MM-dd HH:mm:ss}: {action}";

            activityLog.Add(logEntry);

            db.AddActivityLog(action);
        }

        private async Task CheckQuizAnswer(string userAnswer)
        {
            string feedback = quizManager.CheckAnswer(userAnswer);

            await TypeWriterEffect(feedback);

            if (quizManager.IsQuizFinished())
            {
                await TypeWriterEffect(quizManager.GetFinalResult());
                LogActivity($"Completed a cybersecurity quiz with score: {quizManager.Score}/{quizManager.Questions.Count}");
                quizManager.QuizMode = false;
            }
            else
            {
                await TypeWriterEffect(quizManager.GetCurrentQuestion().Question);
            }
           
        }

        private void DisplayInstantMessage(string msg)
        {
            TextBlock botText = new TextBlock();

            botText.Text = msg;
            botText.Foreground = Brushes.WhiteSmoke;
            botText.TextWrapping = TextWrapping.Wrap;
            botText.FontWeight = FontWeights.Bold;
            botText.Width = 450;

            Border botBubble = new Border();

            botBubble.Background = new SolidColorBrush(Color.FromRgb(0, 100, 0));
            botBubble.CornerRadius = new CornerRadius(15);
            botBubble.Padding = new Thickness(10);
            botBubble.Margin = new Thickness(10, 5, 200, 5);
            botBubble.Child = botText;

            //container alignment
            StackPanel container = new StackPanel();
            container.HorizontalAlignment = HorizontalAlignment.Left;
            container.Children.Add(botBubble);

            BlockUIContainer botContainer = new BlockUIContainer(container);
            rtbChat.Document.Blocks.Add(botContainer);

            rtbChat.ScrollToEnd();
        }

        private async Task BotReply(string message)
        {
            ShowTyingIndicator();
            await Task.Delay(1000);
            HideTypingIndicator();

            if(chkVoice.IsChecked == true)
            {
                speech.Speak(message);
            }
            await TypeWriterEffect(message);
        }

        private async void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddUserMessage("Add Task");
            LogActivity("Opened Add Task menu");

            ShowTyingIndicator();
            
            await BotReply("Please enter the task title.");
            addingTask = true;
        }

        private async void ShowTasks_Click(object sender, RoutedEventArgs e)
        {
            AddUserMessage("Show Tasks");
            LogActivity("Viewed all tasks");

            ShowTyingIndicator();
            List<TaskItem> tasks = db.GetAllTasks();
            if (tasks.Count == 0) 
            {
                await BotReply("You have no tasks at the moment.");
                return; 
            }

            string taskList = "Your Tasks:\n\n";

            foreach(TaskItem task in tasks)
            {
                taskList += $"ID: {task.TaskID}\n" +
                    $"Title: {task.Title}\n" +
                    $"Description: {task.Description}\n" +
                    $"Status: {task.Status}\n";
            }

            await BotReply(taskList);
        }

        private async void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            AddUserMessage("Start Quiz");

            LogActivity("Started a cybersecurity quiz.");
            ShowTyingIndicator();
            quizManager.QuizMode = true;
            quizManager.CurrentQuestion = 0;
            quizManager.Score = 0;

            await BotReply("Cyber Quiz Started!\n\n" + quizManager.GetCurrentQuestion().Question);
        }

        private async void ShowLog_Click(object sender, RoutedEventArgs e)
        {
            AddUserMessage("Show Activity Log");
            ShowTyingIndicator();

            List<string> logs = db.GetActivityLogs();

            HideTypingIndicator();

            if (logs.Count == 0)
            {
                await BotReply("Activity log is empty.");
                return;
            }

            string logText = "Activity Log:\n\n";
            
            foreach(string entry in logs)
            {
                logText += entry + "\n";
            }
            await BotReply(logText);
        }

        private async void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            AddUserMessage("Complete Task");
            ShowTyingIndicator();
            await BotReply("Type: complete task [TaskID]\n\nExample: complete task 1");
        }
        private async void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            AddUserMessage("Delete Task");
            ShowTyingIndicator();
            await BotReply("Type: delete task [TaskID]\n\nExample: delete task 1");
        }
        private async void Help_Click(object sender, RoutedEventArgs e)
        {
            AddUserMessage("Help");
            ShowTyingIndicator();

            DisplayInstantMessage(
        @"CYBERSECURITY TOPICS

  1. Password Security
  2. Phishing Awareness
  3. Malware
  4. Privacy
  5. Firewall
  6. Hacker
  7. Scam
  8. Ransomware
  9. Virus

Use the menu above for:
 • Tasks
 • Quiz
 • Activity Log
 • Exit

Type Help anytime to see this menu again.");
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddUserMessage(string message)
        {
            TextBlock userText = new TextBlock();

            userText.Text = message;
            userText.Foreground = Brushes.White;
            userText.FontWeight = FontWeights.Bold;
            userText.TextWrapping = TextWrapping.Wrap;
            userText.MaxWidth = 600;

            Border userBubble = new Border();

            userBubble.Background = Brushes.DeepSkyBlue;
            userBubble.CornerRadius = new CornerRadius(15);
            userBubble.Padding = new Thickness(10);
            userBubble.Margin = new Thickness(200, 5, 10, 5);

            userBubble.Child = userText;

            StackPanel container = new StackPanel();

            container.HorizontalAlignment = HorizontalAlignment.Right;
            container.Children.Add(userBubble);

            BlockUIContainer userContainer = new BlockUIContainer(container);
            rtbChat.Document.Blocks.Add(userContainer);
            rtbChat.ScrollToEnd();
        }
    }
} 
