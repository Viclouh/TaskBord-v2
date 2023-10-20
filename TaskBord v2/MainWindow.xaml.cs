﻿using System;
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

            
            listStyle.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(CardList_PreviewMouseLeftButtonDown)));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(CardList_Drop)));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.GiveFeedbackEvent, new GiveFeedbackEventHandler(CardList_GiveFeedback)));

            CardListControl.ItemContainerStyle = listStyle;

            listStyle.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(CardList_PreviewMouseLeftButtonDown)));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(CardList_Drop)));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.GiveFeedbackEvent, new GiveFeedbackEventHandler(CardList_GiveFeedback)));

            CardListControl.ItemContainerStyle = listStyle;

            listStyle.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(CardList_PreviewMouseLeftButtonDown)));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(CardList_Drop)));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.GiveFeedbackEvent, new GiveFeedbackEventHandler(CardList_GiveFeedback)));

            CardListControl.ItemContainerStyle = listStyle;

            this.DataContext = this;
            FillTasks();
            
        }


      
        private readonly Style listStyle = null;

        private readonly Style listStyle = null;

        private readonly Style listStyle = null;

            var item = (TaskType)(sender as Button).DataContext;

            TaskType type = TaskTypes.Where(x => x.Id == item.Id).First();

            Task newTask = new Task()
            {
                Name = "Новая задача",
                TaskType = type,
                User = GlobalConstants.Context.Users.First()

         
        }


    
            _dragdropWindow.ShowInTaskbar = false;

            Rectangle r = new Rectangle();
            r.Width = ((FrameworkElement)dragElement).ActualWidth;
            r.Height = ((FrameworkElement)dragElement).ActualHeight;
            r.Fill = new VisualBrush(dragElement);
            this._dragdropWindow.Content = r;


            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);


            this._dragdropWindow.Left = w32Mouse.X;
            this._dragdropWindow.Top = w32Mouse.Y;
            this._dragdropWindow.Show();
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
    }
}
