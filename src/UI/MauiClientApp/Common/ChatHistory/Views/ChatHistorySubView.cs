using MauiClientApp.Common.ChatHistory.Templates;
using EmailViewModel = MauiClientApp.Email.Base.ViewModels.EmailViewModel;

namespace MauiClientApp.Common.ChatHistory.Views;

internal class ChatHistorySubView : Frame
{
    //Fields
    private enum Row { Top = 0, Bottom = 1 }

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
            ItemsSource = EmailViewModel.ChatHistory,
            ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepLastItemInView,
        };

        var micIconLable = new IconLabel(FontAwesomeIcons.Microphone)
            .DynamicResource(View.StyleProperty, "ChatTemplateIcon");
        micIconLable.SetBinding(Label.IsVisibleProperty, new Binding(nameof(EmailViewModel.IsListening)));

        Content = new Grid
        {
            RowDefinitions =
            [
                new RowDefinition { Height = new GridLength(topRowSize, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(bottomRowSize, GridUnitType.Star) }
            ],
            Children =
            {
                historyCollection.Row(Row.Top),
                micIconLable.Row(Row.Bottom),
            }
        };

        this.DynamicResource(View.StyleProperty, "ChatHistoryFrame");
    }
}
