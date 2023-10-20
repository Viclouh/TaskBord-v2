using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.EntityFrameworkCore;
using TaskBord.Model;

using Task = TaskBord.Model.Task;

namespace TaskBord_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<IGrouping<TaskType,TaskBord.Model.Task>> GroupedTasks { get => new ObservableCollection<IGrouping<TaskType, TaskBord.Model.Task>>(Tasks.GroupBy(x => x.TaskType)); }
        private ObservableCollection<Task> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            FillTasks();
            ///Tasks = new ObservableCollection<IGrouping<TaskType, TaskBord.Model.Task>>(task.GroupBy(x => x.TaskType));



            //List<Task> backlogs =  GlobalConstants.Context.Task.Include(x => x.User).Where(p=>p.TaskTypeId == 1).ToList();
            //Backlog.ItemsSource = backlogs;
            //List<Task>progress  = GlobalConstants.Context.Task.Include(x => x.User).Where(p => p.TaskTypeId == 2).ToList();
            //Progress.ItemsSource = progress;
            //List<Task> done = GlobalConstants.Context.Task.Include(x => x.User).Where(p => p.TaskTypeId == 3).ToList();
            //Done.ItemsSource = done;

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var list = (IGrouping<TaskType, TaskBord.Model.Task>)(sender as Button).DataContext;
            Task newTask = new Task()
            {
                Name = "Новая задача",
                TaskType = list.Key,
                User = GlobalConstants.Context.Users.First()

            };
            Tasks.Add(newTask);
            GlobalConstants.Context.Task.Add(newTask);
            GlobalConstants.Context.SaveChanges();
            //foreach (var item in GroupedTasks)
            //{
            //    if (item.Key.Id == list.Key.Id)
            //    {
            //        item.Append(newTask);
            //    }
            //}
            FillTasks();

            Types.ItemsSource = null;
            Types.ItemsSource = GroupedTasks;
        }
        private void FillTasks()
        {
            List<TaskBord.Model.Task> task = GlobalConstants.Context.Task.Include(x => x.TaskType).ToList();
            Tasks = new ObservableCollection<Task>(task);
            
            //Tasks = new ObservableCollection<IGrouping<TaskType, TaskBord.Model.Task>>(task.GroupBy(x => x.TaskType));
        }
    }
}
