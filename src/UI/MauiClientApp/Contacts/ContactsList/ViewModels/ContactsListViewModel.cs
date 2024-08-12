using Application.Contatcs.Features.Queries.GetContactList;

namespace MauiClientApp.Contacts.ContactsList.ViewModels;

internal partial class ContactsListViewModel(IMediator mediator) : ViewModel(mediator, false)
{
    //Properties
    public ObservableCollection<ContactDto> ContactList { get; set; } = [];

    //Life cycle 
    protected override async void ViewAppearing()
    {
        base.ViewAppearing();
        await LoadContactsAsync();
    }

    //Load methods
    private async Task LoadContactsAsync()
    {
        var contactListResult = await Mediator.Send(new GetContactListQuery());
        if (contactListResult.IsFailure) return;

        foreach (var contactDto in contactListResult.Value)
        {
            ContactList.Add(contactDto);
        }
    }

    //Commands
    [RelayCommand]
    public async Task SelectContact(ContactDto selectedContact)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            [nameof(ContactDto)] = selectedContact
        };

        //Open modal
        await NavigationService.NavigateToViewModelAsync<ContactDetailViewModel>(navigationParameter);
    }

    [RelayCommand]
    public async Task CreateContact()
    {
        //Open modal
        await NavigationService.NavigateToViewModelAsync<ContactDetailViewModel>();
    }
}
