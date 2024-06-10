using MauiClientApp.Common.ChatHistory.Emums;
using MauiClientApp.Common.ChatHistory.Models;

namespace MauiClientApp.Common.ChatHistory.Triggers;

internal class ChatTemplateIconTriggers
{
    //Data triggers
    public static DataTrigger HumanColumnTrigger => new(typeof(Label))
    {
        Value = ChatSender.Human,
        Binding = new Binding(nameof(ChatHistoryModel.Sender)),
        Setters =
        {
            new Setter()
            {
                Property = Grid.ColumnProperty,
                Value = (int)ChatTemplateColumn.Left // Go left for human sender
            }
        }
    };
    public static DataTrigger HumanIconTrigger => new(typeof(Label))
    {
        Value = ChatSender.Human,
        Binding = new Binding(nameof(ChatHistoryModel.Sender)),
        Setters =
        {
            new Setter()
            {
                Property = Label.TextProperty,
                Value = FontAwesomeIcons.CircleUser
            }
        }
    };

    public static DataTrigger BotColumnTrigger => new(typeof(Label))
    {
        Value = ChatSender.Bot,
        Binding = new Binding(nameof(ChatHistoryModel.Sender)),
        Setters =
        {
            new Setter()
            {
                Property = Grid.ColumnProperty,
                Value = (int)ChatTemplateColumn.Right // Go right for human sender
            }
        }
    };
    public static DataTrigger BotIconTrigger => new(typeof(Label))
    {
        Value = ChatSender.Bot,
        Binding = new Binding(nameof(ChatHistoryModel.Sender)),
        Setters =
        {
            new Setter()
            {
                Property = Label.TextProperty,
                Value = FontAwesomeIcons.Robot
            }
        }
    };
}
