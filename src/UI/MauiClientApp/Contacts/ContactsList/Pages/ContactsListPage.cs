using MauiClientApp.Contacts.ContactsList.Templates;

namespace MauiClientApp.Contacts.ContactsList.Pages;

internal class ContactsListPage : Page<ContactsListViewModel>
{
    //Construction
    public ContactsListPage(ContactsListViewModel viewModel) : base(viewModel)
    {
        Title = "Contacts";
        Content = new Grid()
        {
            Padding = 10,
            Children =
            {
                new CollectionView()
                {
                    SelectionMode = SelectionMode.Single,
                    ItemTemplate = new ContactDataTemplate(),
                    ItemsSource = ViewModel.ContactList
                }.Invoke(collectionView => collectionView.SelectionChanged += HandleSelectionChanged),
                new Button()
                {
                    Text = "Add",
                    HeightRequest = 50,
                    WidthRequest = 50,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.End,
                }
            }
        };
    }

    //Event handlers
    private async void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not EmailDto selectedEmail) return;

        await ViewModel.ViewContactCommand.ExecuteAsync(selectedEmail);
    }
}
