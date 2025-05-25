using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using ClassRoom.Models;

namespace ClassRoom.Hubs
{
    public class ClassroomHub : Hub
    {
        private const string StudentFilePath = "Data/students.json";

        public async Task GetCurrentStudents()
        {
            var students = LoadStudents();
            await Clients.Caller.SendAsync("ReloadStudents", students);
        }

        public async Task UpdateCompletionStatus(string studentId, bool isCompleted)
        {
            if (!Guid.TryParse(studentId, out var guid))
                return;

            var students = LoadStudents();
            var student = students.FirstOrDefault(s => s.Id == guid);
            if (student != null)
            {
                student.IsCompleted = isCompleted;
                SaveStudents(students);
                await Clients.All.SendAsync("UpdateStatus", studentId, isCompleted);
            }
        }



        public async Task ResetAllStatuses()
        {
            var students = LoadStudents();
            foreach (var student in students)
            {
                student.IsCompleted = false;
            }
            SaveStudents(students);
            await Clients.All.SendAsync("ResetAll");
        }

        public async Task SendMessage(string sender, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", sender, message);
        }

        private List<Student> LoadStudents()
        {
            try
            {
                var json = System.IO.File.ReadAllText(StudentFilePath);
                return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
            catch
            {
                return new List<Student>();
            }
        }

        private void SaveStudents(List<Student> students)
        {
            var json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(StudentFilePath, json);
        }
    }
}
