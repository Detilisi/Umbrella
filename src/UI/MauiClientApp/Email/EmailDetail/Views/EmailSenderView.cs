namespace MauiClientApp.Email.EmailDetail.Views;

public class EmailSenderView : ContentView
{
    //Fields
    private enum Column { Left = 0, Center = 1, Right = 2 }

    //Construction
    public EmailSenderView(string emailSender, DateTime emailSentDate)
    {
        const int fontSize = 30;
        const double leftColumn = 0.2;
        const double rightColumn = 0.25;
        const double centerColumn = 0.55;

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
                new IconLabel(FontAwesomeIcons.CircleUser).Column(Column.Left),
                new HorizontalStackLayout(){ Spacing = 10,
                    Children = 
                    {
                        new IconLabel(FontAwesomeIcons.Repeat) { FontSize = fontSize },
                        new IconLabel(FontAwesomeIcons.Headphones) { FontSize = fontSize}
                    }
                }.Column(Column.Right),

                new VerticalStackLayout()
                {
                    Spacing = 5,
                    Children =
                    {
                        new Label(){ MaxLines = 1, FontSize = 16, Text = emailSender, FontAttributes = FontAttributes.Bold, LineBreakMode = LineBreakMode.TailTruncation},
                        new Label(){ MaxLines = 1, FontSize = 14, Text = emailSentDate.ToString("M"), FontAttributes = FontAttributes.Bold }
                    }
                }.Column(Column.Center)
            }
        };
    }
}
