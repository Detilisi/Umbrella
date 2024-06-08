﻿using MauiClientApp.Common.ChatHistory.Models;
using MauiClientApp.Common.ChatHistory.Triggers;

namespace MauiClientApp.Common.ChatHistory.Templates;

internal class ChatDataTemplate : DataTemplate
{

    //Construction
    public ChatDataTemplate() : base(CreateTemplateGrid)
    {

    }

    private static Grid CreateTemplateGrid()
    {
        var isDarkMode = Microsoft.Maui.Controls.Application.Current.RequestedTheme is AppTheme.Dark;

        //Initialize user icon
        var userIcon = new IconLabel(FontAwesomeIcons.CircleUser)
        {
            FontSize = 40,
            TextColor = isDarkMode? Color.FromUint(0xFF242C3B) : Color.FromUint(0xFFF0F8FF),
            Triggers =
            {
                ChatTemplateIconTriggers.HumanSenderTrigger,
                ChatTemplateIconTriggers.BotSenderTrigger
            }
        };

        //Initialize user message
        var userText = new Label()
        {
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation
        }.Bind(Label.TextProperty,
            static (ChatHistoryModel chat) => chat.Message, mode: BindingMode.OneTime
        );

        var userTextFrame = new Frame()
        {
            Content = userText,
            Triggers =
            {
                ChatTemplateFrameTriggers.HumanSenderTrigger,
                ChatTemplateFrameTriggers.BotSenderTrigger
            }
        }.DynamicResource(View.StyleProperty, "ChatTemplateContentFrame");

        return new Grid()
        {
            Padding = new Thickness(15),
            Children = { userIcon, userTextFrame },
            Triggers =
            {
                ChatTemplateGridTriggers.BotSenderTrigger,
                ChatTemplateGridTriggers.HumanSenderTrigger
            }
        };
    }
}