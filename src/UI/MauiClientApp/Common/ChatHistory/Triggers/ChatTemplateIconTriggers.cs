using MauiClientApp.Common.ChatHistory.Emums;
using MauiClientApp.Common.ChatHistory.Models;

namespace MauiClientApp.Common.ChatHistory.Triggers;

internal class ChatTemplateIconTriggers
{
    //Data triggers
    public static DataTrigger HumanSenderTrigger => new(typeof(Image))
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

    public static DataTrigger BotSenderTrigger => new(typeof(Image))
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
}
