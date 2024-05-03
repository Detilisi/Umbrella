using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Layouts;

namespace Umbrella.Maui.Email.EmailListing.Templates;

public class EmailDataTemplate : DataTemplate
{
    //Fields
    private enum Column { Left = 0, Center = 1, Right = 2 }

    //View components
    private static Image? ImageIcon;
    private static Grid? ContentGrid;
    private static Label? EmailTimeLabel;
    private static Label? EmailSenderLabel;
    private static Label? EmailSubjectLabel;
    private static BoxView? SeparatorBoxView;
    private static VerticalStackLayout? EmailDetailsLayout;

    //Construction
    public EmailDataTemplate() : base(CreateTemplate)
    {

    }

    //Initialization
    private static DockLayout? CreateTemplate()
    {
        InitializeImageIcon();
        InitializeSeparatorBoxView();
        InitializeEmailLabels();
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
    private static void InitializeImageIcon()
    {
        ImageIcon = new()
        {
            WidthRequest = 50,
            HeightRequest = 50,
            //Source = "envelope_solid.svg"
        };
    }
    private static void InitializeSeparatorBoxView()
    {
        SeparatorBoxView = new();
        DockLayout.SetDockPosition(SeparatorBoxView, DockPosition.Bottom);
        SeparatorBoxView.DynamicResource(View.StyleProperty, "EmailDataTemplateSeparator");
    }
    private static void InitializeEmailLabels()
    {
        //Setup
        EmailTimeLabel = new();

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
        EmailTimeLabel.Bind(Label.TextProperty,
            static (EmailModel email) => email.CreatedAt, mode: BindingMode.OneWay,
            stringFormat: "{0:h:mm tt}"
        );

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
                ImageIcon?.Column(Column.Left),
                EmailDetailsLayout?.Column(Column.Center),
                EmailTimeLabel?.Column(Column.Right),
            }
        };
    }
}
