namespace MauiClientApp.Common.ChatHistory.Triggers;

internal class ChatTemplateFrameTriggers
{
    //Data triggers
    public static DataTrigger HumanSenderTrigger => new(typeof(Frame))
    {
        Value = ChatSender.Human,
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

    public static DataTrigger BotSenderTrigger => new(typeof(Frame))
    {
        Value = ChatSender.Bot,
        Binding = new Binding(nameof(ChatHistoryModel.Sender)),
        Setters =
        {
            new Setter()
            {
                Property = Grid.ColumnProperty,
                Value = (int)ChatTemplateColumn.Left // Go left for bot sender
            }
        }
    };
}
