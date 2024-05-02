﻿using Application.Email.Features.Queries.GetEmailList;

namespace MauiClientApp.Email.EmailList.ViewModels;

public class EmailListViewModel(ViewModel childViewModel, IMediator mediator) : EmailViewModel(childViewModel)
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

