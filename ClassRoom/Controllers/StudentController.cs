using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using ClassRoom.Hubs;
using ClassRoom.Models;

namespace ClassRoom.Controllers
{
    public class StudentController : Controller
    {
        private readonly IHubContext<ClassroomHub> _hubContext;

        public StudentController(IHubContext<ClassroomHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Manage()
        {
            var students = LoadStudents();
            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> Save(List<Student> submittedStudents)
        {
            if (submittedStudents == null)
                submittedStudents = new List<Student>();

            var existingStudents = LoadStudents();

            var updatedStudents = submittedStudents
                .Where(s => !string.IsNullOrWhiteSpace(s.Name))
                .Select(s =>
                {
                    var existing = existingStudents.FirstOrDefault(x => x.Id == s.Id);
                    return new Student
                    {
                        Id = s.Id == Guid.Empty ? Guid.NewGuid() : s.Id,
                        Name = s.Name.Trim(),
                        IsCompleted = existing?.IsCompleted ?? false
                    };
                })
                .ToList();

            var json = JsonSerializer.Serialize(updatedStudents, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("Data/students.json", json);

            await _hubContext.Clients.All.SendAsync("ReloadStudents", updatedStudents);

            return RedirectToAction("Index", "Classroom");
        }

        private List<Student> LoadStudents()
        {
            try
            {
                var json = System.IO.File.ReadAllText("Data/students.json");
                return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
            catch
            {
                return new List<Student> {
                    new Student { Name = "Alice", IsCompleted = false },
                    new Student { Name = "Bob", IsCompleted = false },
                    new Student { Name = "Charlie", IsCompleted = false }
                };
            }
        }

    }
}
