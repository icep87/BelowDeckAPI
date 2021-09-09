using System;
namespace BelowDeckAPI.Models.Persistence
{
    public class Setting
    {
        public int Id { get; set; }
        //Group defines where the settings belong for example: site, admin, members
        public string Group { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        //Type defines what kind of value is stored for example: boolean, array, json, number, string
        public string Type { get; set; }
        //Example of flags is PUBLIC
        public string Flags { get; set; }
    }
}
