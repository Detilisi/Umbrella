namespace MauiClientApp.Email.EmailDetail.Views;

public class EmailSenderView : ContentView
{
    // Enum for column indices
    private enum Column { Left = 0, Center = 1, Right = 2 }

    // Bindable properties
    public static readonly BindableProperty EmailSenderProperty =
        BindableProperty.Create(nameof(EmailSender), typeof(string), typeof(EmailSenderView), default(string));

    public static readonly BindableProperty EmailSentDateProperty =
        BindableProperty.Create(nameof(EmailSentDate), typeof(DateTime), typeof(EmailSenderView), default(DateTime));

    // Properties
    public string EmailSender
    {
        get => (string)GetValue(EmailSenderProperty);
        set => SetValue(EmailSenderProperty, value);
    }

    public DateTime EmailSentDate
    {
        get => (DateTime)GetValue(EmailSentDateProperty);
        set => SetValue(EmailSentDateProperty, value);
    }

    // Constructor
    public EmailSenderView()
    {
        const int fontSize = 30;
        const double leftColumn = 0.2;
        const double rightColumn = 0.25;
        const double centerColumn = 0.55;

        var sentDateLabel = new Label() { MaxLines = 1, FontSize = 14, FontAttributes = FontAttributes.Bold };
        var senderLabel = new Label() { MaxLines = 1, FontSize = 16, FontAttributes = FontAttributes.Bold, LineBreakMode = LineBreakMode.TailTruncation };
        
        senderLabel.SetBinding(Label.TextProperty, new Binding(nameof(EmailSender), source: this));
        sentDateLabel.SetBinding(Label.TextProperty, new Binding(nameof(EmailSentDate), source: this, stringFormat: "{0:M}"));

        Content = new Grid()
        {
            ColumnSpacing = 5,
            ColumnDefinitions =
            [
                new() { Width = new GridLength(leftColumn, GridUnitType.Star) },
                new() { Width = new GridLength(centerColumn, GridUnitType.Star) },
                new() { Width = new GridLength(rightColumn, GridUnitType.Star) },
            ],
            Children =
            {
                new IconLabel(FontAwesomeIcons.CircleUser).Column((int)Column.Left),

                new HorizontalStackLayout()
                {
                    Spacing = 10,
                    Children =
                    {
                        new IconLabel(FontAwesomeIcons.Repeat) { FontSize = fontSize },
                        new IconLabel(FontAwesomeIcons.Headphones) { FontSize = fontSize }
                    }
                }.Column((int)Column.Right),

                new VerticalStackLayout() { Spacing = 5, Children = { senderLabel, sentDateLabel }}
                .Column((int)Column.Center)
            }
        };
    }
}
