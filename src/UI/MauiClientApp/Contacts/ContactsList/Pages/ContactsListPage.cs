using MauiClientApp.Contacts.ContactsList.Templates;

namespace MauiClientApp.Contacts.ContactsList.Pages;

internal class ContactsListPage : Page<ContactsListViewModel>
{
    //Construction
    public ContactsListPage(ContactsListViewModel viewModel) : base(viewModel)
    {
        Title = "Contacts"; ToolbarItems.Add(new ToolbarItem
        {
            IconImageSource = new FontImageSource
            {
                Size = 30,
                FontFamily = "FontAwesomeSolid",
                Glyph = FontAwesomeIcons.CirclePlus
            },
            Command = new Command(async () => await ViewModel.CreateContactCommand.ExecuteAsync(null))
        });


        Content = new CollectionView()
        {
            Margin = new Thickness(10),
            SelectionMode = SelectionMode.Single,
            ItemTemplate = new ContactDataTemplate(),
            ItemsSource = ViewModel.ContactList
        }.Invoke(collectionView => collectionView.SelectionChanged += HandleSelectionChanged);
    }

    //Event handlers
    private async void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not ContactDto selectedContact) return;

        await ViewModel.SelectContactCommand.ExecuteAsync(selectedContact);
    }
}
