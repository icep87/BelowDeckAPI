using System;
using System.Collections.Generic;

namespace BelowDeckAPI.Models.Persistence
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
