using MauiClientApp.Common.ChatHistory.Views;

namespace Umbrella.Maui.Email.Base.Pages;

internal abstract partial class EmailPage<TViewModel>(TViewModel viewModel) : 
    Page<TViewModel>(viewModel) where TViewModel : EmailViewModel 
{
    //Fields
    private enum Row { Content = 0, ChatBox = 1 }

    //View components
    protected abstract ScrollView PageContent { get; }
    protected Grid MainGridLayout { get; set; } = null!;
    protected ChatHistorySubView ChatHistory { get; set; } = new(viewModel);

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