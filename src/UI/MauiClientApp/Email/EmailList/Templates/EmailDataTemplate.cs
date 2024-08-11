using Application.Email.Dtos;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Layouts;
using MauiClientApp.Email.EmailList.Templates.Triggers;

namespace Umbrella.Maui.Email.EmailListing.Templates;

public class EmailDataTemplate : DataTemplate
{
    //Fields
    private enum Column { Left = 0, Center = 1, Right = 2 }

    //Construction
    public EmailDataTemplate() : base(CreateTemplate) { }

    private static DockLayout CreateTemplate()
    {
        //Setup 
        const double centerColumn = 0.6;
        const double lateralColumn = 0.2;

        var separatorLine = new SeparatorLine();
        DockLayout.SetDockPosition(separatorLine, DockPosition.Bottom);

        var timeStampLabel = new Label(){ Triggers = { EmailTemplateTriggers.TodayTrigger, EmailTemplateTriggers.NotTodayTrigger } }
            .Bind(Label.TextProperty, static (EmailDto email) => email.CreatedAt, mode: BindingMode.OneWay);

        var addressLabel = new Label()
        {
            MaxLines = 1,
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            LineBreakMode = LineBreakMode.TailTruncation,
        }
        .Bind(Label.TextProperty, static (EmailDto email) => email.SenderName, mode: BindingMode.OneWay);

        var subjectLineLabel = new Label()
        {
            MaxLines = 1,
            FontSize = 12,
            LineBreakMode = LineBreakMode.WordWrap
        }
        .Bind(Label.TextProperty, static (EmailDto email) => email.Subject, mode: BindingMode.OneWay);
        
        
        return new()
        {
            Padding = new Thickness(10),
            Children =
            {
                separatorLine, //DockPosition.Bottom
                new Grid()
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
                        timeStampLabel?.Column(Column.Right),
                        new IconLabel(FontAwesomeIcons.Envelope).Column(Column.Left),
                        new VerticalStackLayout { Spacing = 5, Children = { addressLabel, subjectLineLabel } }.Column(Column.Center),
                    }
                }
            }
        };
    }
}
