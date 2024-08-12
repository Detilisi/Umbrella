using Application.Contatcs.Features.Commands.SaveContact;

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
        if (string.IsNullOrEmpty(ContactName) || string.IsNullOrEmpty(ContactEmail)) return;
        FireViewModelBusy();

        var saveContactCommand = new SaveContactCommand()
        {
            Name = ContactName,
            EmailAddress = ContactEmail
        };

        var saveContactResult = await Mediator.Send(saveContactCommand);
        FireViewModelNotBusy();
        if (saveContactResult.IsFailure) return; // handle error

        await NavigateBackAsync();
    }
}
