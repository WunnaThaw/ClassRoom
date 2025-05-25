namespace ClassRoom.Models
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
