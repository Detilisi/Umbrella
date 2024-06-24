using MauiClientApp.Common.ChatHistory.Templates;

namespace MauiClientApp.Common.ChatHistory.Views;

internal class ChatHistorySubView : Frame
{
    //Fields
    private enum Row { Top = 0, Bottom = 1 }

    //View components
    private Grid HistoryGrid = null!;

    //Construction
    public ChatHistorySubView()
    {
        IntializeViewCompoments();
    }

    //Initialization
    private void IntializeViewCompoments()
    {
        var topRowSize = 0.7;
        var bottomRowSize = 0.3;

        var historyCollection = new CollectionView
        {
            SelectionMode = SelectionMode.None,
            ItemTemplate = new ChatDataTemplate(),
            ItemsSource = ViewModel.ChatHistory,
            ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepLastItemInView,
        };

        var actionIcon = new IconLabel(FontAwesomeIcons.Microphone)
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
                historyCollection.Row(Row.Top),
                actionIcon.Row(Row.Bottom),
            }
        };

        Content = HistoryGrid;
        this.DynamicResource(View.StyleProperty, "ChatHistoryFrame");
    }
}
