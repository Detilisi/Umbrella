namespace MauiClientApp.Common.ChatHistory.Triggers;

internal class ChatTemplateGridTriggers
{
    //Constants
    private const double BigColumn = 0.8;
    private const double SmallColumn = 0.2;

    //Data triggers
    public static DataTrigger HumanSenderTrigger => new(typeof(Grid))
    {
        Value = ChatSender.Human,
        Binding = new Binding(nameof(ChatHistoryModel.Sender)),
        Setters =
        {
            //left_colum is small, right_column is big for HUMAN
            new Setter()
            {
                Property = Grid.ColumnDefinitionsProperty,
                Value = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition { Width = new GridLength(SmallColumn, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(BigColumn, GridUnitType.Star) }
                }
            }
        }
    };

    public static DataTrigger BotSenderTrigger => new(typeof(Grid))
    {
        Value = ChatSender.Bot,
        Binding = new Binding(nameof(ChatHistoryModel.Sender)),
        Setters =
        {
            //left_colum is big, right_column is small for BOT
            new Setter()
            {
                Property = Grid.ColumnDefinitionsProperty,
                Value = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition { Width = new GridLength(BigColumn, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(SmallColumn, GridUnitType.Star) }
                }
            }
        }
    };
}
