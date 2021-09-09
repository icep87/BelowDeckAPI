using System;
using System.Collections.Generic;

namespace BelowDeckAPI.Models.Persistence
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Update_at { get; set; }
        public string Uri { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public PostType PostType { get; set; }
        public int PostTypeId { get; set; }
        public int Status { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}