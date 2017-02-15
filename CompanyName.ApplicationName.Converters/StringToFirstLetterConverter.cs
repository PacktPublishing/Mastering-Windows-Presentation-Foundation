using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts the string representation of the input value into the first letter of that string representation.
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class StringToFirstLetterConverter : IValueConverter
    {
        /// <summary>
        /// Converts the string representation of the input value into the first letter of that string representation.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A spaced string representation of the CamelCase name of the Enum instance specified by the vale input parameter.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;
            return value.ToString()[0];
        }

        /// <summary>
        /// returns DependencyProperty.UnsetValue.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>DependencyProperty.UnsetValue.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}