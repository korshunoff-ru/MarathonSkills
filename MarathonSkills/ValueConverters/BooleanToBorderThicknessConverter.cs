using System;
using System.Globalization;

namespace MarathonSkills
{
    /// <summary>
    /// A converter that takes in a boolean and returns a thickness of 2 if true, useful for applying 
    /// border radius on a true value
    /// </summary>
    public class BooleanToBorderThicknessConverter : BaseValueConverter<BooleanToBorderThicknessConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (bool)value ? 2 : 0;
            else
                return (bool)value ? 0 : 2;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeToEndConverter : BaseValueConverter<TimeToEndConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan timeToEnd = (TimeSpan)value;

            if (timeToEnd.Milliseconds > 0)
                return String.Format("{0} дней {1} часов {2} минут {3} секунд до старта марафона!", timeToEnd.Days, timeToEnd.Hours, timeToEnd.Minutes, timeToEnd.Seconds);
            else
                return "Марафон уже начался!";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
