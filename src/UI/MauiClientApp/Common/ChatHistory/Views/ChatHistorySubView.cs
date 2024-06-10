﻿using MauiClientApp.Common.ChatHistory.Emums;
using MauiClientApp.Common.ChatHistory.Models;
using MauiClientApp.Common.ChatHistory.Templates;

namespace MauiClientApp.Common.ChatHistory.Views;

internal class ChatHistorySubView : Frame
{
    //Fields
    private enum Row { Top = 0, Bottom = 1 }
    private ObservableCollection<ChatHistoryModel> ChatHistory { get; set; }

    //View components
    private Grid HistoryGrid = null!;

    //Construction
    public ChatHistorySubView()
    {
        ChatHistory = 
        [
            new() { 
                Sender = ChatSender.Bot ,
                Message = "Elevate your email experince with Umbrella."
            }
        ];
        IntializeViewCompoments();

        Content = HistoryGrid;
        this.DynamicResource(View.StyleProperty, "ChatHistoryFrame");
    }

    //Initialization
    private void IntializeViewCompoments()
    {
        var topRowSize = 0.7;
        var bottomRowSize = 0.3;

        var chatHistory = new ScrollView()
        {
            Content = new CollectionView
            {
                SelectionMode = SelectionMode.None,
                ItemTemplate = new ChatDataTemplate(),
                ItemsSource = ChatHistory
            }
        };

        var actionIcon = new IconLabel(FontAwesomeIcons.UmbrellaBeach)
            .DynamicResource(View.StyleProperty, "ChatTemplateIcon");

        HistoryGrid = new Grid
        {
            RowDefinitions =
            [
                new RowDefinition { Height = new GridLength(topRowSize, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(bottomRowSize, GridUnitType.Star) }
            ],
            Children =
            {
                chatHistory.Row(Row.Top),
                actionIcon.Row(Row.Bottom),
            }
        };
    }

    //Helper methods
    public void AddToChatHistory(ChatHistoryModel message)
    {
        ChatHistory.Add(message);
    }

}
