﻿namespace MauiClientApp.Email.EmailDetail.Views;

public class EmailSenderView : ContentView
{
    //Fields
    private readonly string _emailSender;
    private readonly DateTime _emailSentDate;
    private enum Column { Left = 0, Center = 1, Right = 2 }

    //View components
    private static Grid ContentGrid = null!;
    private static Label SentAtLabel = null!;
    private static Label SenderNameLabel = null!;

    private static IconLabel SenderIcon = null!;
    private static IconLabel ListenIcon = null!;
    private static IconLabel RepeatIcon = null!;

    private static VerticalStackLayout? EmailDetailsLayout;
    private static HorizontalStackLayout? EmailControlsLayout;

    //Construction
    public EmailSenderView(string emailSender, DateTime emailSentDate)
    {
        _emailSender = emailSender;
        _emailSentDate = emailSentDate;

        InitializeView();
    }

    //Initialization
    protected virtual void InitializeView()
    {
        InitializeLabels();
        InitializeEmailIcons();
        InitializeLayouts();
        InitializeContentGrid();
        
        Content = ContentGrid;
    }

    //View component Initialization
    private static void InitializeEmailIcons()
    {
        SenderIcon = new(FontAwesomeIcons.CircleUser);

        ListenIcon = new(FontAwesomeIcons.Headphones)
        {
            FontSize = 30
        };
        
        RepeatIcon = new(FontAwesomeIcons.Repeat)
        {
            FontSize = 30
        };
    }
    private void InitializeLabels()
    {
        SentAtLabel = new()
        {
            MaxLines = 1,
            FontSize = 14,
            Text = _emailSentDate.ToString("M"),
            FontAttributes = FontAttributes.Bold
        };

        SenderNameLabel = new()
        {
            MaxLines = 1,
            FontSize = 16,
            Text = _emailSender,
            FontAttributes = FontAttributes.Bold,
            LineBreakMode = LineBreakMode.TailTruncation,
        };
    }
    private static void InitializeLayouts()
    {
        EmailDetailsLayout = new()
        {
            Spacing = 5,
            Children =
            {
                SenderNameLabel,
                SentAtLabel
            }
        };
        EmailControlsLayout = new()
        {
            Spacing = 10,
            Children =
            {
                ListenIcon,
                RepeatIcon
            }
        };
    }

    private static void InitializeContentGrid()
    {
        var leftColumn = 0.2;
        var rightColumn = 0.25;
        var centerColumn = 0.55;

        ContentGrid = new()
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
                SenderIcon?.Column(Column.Left),
                EmailDetailsLayout?.Column(Column.Center),
                EmailControlsLayout?.Column(Column.Right),
            }
        };
    }
}
