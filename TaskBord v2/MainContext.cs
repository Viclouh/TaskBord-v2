using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

using TaskBord.Model;

namespace TaskBord
{
    public class MainContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=192.168.221.12;database=BibaAndBoba;User ID=user05;Password=05;TrustServerCertificate=True");
            optionsBuilder.UseSqlServer(@"Data Source=hnt8.ru;database=BibaAndBoba;User ID=sa;Password=_RasulkotV2;TrustServerCertificate=True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TaskType>().HasData(
                new TaskType() {  Id = 1, Name = "backlog" ,/* Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#69f"))*/ },
                new TaskType() {  Id = 2, Name = "in progress",/* Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fc6"))*/ },
                new TaskType() {  Id = 3, Name = "done",/* Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6c6"))*/ }

                );
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Name = "user1", Password = "user1" },
                new User() { Id = 2, Name = "user2", Password = "user2" }
                );
            modelBuilder.Entity<Task>().HasData(
                new Task() { Id = 1, Name = "task1", TaskTypeId = 1, UserId = 1, Description = "desxription1", Date = DateTime.Today},
                new Task() { Id = 2, Name = "task2", TaskTypeId = 1, UserId = 1, Description = "desxription1", Date = DateTime.Today },
                new Task() { Id = 3, Name = "task3", TaskTypeId = 1, UserId = 1, Description = "desxription1", Date = DateTime.Today },
                new Task() { Id = 4, Name = "task4", TaskTypeId = 2, UserId = 2, Description = "desxription2", Date = DateTime.Today },
                new Task() { Id = 5, Name = "task5", TaskTypeId = 2, UserId = 2, Description = "desxription2" , Date = DateTime.Today },
                new Task() { Id = 6, Name = "task6", TaskTypeId = 2, UserId = 2, Description = "desxription2" , Date = DateTime.Today },
                new Task() { Id = 7, Name = "task7", TaskTypeId = 3, UserId = 1, Description = "desxription3" , Date = DateTime.Today },
                new Task() { Id = 8, Name = "task8", TaskTypeId = 3, UserId = 2, Description = "desxription3"   , Date = DateTime.Today },
                new Task() { Id = 9, Name = "task9", TaskTypeId = 3, UserId = 1, Description = "desxription3" , Date = DateTime.Today },
                new Task() { Id = 10, Name = "task10", TaskTypeId = 3, UserId = 1, Description = "desxription3" , Date = DateTime.Today }
                );

        }

        public MainContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }
}
