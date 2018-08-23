using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MarathonSkills
{
    /// <summary>
    /// Base class with value converter
    /// </summary>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        #region Private Members

        private static T _instance;

        #endregion

        #region Markup Extension Methods

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_instance == null)
                _instance = new T();

            return _instance;
        }

        #endregion
        
        #region Value Converter Methods

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion
    }
}
