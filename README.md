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




  Author: Tsundzukani Angel Stoltz
  Diploma in IT (Software Development)
  Rosebank College
