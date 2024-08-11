using Umbrella.Maui.Email.EmailListing.Templates;

namespace MauiClientApp.Email.EmailList.Pages;

internal class EmailListPage : EmailPage<EmailListViewModel>
{
    //Construction
    public EmailListPage(EmailListViewModel viewModel) : base(viewModel)
    {
        Title = "Inbox";
        ToolbarItems.Add(new ToolbarItem
        {
            IconImageSource = new FontImageSource
            {
                Size = 30,
                FontFamily = "FontAwesomeSolid",
                Glyph = FontAwesomeIcons.PenClip
            },
            Command = new Command(async () => await ViewModel.WriteEmailCommand.ExecuteAsync(null))
        });

        ToolbarItems.Add(new ToolbarItem
        {
            Text = "Contacts",
            Order = ToolbarItemOrder.Secondary,
            Command = new Command(async () => await ViewModel.ViewContactsCommand.ExecuteAsync(null))
        });
        ToolbarItems.Add(new ToolbarItem
        {
            Text = "Settings",
            Order = ToolbarItemOrder.Secondary,
        });

        Shell.SetBackButtonBehavior(this, new BackButtonBehavior(){ IsVisible = false, IsEnabled = false });
    }
    
    protected override CollectionView PageContent => new CollectionView()
    {
        SelectionMode = SelectionMode.Single,
        ItemTemplate = new EmailDataTemplate(),
        ItemsSource = ViewModel.EmailMessageList
    }.Invoke(collectionView => collectionView.SelectionChanged += HandleSelectionChanged);

    //Event handlers
    private async void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not EmailDto selectedEmail) return;

        await ViewModel.OpenEmailCommand.ExecuteAsync(selectedEmail);
    }
}
