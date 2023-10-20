using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

using TaskBord.Model;

namespace TaskBord_v2.Converters
{
    class TaskTypeToColorConverter : IValueConverter
    {
        SolidColorBrush[] solidColorBrushes = {
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4f9ce8")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f4c650")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5dbf61")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fb9902")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fffc31")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#62b42c")),
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8305b1"))
            };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if(value is TaskType taskType) 
            {
                int index = taskType.Id-1 - taskType.Id/solidColorBrushes.Length;
                return solidColorBrushes[index];
            }
           return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4f9ce8"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
