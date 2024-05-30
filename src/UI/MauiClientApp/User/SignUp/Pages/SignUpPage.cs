using MauiClientApp.User.SignUp.Views;

namespace MauiClientApp.User.SignUp.Pages;

internal class SignUpPage : Page<SignUpViewModel>
{
    //Construction
    public SignUpPage(SignUpViewModel viewModel) : base(viewModel)
    {
        Content = new VerticalStackLayout()
        {
            Children =
            {
                new SignUpHeaderView()
            }
        };
    }
}
