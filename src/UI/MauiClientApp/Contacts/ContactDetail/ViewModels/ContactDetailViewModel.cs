namespace MauiClientApp.Contacts.ContactDetail.ViewModels;

internal partial class ContactDetailViewModel(IMediator mediator) : ViewModel(mediator, false)
{
    //Properties
    private ContactDto SelectedContact { get; set; } = null!;

    //View elements
    [ObservableProperty] private string contactName = string.Empty;
    [ObservableProperty] private string contactEmail = string.Empty;
    
    //Navigation
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        SelectedContact = (ContactDto)query[nameof(ContactDto)];
        
        ContactName = SelectedContact.Name;
        ContactEmail = SelectedContact.EmailAddress;
    }

    //Commands
    [RelayCommand]
    public async Task SaveContact()
    {
        //var command = CreateCont
    }
}
