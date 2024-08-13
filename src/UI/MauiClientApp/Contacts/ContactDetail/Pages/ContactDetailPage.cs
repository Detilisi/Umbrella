namespace MauiClientApp.Contacts.ContactDetail.Pages;

internal class ContactDetailPage : Page<ContactDetailViewModel>
{
    public ContactDetailPage(ContactDetailViewModel viewModel): base(viewModel)
    {
        Title = "Contact";

        ToolbarItems.Add(new ToolbarItem
        {
            IconImageSource = new FontImageSource
            {
                Size = 30,
                FontFamily = "FontAwesomeSolid",
                Glyph = FontAwesomeIcons.TrashCan,
            },
            //Command = new Command(async () => await ViewModel.ReplyEmailCommand.ExecuteAsync(null))
        });


        InitializeContent();
    }

    private void InitializeContent()
    {
        var nameEntry = new Entry { Placeholder = "Name:" }.DynamicResource(StyleProperty, "SignUpEntry");
        nameEntry.Bind(Entry.TextProperty, static (ContactDetailViewModel vm) =>
            vm.ContactName, static (ContactDetailViewModel vm, string text) => vm.ContactName = text);

        var emailEntry = new Entry { Placeholder = "Email:" }.DynamicResource(StyleProperty, "SignUpEntry");
        emailEntry.Bind(Entry.TextProperty, static (ContactDetailViewModel vm) =>
            vm.ContactEmail, static (ContactDetailViewModel vm, string text) => vm.ContactEmail = text);

        var saveButton = new Button()
        {
            Text = "Save",
            FontSize = 24,
            WidthRequest = 300,
            Command = ViewModel.SaveContactCommand
        };

        nameEntry.SetBinding(Entry.IsEnabledProperty, new Binding(nameof(ViewModel.IsBusy), converter: new InverseBooleanConverter()));
        emailEntry.SetBinding(Entry.IsEnabledProperty, new Binding(nameof(ViewModel.IsBusy), converter: new InverseBooleanConverter()));
        saveButton.SetBinding(Button.IsEnabledProperty, new Binding(nameof(ViewModel.IsBusy), converter: new InverseBooleanConverter()));

        Content = new VerticalStackLayout()
        {
            Spacing = 25,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                nameEntry,
                emailEntry,
                saveButton
            }
        };
    }
}
