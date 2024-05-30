using MauiClientApp.User.SignUp.Views;

namespace MauiClientApp.User.SignUp.Pages;

internal class SignUpPage : Page<SignUpViewModel>
{
    //View components
    private Entry EmailEntry = null!;
    private Entry PasswordEntry = null!;
    private Button SignUpButton = null!;

    //Construction
    public SignUpPage(SignUpViewModel viewModel) : base(viewModel)
    {
        InitializeViewComponents();

        Content = new VerticalStackLayout()
        {
            Spacing = 25,
            Children =
            {
                new SignUpHeaderView(),
                EmailEntry,
                PasswordEntry,
                SignUpButton
            }
        };
    }

    private void InitializeViewComponents()
    {
        EmailEntry = new Entry { Placeholder = "Email:" };
        EmailEntry.DynamicResource(StyleProperty, "SignUpEntry");
        EmailEntry.Bind(Entry.TextProperty, static (SignUpViewModel vm) => vm.UserEmail,
            static (SignUpViewModel vm, string text) => vm.UserEmail = text);

        PasswordEntry = new Entry 
        { 
            Placeholder = "Password:",
            IsPassword = true 
        };
        PasswordEntry.DynamicResource(StyleProperty, "SignUpEntry");
        PasswordEntry.Bind(Entry.TextProperty, static (SignUpViewModel vm) => vm.UserPassword,
            static (SignUpViewModel vm, string text) => vm.UserPassword = text);

        SignUpButton = new Button()
        {
            Text = "Next",
            FontSize = 24,
            WidthRequest = 300,
            Command = BindingContext.RegisterUserCommand
        };
    }
}
