using Umbrella.Maui.Email.EmailListing.Templates;

namespace MauiClientApp.Email.EmailList.Pages;

internal class EmailListPage : EmailPage<EmailListViewModel>
{
    //Construction
    public EmailListPage(EmailListViewModel viewModel) : base(viewModel)
    {
        InitializeViewComponents();
    }

    protected override ScrollView PageContent => new()
    {
        Content = new CollectionView
        {
            SelectionMode = SelectionMode.Single,
            ItemTemplate = new EmailDataTemplate(),
            ItemsSource = ViewModel.EmailMessageList,

        }
        .Invoke(collectionView => collectionView.SelectionChanged += HandleSelectionChanged)
    };

    //Initialize
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        ViewModel.StartVMConversationCommand.Execute(null);
    }

    //View component Initialization
    private void InitializeViewComponents()
    {
        Title = "Inbox";
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
        {
            IsVisible = false,
            IsEnabled = false
        });

        var outBoxToolbarItem = new ToolbarItem
        {
            IconImageSource = new FontImageSource
            {
                Size = 30,
                FontFamily = "FontAwesomeSolid",
                Glyph = FontAwesomeIcons.PenClip
            },
            Command = new Command(async () => await ViewModel.WriteEmailCommand.ExecuteAsync(null))
        };

        ToolbarItems.Add(outBoxToolbarItem);
    }

    //Event handlers
    private async void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not EmailModel selectedEmail) return;

        await ViewModel.OpenEmailCommand.ExecuteAsync(selectedEmail);
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}
