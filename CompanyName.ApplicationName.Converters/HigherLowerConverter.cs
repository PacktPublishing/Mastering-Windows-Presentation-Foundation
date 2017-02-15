using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts the two integer values of the input into a string that represents an upward or downward arrow, depending if the current value is higher or lower than the previous value.
    /// </summary>
    public class HigherLowerConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts the string representation of the input value into the first letter of that string representation.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A string that represents an upward or downward arrow, depending if the current value is higher or lower than the previous value.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2 || values[0] == null || values[1] == null || values[0].GetType() != typeof(int) || values[1].GetType() != typeof(int)) return DependencyProperty.UnsetValue;
            int intValue = (int)values[0];
            int previousValue = (int)values[1];
            return intValue > previousValue ? "->" : "<-";
        }

        /// <summary>
        /// returns DependencyProperty.UnsetValue.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>DependencyProperty.UnsetValue.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[2] { DependencyProperty.UnsetValue, DependencyProperty.UnsetValue };
        }
    }
}