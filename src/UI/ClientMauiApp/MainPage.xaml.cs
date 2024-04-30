using Application.User.Features.Commands.RegisterUser;
using MauiClientApp.Email.EmailList.ViewModels;
using MediatR;

namespace ClientMauiApp;

public partial class MainPage : ContentPage
{
    int count = 0;
    IMediator _mediator;
    EmailListViewModel _emailListViewModel;

    public MainPage(IMediator mediator)
    {
        _mediator = mediator;
        //_emailListViewModel = emailListViewModel;
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;
        
        //_emailListViewModel.OnViewModelStarting();

        var registerUser = new RegisterUserCommand()
        {
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            EmailAddress = null,
            EmailPassword = "password",
            UserName = "username",
        };

        _mediator.Send(registerUser);   

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
