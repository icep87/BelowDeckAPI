using System;
using BelowDeckAPI.Models;
using BelowDeckAPI.Models.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BelowDeckAPI.Data
{
    public class DatabaseContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<PostType> PostTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>()
                .HasKey(at => new { at.PostId, at.TagId });
            modelBuilder.Entity<MenuItem>()
                .HasKey(mi => new { mi.PostId, mi.MenuId });
            base.OnModelCreating(modelBuilder);

            //Let's add default data upon inital creation
            const string roleID = "79d660d1-4e76-4e12-88a6-82751a9fd6a9";
            const string adminID = "af0f9a51-1688-4df6-8f3d-eb0af64a8ba6";

            //Add data to Roles
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = roleID,
                Name = "Admin",
                NormalizedName = "Administrator",
                Description = "Administrator with access to all functions"
            });

            var passwordHash = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminID,
                UserName = "admin",
                Name = "Administrator",
                NormalizedUserName = "admin",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com",
                EmailConfirmed = false,
                PasswordHash = passwordHash.HashPassword(null, "admin123"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                RoleId = roleID,
                UserId = adminID
            });

            //Add data to ArticleType
            modelBuilder.Entity<PostType>().HasData(new PostType
            {
                Id = 1,
                Name = "Page"
            },
            new PostType
            {
                Id = 2,
                Name = "Article"
            });

            //Add data to Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Content" },
                new Category { Id = 2, Name = "Javascript" },
                new Category { Id = 3, Name = "Software" }
            );

            //Add data to Articles
            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Title = "Home", Content = "Välkommen till min hemsida. ", Uri = "", PostTypeId = 1, CategoryId = 1, Status = 1, UserId = adminID, Update_at = DateTime.Now, Created_at = DateTime.Now },
                new Post { Id = 2, Title = "About us", Content = "Om mig", Uri = "aboutus", PostTypeId = 1, CategoryId = 1, Status = 1, UserId = adminID, Update_at = DateTime.Now, Created_at = DateTime.Now },
                new Post { Id = 3, Title = "Why “Headless CMS” Is Becoming So Popular?", Content = "Headless CMS architecture is rising in popularity in the development world. This model allows breakthrough user experiences, gives developers the great flexibility to innovate, and helps site owners future-proof their builds by allowing them to refresh the design without re-implementing the whole CMS.", Uri = "why-headless-cms-is-becoming-so-popular", PostTypeId = 2, CategoryId = 2, Status = 1, UserId = adminID, Update_at = DateTime.Now, Created_at = DateTime.Now },
                new Post { Id = 4, Title = "The JavaScript void operator", Content = "Did you know that JavaScript has a void operator just to explicitly return undefined. Its a unary operator, meaning only one operand can be used with it. You can use it like shown below — standalone or with a parenthesis.", Uri = "the-javascript-void-operator", PostTypeId = 2, CategoryId = 2, Status = 1, UserId = adminID, Update_at = DateTime.Now, Created_at = DateTime.Now }
            );

            //Add menus
            modelBuilder.Entity<Menu>().HasData(
                new Menu { Id = 1, Name = "MainMenu" }
            );

            //Add menu items
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { MenuId = 1, PostId = 1 },
                new MenuItem { MenuId = 1, PostId = 2 }
            );

            //Add Settings items
            modelBuilder.Entity<Setting>().HasData(
                new Setting { Id = 1, Group = "site", Key = "title", Value = "BetterDevelopment", Type = "string", Flags = "PUBLIC" },
                new Setting { Id = 2, Group = "site", Key = "description", Value = "Betterdevelopment is all about helping you get better.", Type = "string", Flags = "PUBLIC" },
                new Setting { Id = 3, Group = "site", Key = "url", Value = "www.betterdevelopment.com", Type = "string", Flags = "" },
                new Setting { Id = 4, Group = "site", Key = "main_navigation", Value = "1", Type = "string", Flags = "PUBLIC" },
                new Setting { Id = 5, Group = "site", Key = "footer_nagivation", Value = "1", Type = "string", Flags = "PUBLIC" },
                new Setting { Id = 6, Group = "site", Key = "logo", Value = "", Type = "string", Flags = "PUBLIC" },
                new Setting { Id = 7, Group = "site", Key = "icon", Value = "", Type = "string", Flags = "PUBLIC" },
                new Setting { Id = 8, Group = "site", Key = "facebook", Value = "", Type = "string", Flags = "PUBLIC" },
                new Setting { Id = 9, Group = "site", Key = "meta_title", Value = "", Type = "string", Flags = "PUBLIC" },
                new Setting { Id = 10, Group = "site", Key = "meta_description", Value = "", Type = "string", Flags = "PUBLIC" }

            );

        }
    }
}