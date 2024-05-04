using Application.Email.Features.Queries.GetEmailList;

namespace MauiClientApp.Email.EmailList.ViewModels;

public class EmailListViewModel(IMediator mediator) : EmailViewModel(default)
{
    //Fields
    private readonly IMediator _mediator = mediator;

    //Properties
    public ObservableCollection<EmailModel> EmailMessageList { get; set; } = [];

    //Life cycle
    public override async void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        //load emails
        await LoadEmailsAsync(token);
    }

    //Load methods
    private async Task LoadEmailsAsync(CancellationToken token)
    {
        var userId = 1;
        var loadEmailQuery = new GetEmailListQuery(userId);

        var emailList = await _mediator.Send(loadEmailQuery, token);
        if (emailList.IsFailure) return;

        foreach (var emailModel in emailList.Value)
        {
            EmailMessageList.Add(emailModel);
        }
    }
}

