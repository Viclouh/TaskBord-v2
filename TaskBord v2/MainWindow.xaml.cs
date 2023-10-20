using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.EntityFrameworkCore;

using TaskBord.Model;

using TaskBord_v2.CustomControls;

using Task = TaskBord.Model.Task;

namespace TaskBord_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<TaskType> _taskTypes;
        public ObservableCollection<TaskType> TaskTypes { get => _taskTypes; set => _taskTypes = value; }

        public MainWindow()
        {
            InitializeComponent();
            SolidColorBrush[] solidColorBrushes = {
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4f9ce8")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f4c650")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5dbf61")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fb9902")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fffc31")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#62b42c")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8305b1"))
            };


            this.DataContext = this;
            FillTasks();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var item = (TaskType)(sender as Button).DataContext;

            TaskType type = TaskTypes.Where(x => x.Id == item.Id).First();

            Task newTask = new Task()
            {
                Date = DateTime.Now,
                Name = "Новая задача",
                TaskType = type,
                User = GlobalConstants.Context.Users.First()
            };
            type.Tasks.Add(newTask);
        }

        private void FillTasks()
        {
            TaskTypes = new ObservableCollection<TaskType>(GlobalConstants.Context.TaskTypes.Include(x => x.Tasks).ThenInclude(x=>x.User));
        }




        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
            var type = (TaskType)(sender as StackPanel).DataContext;

            TaskCard draggedItem = e.Data.GetData(typeof(TaskCard)) as TaskCard;

            if (draggedItem == null)
            {
                throw new Exception();
            }
            draggedItem.Task.TaskType = type;
            GlobalConstants.Context.SaveChanges();



        }

    }
}