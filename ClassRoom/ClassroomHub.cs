using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ClassRoom.Hubs
{
    public class ClassroomHub : Hub
    {
        public async Task GetCurrentStudents()
        {
            List<string> students;

            try
            {
                var json = System.IO.File.ReadAllText("Data/students.json");
                students = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            catch
            {
                students = new List<string> { };
            }

            await Clients.Caller.SendAsync("ReloadStudents", students);
        }

        public async Task UpdateCompletionStatus(int studentId, bool isCompleted)
        {
            await Clients.All.SendAsync("UpdateStatus", studentId, isCompleted);
        }

        public async Task ResetAllStatuses()
        {
            await Clients.All.SendAsync("ResetAll");
        }
    }
}
