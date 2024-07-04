
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions configurations) : base(configurations)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                 .HasOne(m => m.UserCreator)
                 .WithMany(m=>m.Messages)
                 .HasForeignKey(m => m.UserCreatorId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ChatCreateds)
                .WithOne(m => m.UserCreator)
                .HasForeignKey(m => m.UserCreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Users)
                .UsingEntity(j => j.ToTable("UserChats"));

            modelBuilder.Entity<User>()
              .HasIndex(u => u.Login)
              .IsUnique();
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Chat> Chats { get; set; }


    }
}
