namespace MauiClientApp.Contacts.ContactsList.Pages;

internal class ContactsListPage : Page<ContactsListViewModel>
{
    public ContactsListPage(ContactsListViewModel viewModel) : base(viewModel)
    {
        Content = new Grid()
        {
            Padding = 10,
            Children =
            {
                new CollectionView()
                {
                    ItemsSource = new List<string>()
                    {
                        "Item", 
                        "Item", 
                        "Item", 
                        "Item", 
                        "Item", 
                        "Item", 
                        "Item", 
                        "Item", 
                        "Item", 
                        "Item", 
                        "Item", 
                        "Item", 
                        "Item", 
                    }
                },
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
}
