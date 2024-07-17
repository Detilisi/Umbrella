﻿using Application.Email.Features.Commands.SendEmail;
using Application.User.Abstractions.Services;
using Domain.Common.ValueObjects;
using System.Text.RegularExpressions;

namespace MauiClientApp.Email.EmailEdit.ViewModels;

internal partial class EmailEditViewModel(IMediator mediator, IUserSessionService userSessionService) : EmailViewModel(mediator)
{
    //Fields
    private readonly IUserSessionService _userSessionService = userSessionService;

    //View elements
    [ObservableProperty] private string sender = string.Empty;
    [ObservableProperty] private string recipient = string.Empty;
    [ObservableProperty] private string subject = string.Empty;
    [ObservableProperty] private string body = string.Empty;

    //Life cycle 
    public override void OnViewModelStarting()
    {
        base.OnViewModelStarting();

        var currentUserResult = _userSessionService.GetCurrentSession();
        if (currentUserResult.IsFailure) return; //Handle error

        Sender = currentUserResult.Value.EmailAddress;
    }

    //Navigation
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var currentUserResult = _userSessionService.GetCurrentSession();
        if (currentUserResult.IsFailure) return; //Handle error
        Sender = currentUserResult.Value.EmailAddress;

        var selectedEmail = (EmailModel)query[nameof(EmailModel)];
        Recipient = selectedEmail.Recipient;
        Subject = $"RE: {selectedEmail.Subject}";
    }

    //Commands
    [RelayCommand]
    public async Task SendEmail()
    {
        var emailDraft = new EmailModel(){
            Sender = Sender,
            SenderName = Sender,
            Recipient = Recipient,
            Subject = Subject,
            Body = Body,
        };
        var sendCommand = new SendEmailCommand(emailDraft);
        var sendEmailResult = await _mediator.Send(sendCommand);
        if (sendEmailResult.IsFailure) return; // handle error

        await NavigationService.NavigateToViewModelAsync<EmailListViewModel>();
    }

    //Handler methods
    public override async Task HandleUserInteractionAsync()
    {
        var token = _cancellationTokenSource.Token;

        //Introduction
        await SpeechService.SpeakAsync(UiStrings.DraftInfo_Introduction, token);
        await SpeechService.SpeakAsync(UiStrings.DraftInfo_Instructions, token);

        //Get reciepient email 
        await SpeechService.SpeakAsync(UiStrings.DraftQuery_EmailRecipient, token);
        var recipientEmailAddress = await ListenGetEmailAddress(token);
        Recipient = recipientEmailAddress;

        //Get email subject line
        await SpeechService.SpeakAsync(UiStrings.DraftQuery_EmailSubject, token);
        var emailSubjectLine = await ListenGetEmailSubjectLine(token);
        Subject = emailSubjectLine;

        //Get email body text
        await SpeechService.SpeakAsync(UiStrings.DraftQuery_EmailBody, token);
        var emailBodyText = await ListenGetEmailSubjectLine(token); 
        Body = emailBodyText;

        //Get email body text
        await SpeechService.SpeakAsync(string.Format(UiStrings.DraftInfo_EmailSummary, Recipient, Subject, Body), token);
        await SpeechService.SpeakAsync(UiStrings.DraftQuery_SendEmail, token);
        var userIntent = await ListenForUserIntent();
        if (userIntent == UserIntent.Yes || userIntent == UserIntent.Ok)
        {
            await SendEmailCommand.ExecuteAsync(null);
            await SpeechService.SpeakAsync(string.Format(UiStrings.DraftResponse_SendEmail, Recipient), token);
            //Go back
        }
    }

    //Helper methods
    protected async Task<string> ListenGetEmailAddress(CancellationToken token) // Move to an extension
    {
        var userInputFailCount = 0;
        while (!token.IsCancellationRequested)
        {
            try
            {
                if (userInputFailCount == 4)
                {
                    OnViewModelClosing(); // Close app
                    break;
                }

                var userInput = await SpeechService.ListenAsync(token);
                if (userInput.IsFailure)
                {
                    userInputFailCount++;
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_Invalid, token);
                    continue;
                }

                // Assuming we have a method to validate email addresses
                string sanitizedString = Regex.Replace(userInput.Value, @"\s+", string.Empty);
                var emailInput = sanitizedString.Replace("at", "@").Replace("dot", ".");
                if (EmailAddress.IsValidEmail(emailInput))
                {
                    await SpeechService.SpeakAsync(string.Format(UiStrings.DraftQuery_EmailRecipient_Confirmation, emailInput), token);
                    
                    var userIntent = await ListenForUserIntent();
                    if (userIntent == UserIntent.Yes || userIntent == UserIntent.Ok)
                    {
                        return emailInput;
                    }

                    await SpeechService.SpeakAsync(UiStrings.DraftResponse_EmailRecipient_Reject, token);
                }
                else
                {
                    userInputFailCount++;
                    await SpeechService.SpeakAsync(string.Format(UiStrings.DraftResponse_EmailRecipient_Invalid, userInput.Value), token);
                    continue;
                }
            }
            catch (OperationCanceledException)
            {
                await SpeechService.StopListenAsync(default);
                break;
            }
        }

        return string.Empty;
    }

    protected async Task<string> ListenGetEmailSubjectLine(CancellationToken token)
    {
        var userInputFailCount = 0;

        while (!token.IsCancellationRequested)
        {
            try
            {
                if (userInputFailCount == 4)
                {
                    OnViewModelClosing(); // Close app
                    break;
                }

                var userInput = await SpeechService.ListenAsync(token);
                if (userInput.IsFailure)
                {
                    userInputFailCount++;
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_Invalid, token);
                    continue;
                }

                var subjectLine = userInput.Value.Trim();
                await SpeechService.SpeakAsync(string.Format(UiStrings.DraftQuery_EmailSubject_Confirmation, subjectLine), token);

                var userIntent = await ListenForUserIntent();
                if (userIntent == UserIntent.Yes || userIntent == UserIntent.Ok) return subjectLine;

                await SpeechService.SpeakAsync(UiStrings.DraftResponse_EmailSubject_Reject, token);
            }
            catch (OperationCanceledException)
            {
                await SpeechService.StopListenAsync(default);
                break;
            }
        }

        return string.Empty;
    }
}
