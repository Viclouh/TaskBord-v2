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

            listStyle = new Style(typeof(ListBoxItem));
            listStyle.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(CardList_PreviewMouseLeftButtonDown)));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(CardList_Drop)));
            listStyle.Setters.Add(new EventSetter(ListBoxItem.GiveFeedbackEvent, new GiveFeedbackEventHandler(CardList_GiveFeedback)));

            CardListControl.ItemContainerStyle = listStyle;

            this.DataContext = this;
            FillTasks();
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var item = (TaskType)(sender as Button).DataContext;

            TaskType type = TaskTypes.Where(x => x.Id == item.Id).First();

            Task newTask = new Task()
            {
                Name = "Новая задача",
                TaskType = type,
                User = GlobalConstants.Context.Users.First()
            };
            type.Tasks.Add(newTask);
        }

        private void FillTasks()
        {
            TaskTypes = new ObservableCollection<TaskType>(GlobalConstants.Context.TaskTypes.Include(x => x.Tasks));
        }

        private Window _dragdropWindow = null;
        private readonly Style listStyle = null;


        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
            var type = (TaskType)(sender as StackPanel).DataContext;
            
            TaskCard draggedItem = e.Data.GetData(typeof(TaskCard)) as TaskCard;
            
            if (draggedItem == null) {
                throw new Exception();
            }
            draggedItem.Task.TaskType = type;
            GlobalConstants.Context.SaveChanges();



            //var droppedData = e.Data.GetData(typeof(TaskCard)) as TaskCard;
            //var target = (sender as StackPanel).DataContext as StackPanel;

            //int targetIndex = CardListControl.Items.IndexOf(target);

            //droppedData.Effect = null;
            //droppedData.RenderTransform = null;


            // remove the visual feedback drag and drop item
            if (this._dragdropWindow != null)
            {
                this._dragdropWindow.Close();
                this._dragdropWindow = null;
            }
        }


        protected void CardList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem)
            {
                var draggedItem = sender as ListBoxItem;
                var card = draggedItem.DataContext as TaskCard;

                card.Effect = new DropShadowEffect
                {
                    Color = new Color { A = 50, R = 0, G = 0, B = 0 },
                    Direction = 320,
                    ShadowDepth = 0,
                    Opacity = .75,
                };
                card.RenderTransform = new RotateTransform(2.0, 300, 200);

                draggedItem.IsSelected = true;

                // create the visual feedback drag and drop item
                CreateDragDropWindow(card);
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
            }
        }



        private void CreateDragDropWindow(Visual dragElement)
        {
            this._dragdropWindow = new Window();
            _dragdropWindow.WindowStyle = WindowStyle.None;
            _dragdropWindow.AllowsTransparency = true;
            _dragdropWindow.AllowDrop = false;
            _dragdropWindow.Background = null;
            _dragdropWindow.IsHitTestVisible = false;
            _dragdropWindow.SizeToContent = SizeToContent.WidthAndHeight;
            _dragdropWindow.Topmost = true;
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
