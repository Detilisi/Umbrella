using MauiClientApp.Common.ChatHistory.Views;
using EmailViewModel = MauiClientApp.Email.Base.ViewModels.EmailViewModel;

namespace Umbrella.Maui.Email.Base.Pages;

internal abstract partial class EmailPage<TViewModel> : Page<TViewModel> where TViewModel : EmailViewModel 
{
    //Fields
    private enum Row { Content = 0, ChatBox = 1 }

    //View components
    protected abstract View PageContent { get; }

    //Construction
    protected EmailPage(TViewModel viewModel) : base(viewModel) => InitializeEmailPage();

    //View initialization
    protected virtual void InitializeEmailPage()
    {
        Padding = 0;
        Content = CreateMainView;
    }
    private Grid CreateMainView
    {
        get
        {
            const double contentRowHeight = 0.72;
            const double chatBoxRowHeight = 0.28;

            var contentRowDefinition = new RowDefinition { Height = new GridLength(contentRowHeight, GridUnitType.Star) };
            var chatBoxRowDefinition = new RowDefinition { Height = new GridLength(chatBoxRowHeight, GridUnitType.Star) };

            return new Grid
            {
                RowDefinitions = [ contentRowDefinition, chatBoxRowDefinition ],
                Children = { PageContent.Row(Row.Content), new ChatHistorySubView().Row(Row.ChatBox) }
            };
        }
    }
}