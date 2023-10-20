using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace TaskBord_v2.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для TextBoxBlock.xaml
    /// </summary>
    public partial class TextBoxBlock : UserControl
    {


        public string TextString
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("TextString", typeof(string), typeof(TextBoxBlock), new PropertyMetadata(""));


        public bool isFocused
        {
            get { return (bool)GetValue(isFocusedProperty); }
            set { SetValue(isFocusedProperty, value); }
        }
        public static readonly DependencyProperty isFocusedProperty =
            DependencyProperty.Register("isFocused", typeof(bool), typeof(TextBoxBlock), new PropertyMetadata(false));

        public TextBoxBlock()
        {
            InitializeComponent();
        }



        private void ContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            isFocused = true;
            NameBlock.Visibility = Visibility.Collapsed;
            NameBox.Visibility = Visibility.Visible;
            NameBox.IsEnabled = true;
            // Добавьте небольшую задержку перед установкой фокуса
            Dispatcher.BeginInvoke(new Action(() =>
            {
                NameBox.Focus();
            }));
            NameBox.Select(NameBox.Text.Length, NameBox.Text.Length);

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NameBox.Visibility = Visibility.Collapsed;
            NameBlock.Visibility = Visibility.Visible;
            isFocused = true;
        }
    }
}
