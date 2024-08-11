using Application.Common.Abstractions.Services;

namespace MauiClientApp.Contacts.ContactsList.ViewModels;

internal class ContactsListViewModel(IMediator mediator, IEncryptionService encryptionService) : ViewModel(mediator, false)
{
}
