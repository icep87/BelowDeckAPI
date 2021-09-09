using System;
namespace BelowDeckAPI.Models.Persistence
{
    public class MenuItem
    {
        public Menu Menu { get; set; }
        public int MenuId { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public int Order { get; set; }
    }
}
