using System.Collections.Generic;
using TaskManager.Entities;

namespace TaskManager.Models
{
    public class InitiativeDto
    {
        public string Name { get; set; }
        public List<Epic> Epics { get; set; }
    }
}
