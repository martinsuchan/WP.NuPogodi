using System;
using System.Globalization;
using System.Windows.Data;

namespace Win8.Core.Converters
{
    /// <summary>
    /// Formatter used for formatting string using CurrentCulture and specified format in Databinding.
    /// </summary>
    public class StringFormatter : IValueConverter
    {
        /// <summary>
        /// Format to be used in the converter. If null or empty, unchanged value is returned.
        /// </summary>
        public string Format { get; set; }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(Format)) return value;

            return string.Format(CultureInfo.CurrentCulture, Format, value).Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
