namespace MauiClientApp.Contacts.ContactDetail.Pages;

internal class ContactDetailPage : Page<ContactDetailViewModel>
{
    public ContactDetailPage(ContactDetailViewModel viewModel): base(viewModel)
    {
        var nameEntry = new Entry { Placeholder = "Name:" }
            .DynamicResource(StyleProperty, "SignUpEntry");

        var emailEntry = new Entry { Placeholder = "Email:" }
            .DynamicResource(StyleProperty, "SignUpEntry");

        //Size = new Size();
        Content = new VerticalStackLayout()
        {
            Spacing = 25,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                nameEntry,
                emailEntry
            }
        };
    }
}
