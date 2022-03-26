using System.Collections.Generic;

namespace TaskManager.Entities
{
    public class Initiative
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Epic> Epics { get; set; }
    }
}
