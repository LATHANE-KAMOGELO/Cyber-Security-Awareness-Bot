Cybersecurity Awareness Bot (POE Part 1 & 2)
👤 Student Details
Name: Kamogelo Lathane
Module: Programming
Assessment: POE Part 1
📌 Project Description

The Cybersecurity Awareness Bot is a C# chatbot system designed to educate users about important cybersecurity topics such as password safety, phishing, malware, scams, safe browsing, privacy, and general online safety awareness.

The system simulates a real conversation between a user and a chatbot. It responds intelligently using keyword detection and topic-based logic. The bot also includes interactive features such as ASCII art, audio greeting, and enhanced UI elements (depending on version).

This project demonstrates the use of structured programming, object-oriented design, and user interaction handling in C#.

✨ Features
🧠 Smart keyword-based chatbot responses
💬 Conversational interaction with users
👤 Personalised user experience (name input)
🛡️ Cybersecurity awareness topics
🔊 Voice greeting using WAV audio file
🎨 ASCII art title screen
⌨️ Input validation (empty messages handled safely)
🚪 Exit commands (exit, quit, bye)
🧩 Modular code using multiple classes
🪟 Graphical interface version (Windows Forms upgrade included)
🧠 Cybersecurity Topics Covered

The chatbot provides information on:

Password safety and management
Phishing attacks and detection
Malware and viruses
Online scams and fraud prevention
Safe browsing practices
Public Wi-Fi risks
Privacy and personal data protection
Two-factor authentication (2FA)
💡 Example User Questions

Users can ask:

What is phishing?
How do I create a strong password?
Is public Wi-Fi safe?
What is malware?
How do I avoid scams?
What is cybersecurity?
Tell me about privacy
Help
🧰 Technologies Used
C#
.NET (Console & Windows Forms)
Visual Studio
System.Media (Audio playback)
Object-Oriented Programming (OOP)
GitHub (Version Control)
GitHub Actions (CI Pipeline)
📂 Project Structure
PROG6221POE
│
├── Program.cs              → Application entry point
├── Form1.cs                → Windows Forms chatbot UI (latest version)
├── ChatbotEngine.cs        → Core chatbot logic and intelligence
├── AudioPlayer.cs          → Handles WAV audio playback
├── AsciiArt.cs             → ASCII banner display
├── greeting.wav            → Voice greeting audio file
└── README.md               → Project documentation
🔊 Audio Feature

The application includes a WAV voice greeting that plays when the user connects or starts the application.

Requirements:
greeting.wav must be placed in the project output folder

File must be set to:

Copy to Output Directory → Copy always

This enhances user engagement and makes the chatbot more interactive.

🖥️ How to Run the Program
Open the project in Visual Studio
Ensure greeting.wav is included in the project
Set file properties to Copy Always
Build the solution
Run the application
Enter your name
Click CONNECT
Start chatting with the bot
🚪 Exit Commands

To close the chatbot, type:

exit
quit
bye
🎨 ASCII Art Source

ASCII art was generated using:

👉 https://patorjk.com/software/taag/

It was customised to create a cybersecurity-themed header.

⚙️ Input Validation

The system handles invalid inputs such as:

Empty name input
Empty messages
Unknown questions

The bot responds politely and prompts the user to try again.

🔄 Continuous Integration (CI)

GitHub Actions is used to automatically build and test the project whenever changes are pushed to the repository.

📎 GitHub Repository

https://github.com/LATHANE-KAMOGELO/Cyber-Security-Awareness-Bot

🎥 YouTube Presentation

https://youtu.be/fDMrueMsURI

📌 Conclusion

This project demonstrates the development of a cybersecurity awareness chatbot using C#. It applies programming concepts such as:

Input handling
Conditional logic
Object-oriented programming
Modular design
User interaction systems
