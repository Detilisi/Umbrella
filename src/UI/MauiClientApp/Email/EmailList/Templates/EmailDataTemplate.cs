using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Layouts;
using System.Globalization;

namespace Umbrella.Maui.Email.EmailListing.Templates;

public class EmailDataTemplate : DataTemplate
{
    //Fields
    private enum Column { Left = 0, Center = 1, Right = 2 }

    //View components
    private static Label EmailIcon = null!;
    private static Grid ContentGrid = null!;
    private static Label EmailTimeLabel = null!;
    private static Label EmailSenderLabel = null!;
    private static Label EmailSubjectLabel = null!;
    private static BoxView SeparatorBoxView = null!;
    private static VerticalStackLayout EmailDetailsLayout = null!;

    //Construction
    public EmailDataTemplate() : base(CreateTemplate)
    {

    }

    //Initialization
    private static DockLayout? CreateTemplate()
    {
        InitializeEmailIcon();
        InitializeSeparatorBoxView();
        InitializeEmailLabels();
        InitializeEmailTimeLabel();
        InitializeEmailDetailsLayout();
        InitializeContentGrid();

        return new()
        {
            Padding = new Thickness(10),
            Children =
            {
                ContentGrid,
                SeparatorBoxView
            }
        };
    }

    //View component Initialization 
    private static void InitializeEmailIcon()
    {
        EmailIcon = new()
        {
            Text = FontAwesomeIcons.Envelope,
            HorizontalOptions = LayoutOptions.Center
        };

        EmailIcon.DynamicResource(View.StyleProperty, "IconLabelStyle");
    }
    private static void InitializeSeparatorBoxView()
    {
        SeparatorBoxView = new();
        DockLayout.SetDockPosition(SeparatorBoxView, DockPosition.Bottom);
        SeparatorBoxView.DynamicResource(View.StyleProperty, "EmailDataTemplateSeparator");
    }
    private static void InitializeEmailTimeLabel()
    {
        var todayTrigger = new DataTrigger(typeof(Label))
        {
            Value = true,
            Binding = new Binding(nameof(EmailModel.CreatedAt))
            {
                Converter = new DateTimeToTodayConverter()
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

        var notTodayTrigger = new DataTrigger(typeof(Label))
        {
            Value = false,
            Binding = new Binding(nameof(EmailModel.CreatedAt))
            {
                Converter = new DateTimeToTodayConverter()
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
        
        EmailTimeLabel = new()
        {
            Triggers =
            {
                todayTrigger,
                notTodayTrigger
            }
        };
    }
    private static void InitializeEmailLabels()
    {
        //Setup
        EmailSubjectLabel = new()
        {
            MaxLines = 1,
            FontSize = 12,
            LineBreakMode = LineBreakMode.WordWrap
        };

        EmailSenderLabel = new()
        {
            MaxLines = 1,
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            LineBreakMode = LineBreakMode.TailTruncation,
        };

        //Databings
        EmailSubjectLabel.Bind(Label.TextProperty,
            static (EmailModel email) => email.Subject, mode: BindingMode.OneWay
        );

        EmailSenderLabel.Bind(Label.TextProperty,
            static (EmailModel email) => email.Sender, mode: BindingMode.OneWay
        );
    }
    private static void InitializeEmailDetailsLayout()
    {
        EmailDetailsLayout = new()
        {
            Spacing = 5,
            Children =
            {
                EmailSenderLabel,
                EmailSubjectLabel
            }
        };

        DockLayout.SetDockPosition(EmailDetailsLayout, DockPosition.Top);
    }
    private static void InitializeContentGrid()
    {
        var centerColumn = 0.6;
        var lateralColumn = 0.2;

        ContentGrid = new()
        {
            ColumnSpacing = 5,
            ColumnDefinitions =
            [
                new() { Width = new GridLength(lateralColumn, GridUnitType.Star) },
                new() { Width = new GridLength(centerColumn, GridUnitType.Star) },
                new() { Width = new GridLength(lateralColumn, GridUnitType.Star) },
            ],
            Children =
            {
                EmailIcon?.Column(Column.Left),
                EmailDetailsLayout?.Column(Column.Center),
                EmailTimeLabel?.Column(Column.Right),
            }
        };
    }
}

//Helpers
public class DateTimeToTodayConverter : IValueConverter
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
