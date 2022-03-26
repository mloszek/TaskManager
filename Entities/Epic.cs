using System.Collections.Generic;

namespace TaskManager.Entities
{
    public class Epic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Initiative Initiative { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
