using MauiClientApp.Email.EmailList.Templates.Converters;

namespace MauiClientApp.Email.EmailList.Templates.Triggers;

internal static class EmailTemplateTriggers
{
    public static DataTrigger TodayTrigger = new(typeof(Label))
    {
        Value = true,
        Binding = new Binding(nameof(EmailModel.CreatedAt))
        {
            Converter = new DateTimeToTodayCheckerConverter()
        },
        Setters =
        {
            new Setter
            {
                Property = Label.TextProperty,
                Value = new Binding(nameof(EmailModel.CreatedAt), stringFormat: "{0:h:mm tt}")
            }
        }
    };

    public static DataTrigger NotTodayTrigger = new(typeof(Label))
    {
        Value = false,
        Binding = new Binding(nameof(EmailModel.CreatedAt))
        {
            Converter = new DateTimeToTodayCheckerConverter()
        },
        Setters =
        {
            new Setter
            {
                Property = Label.TextProperty,
                Value = new Binding(nameof(EmailModel.CreatedAt), stringFormat: "{0:dd MMM}")
            }
        }
    };
}
