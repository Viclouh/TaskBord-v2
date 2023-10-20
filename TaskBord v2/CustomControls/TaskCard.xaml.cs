using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using TaskBord.Model;

namespace TaskBord_v2.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для TaskCard.xaml
    /// </summary>
    public partial class TaskCard : UserControl
    {
        public bool IsDragDropDisabled { get; set; }

        public Task Task
        {
            get { return (Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(Task), typeof(TaskCard), new PropertyMetadata(new Task()));

        public TaskCard()
        {
            InitializeComponent();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsDragDropDisabled)
            {
                return;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(this, this, DragDropEffects.Move);
            }
        }

        private void TextBoxBlock_TextStringChanged(object sender, EventArgs e)
        {
            GlobalConstants.Context.SaveChanges();
        }
    }
}
