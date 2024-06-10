using MauiClientApp.Common.ChatHistory.Models;
using MauiClientApp.Common.ChatHistory.Views;

namespace Umbrella.Maui.Email.Base.Pages;

public abstract partial class EmailPage<TViewModel>(TViewModel viewModel) : 
    Page<TViewModel>(viewModel) where TViewModel : EmailViewModel 
{
    //Fields
    private enum Row { Content = 0, ChatBox = 1 }

    //View components
    protected View ChatHistory { set; get; } = null!;
    protected abstract ScrollView PageContent { get; }
    protected Grid MainGridLayout { get; set; } = null!;

    //Initialization
    protected override void OnAppearing()
    {
        InitializeEmailPage();

        base.OnAppearing();
    }

    protected virtual void InitializeEmailPage()
    {
        ChatHistory = new ChatHistorySubView();
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