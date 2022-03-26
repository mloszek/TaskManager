using TaskManager.Entities;

namespace TaskManager.Models
{
    public class TaskDto
    {
        public string Signature { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Epic Epic { get; set; }
        public Status Status { get; set; }
    }
}
