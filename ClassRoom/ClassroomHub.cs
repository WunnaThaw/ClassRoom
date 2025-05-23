using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ClassRoom
{
    public class ClassroomHub : Hub
    {
        // Store completion status in memory
        private static ConcurrentDictionary<int, bool> _completionStatus = new ConcurrentDictionary<int, bool>();

        // List of student names (could be made configurable)
        private static List<string> _studentNames = new List<string>
    {
        "Alice", "Bob", "Charlie", "Diana", "Eve", "Frank"
    };

        public override async Task OnConnectedAsync()
        {
            // Initialize status for all students if not already set
            for (int i = 0; i < _studentNames.Count; i++)
            {
                _completionStatus.TryAdd(i, false);
            }

            // Send current status to the newly connected client
            await Clients.Caller.SendAsync("InitializeStatuses", _completionStatus);

            await base.OnConnectedAsync();
        }

        public async Task UpdateCompletionStatus(int studentId, bool isCompleted)
        {
            _completionStatus[studentId] = isCompleted;
            await Clients.All.SendAsync("UpdateStatus", studentId, isCompleted);
        }

        public async Task ResetAllStatuses()
        {
            foreach (var key in _completionStatus.Keys.ToList())
            {
                _completionStatus[key] = false;
            }
            await Clients.All.SendAsync("ResetAll");
        }
    }
}
