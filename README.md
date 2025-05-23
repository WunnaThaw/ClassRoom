# Classroom Homework Completion Tracker

The Classroom Homework Tracker is a real-time web application designed to help teachers and students monitor homework completion status during class. Built with ASP.NET Core and SignalR, this application allows students to mark their assignments as completed with a single click, while teachers (or all participants) can instantly see the progress and reset the status for a new task.

## Key Features

✅ **Real-Time Updates** – All connected users see button color changes instantly  
✅ **Simple UI** – Clean, intuitive interface with color-coded buttons  
✅ **No Database Required** – Status tracking happens in memory via SignalR  
✅ **Multi-Platform** – Works on any device with a web browser  
✅ **No Login Required** – Easy to use without authentication barriers

## How It Works

1. Students open the webpage and click their "Complete" button when finished.
2. The button turns green (✔️) to indicate completion.
3. Teachers (or any user) can click "Reset All" to clear all statuses for a new activity.
4. All changes appear live for everyone in the same virtual classroom.

## Technology Stack

**Backend**: ASP.NET Core (C#) with SignalR for real-time communication  
**Frontend**: HTML, CSS (Bootstrap), JavaScript

## Use Cases

🏫 **In-Person Classes** – Quickly check who has finished an in-class assignment  
💻 **Remote Learning** – Track progress during virtual lessons  
📝 **Timed Quizzes** – Monitor completion within a set time limit
