using MauiClientApp.User.SignUp.Views;

namespace MauiClientApp.User.SignUp.Pages;

internal class SignUpPage : Page<SignUpViewModel>
{
    //View components
    private Entry EmailEntry = null!;
    private Entry PasswordEntry = null!;

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
                PasswordEntry
            }
        };
    }

    private void InitializeViewComponents()
    {
        EmailEntry = new Entry { Placeholder = "Email:" };
        EmailEntry.DynamicResource(StyleProperty, "SignUpEntry");
        EmailEntry.Bind(Entry.TextProperty, static (SignUpViewModel vm) => vm.NewUser.EmailAddress,
            static (SignUpViewModel vm, string text) => vm.NewUser.EmailAddress = text);

        PasswordEntry = new Entry { Placeholder = "Password:" };
        PasswordEntry.DynamicResource(StyleProperty, "SignUpEntry");
        PasswordEntry.Bind(Entry.TextProperty, static (SignUpViewModel vm) => vm.NewUser.EmailAddress,
            static (SignUpViewModel vm, string text) => vm.NewUser.EmailAddress = text);
    }
}
