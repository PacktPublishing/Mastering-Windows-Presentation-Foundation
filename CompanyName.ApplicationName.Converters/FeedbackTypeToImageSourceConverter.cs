using CompanyName.ApplicationName.DataModels.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CompanyName.ApplicationName.Converters
{
    /// <summary>
    /// Converts a FeedbackType enumeration into a .png ImageSource object.
    /// </summary>
    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class FeedbackTypeToImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// Converts a FeedbackType enumeration into a .png, ImageSource object.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>An Image object. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(FeedbackType) || targetType != typeof(ImageSource)) return null;
            string imageName = string.Empty;
            FeedbackType feedbackType = (FeedbackType)value;
            switch (feedbackType)
            {
                case FeedbackType.None: return null;
                case FeedbackType.Error: imageName = "Error_16"; break;
                case FeedbackType.Success: imageName = "Success_16"; break;
                case FeedbackType.Validation: imageName = "Warning_16"; break;
                case FeedbackType.Warning: imageName = "Warning_16"; break;
                case FeedbackType.Information: imageName = "Information_16"; break;
                case FeedbackType.Question: imageName = "Question_16"; break;
                default: return null;
            }
            return string.Format("pack://application:,,,/CompanyName.ApplicationName;component/Images/{0}.png", imageName);
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>null.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}