CYBER SECURITY AWARENESS (CBOT)
This project was developed as part of a Programming PoE and demonstrates practical 
cybersecurity awareness through an interactive chatbot system

PART 1  

This project is a C# console-based chatbot designed to educate users about cybersecurity topics 
such as password safety, phshing, computer viruses and safe browsing.
The chatbot provides an interactive experience using menus, submenus, and real time responses to 
help users learn how to stay safe online.

ADDED FEATURES
1. Voice Greeting(audio support)
2. ASCII logo with colored console UI
3. Typewriter text effect
4. Typing delay simulation
5. Personalized user interaction
6. Menu-driven navigation system
7. Submenus with detailed cybersecurity topics
8. Input Validation for user entries
9. Excpetion handling for stability

SCREENSHOTS
 //Startup Interface
<img width="986" height="460" alt="Screenshot 2026-04-12 230045" src="https://github.com/user-attachments/assets/5cbeb124-649f-443f-ba18-4c36374b9e32" />
 //Menu
 <img width="1092" height="383" alt="Screenshot 2026-04-12 230138" src="https://github.com/user-attachments/assets/0cf88177-f59f-49bb-8366-114c8d234082" />
 //Submenu
 <img width="978" height="697" alt="Screenshot 2026-04-12 230251" src="https://github.com/user-attachments/assets/f8021b16-e9c5-40ce-9513-99044b350e51" />


HOW TO RUN THE PROJECT
1. Open project in visual studio
2. Rstore NuGet package
3. Build the solution
4. Run the program


PROJECT STRUCTURE
program. cs -> Entry poinr of the application
chatbot.cs -> Handles chatbot logic and interaction
UIHelper.cs -> Handles UI elements like typewriter effect and logo
GreetingVoiceSpeech.cs -> handles voice greeting

KEY CONCEPTS
- Input validation using TryParse
- Exception handling using try-catch
- Loop control structures
- Modular programming using classes and methods
- Console UI enhancement techniques

CHALLENGES AND SOLUTIONS
- Issue: Console colors were not displaying correctly  
  Solution: Removed color handling from the TypeText method to prevent overriding

- Issue: Invalid user input caused errors  
  Solution: Implemented input validation using TryParse and loops

- Issue: Making chatbot interactive  
  Solution: Added menus, submenus, and typing delay simulation

PART 2

This part was developed using C# nad WPF
-The purpose of the chatbot is to educate users about the important cybersecurity topics like: 
  1. Password Safety
  2. Phishing
  3. Malware
  4. Privacy
  5. Firewalls
  6. Hackers
  7. Scams
  8. Ransommware
  9. Viruses

The chatbot interacts with the users via a modern graphical user interface and provides cybersecurity tips in a conversational way. This project improves cybersecurity awraeness while also demonstrating important programming concepts like: 
  1. Object Oriented Programming
  2. GUI Design
  3. Collections
  4. Event Hnadling
  5. Excpetion Handling
  6. Text to speech
  7. Memory features
  8. Random Responses

Objectives
 The objectives of this project are to: 
  - Educate users about cybersecurity threats
  - Create an interactive chatbot experience
  - Implement a modern GUI using WPF
  - Use speech synthesis for accessibility
  - Allow the chatbot to remember user preferences
  - Improve user interaction with animations and styled chat bubbles
    
Technologies used
 The project was developed using:
  - C# => Programming language
  - WPF => GUI development
  - XAML => Interface Design
  - .NET => Application Framework
  - SpeechSynthesizer => Voice Output
  - Visual Studio => Development Environment
    
Features Implemented
  - GUI Interface, the chatbot uses graphical user interface built with WPF and XAML instead of console application.
   Features include:
    -Chat display area 
    - Input textbox
    - Send button
    - Voice toggles checkbox
    - Syles interface with colors and rounded corners

  - Cybersecurity knowledge base
    The chatbot contains information about:
      - Password Safety
      - Phishing
      - Malware
      - Privacy
      - Firewalls
      - Hackers
      - Scams
      - Ransommware
      - Viruses
   Each topic contains multiple educational responses.

 - Random Responses
   The chatbot stored responses using: Dictionary<string, List<string>>
    - This stores a topic as the key, multiple responses as the value.
      Example: responses["phishing"] = new List<string>()
      this allows the chatbot to organize the topics, store multiple responses, and select random responses
    - This allows multiple responses per topic, and random reply selection
   
 - Prevention of repeated responses
   The chatbot prevents repeating the same response tice in a row using: Dictionary<string, int> lastResponseIndexes
    - This improves user experience by making conversations feel more natural.
   
 - Memory Feature
   The chatbot can remember the user's favourite cybersecurity topic.
   Example:
     user: My favourite topic is phishing
     chatbot: Awesome! I'll remember that your favorite cybersecurity topic is phishing.
    -The chatbot can also recall this information later.

  - Voice Output
    The application uses SpeechSynthesizer to read chatbot responses aloud.
    User can enable or disable voice output using the checkbox in the interface.

  - Chat bubbles
    The application visually separates:
     - User messages
     - Chatbot messages

    Features:
     - User messages appear on the right
     - Chatbot messages appear on the left
     - Rounded corners
     - Different colors for each participant

  - Input Validation
    The chatbot validates:
     - Empty input
     - Invalid commands
     - Unexpected interactions
   This prevents crashes and improves reliability

  - Event Handling
    The chatbot uses event-driven programming.
    Example:
     btnSend_Click
    This method runs when the Send button is clicked.

- GUI Design choices

   The interface was designed with:
     - Dark background theme
     - Lime green cybersecurity aesthetic
     - Rounded borders
     - Chat bubble styling
     - Auto-scrolling chat area
       
     These choices improve readability and user experience.
  
 - Challenges Faced
 
   Some challenges encountered during development included:
    - Preventing repeated responses
    - Aligning chat bubbles correctly
    - Implementing speech synthesis
    - Creating the typewriter animation
    - Handling application shutdown correctly
    - Auto-scrolling the RichTextBox

   These were solved through testing and debugging.

<!--Interface-->
<img width="1366" height="720" alt="Screenshot 2026-05-26 213034" src="https://github.com/user-attachments/assets/4493d814-597b-416d-b7d4-85a296a49e3f" />

<img width="1366" height="720" alt="Screenshot 2026-05-26 214012" src="https://github.com/user-attachments/assets/7ca49a07-dab9-40a5-ac5e-af35e0c1f0f5" />

PART 3

CBOT is Cybersecurity Awareness Chatbot developed using C# and WPF.
the chatbot educates users about cyverseucrity topic such as: 
 - Password Safety
 - Phishing Awareness
 - Malware
 - Privacy Protection
 - Firewalls
 - Ransomware
 - Online Scames
The chtbot also includes task management, reminders, a cybersecurity quiz, activity loging, sentiment detection, and databse integration.

FEATURES

Part 1 Features:
 1. Greeting System
    - User enters thei name at startup
    - Personalised welcome message is displayed
   
 2. Cybersecurity Knowledge Base
    The Chatbot provides information on:
    - Password safety
    - Phishing
    - Malware
    - Privacy
    - Firewalls
    - Hackers
    - Scams
    - Viruses
    - Ransomware
   
 3. Sentiment detection
    The chatbot recognises emotions like:
    - Worried
    - Nervous
    - Scared
    - Curious
    Example:
    User: I am worried about hackers
    Bot: It's completely okay to feel worried about cybersecurity.
         Learning safe habits already puts you ahead of many people.
      
  <img width="722" height="720" alt="Screenshot 2026-06-24 231503" src="https://github.com/user-attachments/assets/76fca6f5-5eb6-4b55-8b40-9aae5d78b51d" />

  4. Voice Responses
     Users can enable or disable speech responses using the Voice Enabled checkbox.

Part 2 Features

1. GUI Interface
   Developed using WPF.
   Features include:
   - User Chat bubbles
   - Bot Chat bubbles
   - Typing indicator
   - Navigation menu
   - Voice Support
   - Interactive Chatbot Interface

Part 3 Features

1. Task Assistant
   - Users can create cybersecurity related tasks
     
2. Add Task/Reminder
   <img width="631" height="545" alt="Screenshot 2026-06-20 213855" src="https://github.com/user-attachments/assets/8d726719-27fc-455e-99af-afd272f19b46" />

3. View Task
   <img width="722" height="720" alt="Screenshot 2026-06-24 232757" src="https://github.com/user-attachments/assets/c0165557-d78d-4805-8341-92e6562a1de0" />

- Users can also Complete Task, delete tasks and also cancel a task mid creation.

4. Database Integration
   MySQL is used to store:
   - Tasks
      Stored Information:
      - TaskID
      - Title
      - Description
      - Reminder Date
      - Status
        
   - Activity Logs
     Stored Information:
     - Timestamp
     - User action

   - Chatbot Responses
     -All chatbot responses are stored in the database for tracking and reporting purposes.
<img width="1366" height="720" alt="Screenshot 2026-06-25 001201" src="https://github.com/user-attachments/assets/77b7427b-9735-430d-8b9a-1ba2b6b8ff3d" />

5. Cybersecurity Quiz
   The chatbot contains more than 10 cybersecurity questions.
   Both multiple choice questions and True/False questions.

6. Quiz Features
   - Immediate Feedabck
   - Final score after quiz completions

7. NLP Simulation
   The Chatbot uses keybord detection and string matching to identify user intent.
   Examples:
   - Task Creation Recognizes:
     - Add a task
     - Create a task
     - New task
     - Remind me to
     - Set a reminder

    - Quiz Commands Recognizes:
      - Start quiz
      - Play quiz
      - Test me
      - Challenge me
      - Ask me questions
        
    - Activity Log Commands Recognizes:
      - Show activity log
      - Recent activity
      - Show history
      - What have i done
     This allows the chatbot to understand different wats users may phrase requests.

TECHNOLOGIES USED
1. C#
2. WPF
3. .NET
4. MySQL
5. XAML
6. SpeechSynthesizer

   
HOW TO RUN 
1. Open the project in Visual Studio.
2. Ensure MySQL Server is Running.
3. Configure the database connection string.
4. Build the solution.
5. Run the application.
6. Enter your name when prompted.
7. Start interacting with the chatbot.

CONCLUSION

- This project combines cybersecurity education tasks management, quiz-based learning, NLP simulation, database integration, activity tracking, voice interaction, and a graphical user interface to provide an interactive cybersecurity awareness experience for users.
  Author: Tsundzukani Angel Stoltz
  Diploma in IT (Software Development)
  Rosebank College
