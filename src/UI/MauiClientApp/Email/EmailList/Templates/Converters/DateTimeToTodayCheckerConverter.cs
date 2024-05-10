using System.Globalization;

namespace MauiClientApp.Email.EmailList.Templates.Converters;

internal class DateTimeToTodayCheckerConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is DateTime date && date.Date == DateTime.Today;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
