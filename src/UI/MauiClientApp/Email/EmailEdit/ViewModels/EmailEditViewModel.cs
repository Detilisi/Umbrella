using Application.Email.Features.Commands.SendEmail;
using Application.User.Abstractions.Services;
using Domain.Common.ValueObjects;
using System.Globalization;
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
    protected override void ViewAppearing()
    {
        base.ViewAppearing();

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
        var sendEmailResult = await Mediator.Send(sendCommand);
        if (sendEmailResult.IsFailure)
        {
            await SpeechService.SpeakAsync(UiStrings.DraftResponse_SendEmail_Failed);
        }
        else
        {
            await SpeechService.SpeakAsync(string.Format(UiStrings.DraftResponse_SendEmail, Recipient));
        }
        
        
        await NavigationService.NavigateToPreviousViewModelAsync();
    }

    //Handler methods
    protected override async Task ExecuteBackgroundOperation()
    {
        var token = ActivityToken.Token;

        //Introduction
        await SpeechService.SpeakAsync(UiStrings.DraftInfo_Introduction, token);
        await SpeechService.SpeakAsync(UiStrings.DraftInfo_Instructions, token);

        //Get reciepient email 
        await SpeechService.SpeakAsync(UiStrings.DraftQuery_EmailRecipient, token);
        var recipientEmailAddress = await ListenGetEmailAddress(token);
        Recipient = recipientEmailAddress;

        //Get email subject line
        await SpeechService.SpeakAsync(UiStrings.DraftQuery_EmailSubject, token);
        var emailSubjectLine = await DictateEmailSubjectOrBody(isForEmailBody: false, token);
        Subject = emailSubjectLine;

        //Get email body text
        await SpeechService.SpeakAsync(UiStrings.DraftQuery_EmailBody, token);
        var emailBodyText = await DictateEmailSubjectOrBody(isForEmailBody: true, token); 
        Body = emailBodyText;

        //Get email body text
        await SpeechService.SpeakAsync(string.Format(UiStrings.DraftInfo_EmailSummary, Recipient, Subject, Body), token);
        await SpeechService.SpeakAsync(UiStrings.DraftQuery_SendEmail, token);
        var captureResult = await CaptureUserInputAndIntentAsync();
        if (captureResult.Item2 == UserIntent.SendEmail || captureResult.Item2 == UserIntent.Yes || captureResult.Item2 == UserIntent.Ok)
        {
            await SendEmailCommand.ExecuteAsync(null);
        }
        else
        {
            await SpeechService.SpeakAsync(UiStrings.DraftInfo_EmailNotSend, token);
            await NavigationService.NavigateToPreviousViewModelAsync();
        }
    }

    //Helper methods
    protected async Task<string> ListenGetEmailAddress(CancellationToken token) // Move to an extension
    {
        bool capturedEmailAddress = false;
        while (!capturedEmailAddress && !token.IsCancellationRequested)
        {
            try
            {
                var captureResult = await CaptureUserInputAndIntentAsync(ignoreUndefinedIntent: true);
                var sanitizedString = Regex.Replace(captureResult.Item1, @"\s+", string.Empty);
                var emailInput = sanitizedString.Replace("at", "@").Replace("dot", ".");

                if (EmailAddress.IsValidEmail(emailInput))
                {
                    await SpeechService.SpeakAsync(string.Format(UiStrings.DraftQuery_Confirmation, emailInput), token);

                    captureResult = await CaptureUserInputAndIntentAsync();
                    if (captureResult.Item2 == UserIntent.Yes || captureResult.Item2 == UserIntent.Ok)
                    {
                        capturedEmailAddress = true;
                        return emailInput.ToLower();
                    }
                    await SpeechService.SpeakAsync(UiStrings.DraftResponse_EmailRecipient_Reject, token);
                }
                else
                {
                    await SpeechService.SpeakAsync(string.Format(UiStrings.DraftResponse_EmailRecipient_Invalid, captureResult.Item1), token);
                    continue;
                }
            }
            catch
            {
                await SpeechService.StopListenAsync(default);
                break;
            }
        }

        return string.Empty;
    }

    protected async Task<string> DictateEmailSubjectOrBody(bool isForEmailBody, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                var captureResult = await CaptureUserInputAndIntentAsync(ignoreUndefinedIntent: true);
                var dictatedText = captureResult.Item1.Trim();

                var textInfo = new CultureInfo("en-US", false).TextInfo;
                dictatedText = isForEmailBody ? char.ToUpper(dictatedText[0]) + dictatedText[1..] : textInfo.ToTitleCase(dictatedText);
                await SpeechService.SpeakAsync(string.Format(UiStrings.DraftQuery_Confirmation, dictatedText), token);

                captureResult = await CaptureUserInputAndIntentAsync();
                if (captureResult.Item2 == UserIntent.Yes || captureResult.Item2 == UserIntent.Ok) return dictatedText;

                await SpeechService.SpeakAsync(isForEmailBody? UiStrings.DraftResponse_EmailBody_Reject : UiStrings.DraftResponse_EmailSubject_Reject, token);
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
