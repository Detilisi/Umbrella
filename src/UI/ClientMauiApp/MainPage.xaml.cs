using Application.User.Features.Commands.RegisterUser;
using MediatR;

namespace ClientMauiApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        IMediator _mediator;

        public MainPage(IMediator mediator)
        {
            _mediator = mediator;
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            var registerUser = new RegisterUserCommand()
            {
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                EmailAddress = "test@test.com",
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

}
