using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using TaskBord.Model;

namespace TaskBord_v2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //var context = GlobalConstants.Context;
            //context.TaskTypes.Add(new TaskType
            //{
            //    Name = DateTime.Now.ToShortTimeString(),
            //    Tasks = new System.Collections.ObjectModel.ObservableCollection<Task>
            //    {
            //        new Task { Description = "Test govna", Name = "Test", UserId = 1 }
            //    }
            //});
            //context.SaveChanges();
        }
    }
}
