using System.Collections.Generic;
using TaskManager.Entities;

namespace TaskManager.Models
{
    public class EpicDto
    {
        public string Name { get; set; }
        public Initiative Initiative { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
