using MauiClientApp.Common.ChatHistory.Models;
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
        
        //Initialize user icon
        var userIcon = new IconLabel(string.Empty)
        {
            Triggers =
            {
                ChatTemplateIconTriggers.HumanSenderTrigger,
                ChatTemplateIconTriggers.HumanIconTrigger,
                ChatTemplateIconTriggers.BotSenderTrigger,
                ChatTemplateIconTriggers.BotIconTrigger
            }
        }.DynamicResource(View.StyleProperty, "ChatTemplateIcon");

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
