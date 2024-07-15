using Application.Email.Features.Commands.SendEmail;
using Application.User.Abstractions.Services;
using Domain.Common.ValueObjects;
using System.Text.RegularExpressions;

namespace MauiClientApp.Email.EmailEdit.ViewModels;

internal partial class EmailEditViewModel(IMediator mediator, IUserSessionService userSessionService) : EmailViewModel(mediator)
{
    //Fields
    private readonly IUserSessionService _userSessionService = userSessionService;

    //Properties
    [ObservableProperty]
    public EmailModel emailDraft = null!;

    //Life cycle 
    public override void OnViewModelStarting()
    {
        base.OnViewModelStarting();

        var currentUserResult = _userSessionService.GetCurrentSession();
        if (currentUserResult.IsFailure) return; //Handle error

        EmailDraft = new() 
        {
            Recipients = [],
            Body = string.Empty,
            Subject = string.Empty,
            SenderName = currentUserResult.Value.UserName,
            Sender = currentUserResult.Value.EmailAddress,
        };
    }

    //Navigation
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var currentUserResult = _userSessionService.GetCurrentSession();
        if (currentUserResult.IsFailure) return; //Handle error

        var selectedEmail = (EmailModel)query[nameof(EmailModel)];
        EmailDraft = new()
        {
            Body = string.Empty,
            Subject = $"RE: {selectedEmail.Subject}",
            SenderName = currentUserResult.Value.UserName,
            Sender = currentUserResult.Value.EmailAddress,
            Recipients = [selectedEmail.Recipients.FirstOrDefault()],
        };
    }

    //Commands
    [RelayCommand]
    public async Task SendEmail()
    {
        var sendCommand = new SendEmailCommand(EmailDraft);
        var sendEmailResult = await _mediator.Send(sendCommand);
        if (sendEmailResult.IsFailure) return; // handle error

        await NavigationService.NavigateToViewModelAsync<EmailListViewModel>();
    }

    //Handler methods
    public override async Task HandleUserInteractionAsync()
    {
        var token = _cancellationTokenSource.Token;

        //Introduction
        await SpeechService.SpeakAsync("Welcome to the email drafting page!", token);
        await SpeechService.SpeakAsync("I'll guide you through writing and sending your email. Let's get started!", token);

        //Get reciepient email 
        await SpeechService.SpeakAsync("First, who would you like to send this email to? Please say the recipient's email address.", token);
        var recipientEmailAddress = await ListenGetEmailAddress(token);

        //Get email subject line
        await SpeechService.SpeakAsync("Got it. Next, what is the subject of your email? Please state the subject line.", token);
        var emailSubjectLine = await ListenGetEmailSubjectLine(token);

        //Get email body text
        await SpeechService.SpeakAsync("Perfect. Now, let's compose the body of your email. Please dictate your message clearly.", token);
        var emailBodyText = await ListenGetEmailSubjectLine(token); // Create DictateEmailBodyAsync

        //Get email body text
        await SpeechService.SpeakAsync("Thank you! Your email is ready to be sent. Do you need to make any changes, or shall I send it now?", token);
        var userIntent = await ListenForUserIntent();
        if (userIntent == UserIntent.Yes || userIntent == UserIntent.Ok)
        {
            await SendEmailCommand.ExecuteAsync(null);
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
                    await SpeechService.SpeakAsync(string.Format("You said {0}, is that correct?", emailInput), token);
                    
                    var userIntent = await ListenForUserIntent();
                    if (userIntent == UserIntent.Yes || userIntent == UserIntent.Ok)
                    {
                        return userInput.Value;
                    }

                    await SpeechService.SpeakAsync("Ok, please try saying the email again.", token);
                }
                else
                {
                    userInputFailCount++;
                    await SpeechService.SpeakAsync(string.Format("{0} is an invalid email address, please try again", userInput.Value), token);
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
                if (!string.IsNullOrEmpty(subjectLine))
                {
                    await SpeechService.SpeakAsync($"You said: {subjectLine}. Is that correct?", token);

                    var userIntent = await ListenForUserIntent();
                    if (userIntent == UserIntent.Yes || userIntent == UserIntent.Ok)
                    {
                        return subjectLine;
                    }

                    await SpeechService.SpeakAsync("Ok, please dictate the subject line again.", token);
                }
                else
                {
                    userInputFailCount++;
                    await SpeechService.SpeakAsync("The subject line cannot be empty. Please try again.", token);
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
}
