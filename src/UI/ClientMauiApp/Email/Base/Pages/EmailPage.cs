namespace Umbrella.Maui.Email.Base.Pages;

public abstract class EmailPage<TViewModel> : Page<TViewModel> where TViewModel : EmailViewModel 
{
    //Fields
    private enum Row { Content = 0, ChatBox = 1 }

    //View components
    protected View ChatHistory { set; get; }
    protected abstract ScrollView PageContent { get; }
    protected Grid MainGridLayout { get; set; } = null!;

    //Construction
    protected EmailPage(string title, TViewModel viewModel, View chatHistoryView) : base(viewModel)
    {
        Title = title;
        ChatHistory = chatHistoryView;
    }

    //Initialization
    protected override void OnAppearing()
    {
        InitializeEmailPage();

        base.OnAppearing();
    }

    protected virtual void InitializeEmailPage()
    {
        InitializeMainGridLayout();

        Padding = 0;
        Content = MainGridLayout;
    }

    //View component initialization
    private void InitializeMainGridLayout()
    {
        var contentRowSize = 0.72;
        var chatBoxRowSize = 0.28;

        MainGridLayout = new Grid
        {
            RowDefinitions =
            [
                new RowDefinition { Height = new GridLength(contentRowSize, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(chatBoxRowSize, GridUnitType.Star) }
            ],
            Children =
            {
                PageContent.Row(Row.Content), 
                ChatHistory.Row(Row.ChatBox),
            }
        };
    }
}