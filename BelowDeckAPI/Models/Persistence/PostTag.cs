﻿using System;
using Microsoft.EntityFrameworkCore;

namespace BelowDeckAPI.Models.Persistence
{
    public class PostTag
    {
        public Tag Tag { get; set; }
        public int TagId { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
