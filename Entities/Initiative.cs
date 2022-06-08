using System.Collections.Generic;

namespace TaskManager.Entities
{
    public class Initiative
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Epic> Epics { get; set; }
        public int? CreatedById { get; set; }
        public User CreatedBy { get; set; }
    }
}
