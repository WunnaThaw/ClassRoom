using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using ClassRoom.Hubs;

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
        public async Task<IActionResult> Save(List<string> students)
        {
            students = students.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            var json = JsonSerializer.Serialize(students);
            System.IO.File.WriteAllText("Data/students.json", json);

            await _hubContext.Clients.All.SendAsync("ReloadStudents", students);
            return RedirectToAction("Index", "Classroom");
        }

        private List<string> LoadStudents()
        {
            try
            {
                var json = System.IO.File.ReadAllText("Data/students.json");
                return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            catch
            {
                return new List<string> { "Alice", "Bob", "Charlie" };
            }
        }
    }
}
