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
            VerticalOptions = LayoutOptions.Center,
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
        Title = "Let's get started!";
        EmailEntry = new Entry { Placeholder = "Email:" };
        EmailEntry.DynamicResource(StyleProperty, "SignUpEntry");
        EmailEntry.Bind(Entry.TextProperty, static (SignUpViewModel vm) => vm.UserEmail,
            static (SignUpViewModel vm, string text) => vm.UserEmail = text);

        PasswordEntry = new Entry
        {
            IsPassword = true,
            Placeholder = "App Password:"
        };
        PasswordEntry.DynamicResource(StyleProperty, "SignUpEntry");
        PasswordEntry.Bind(Entry.TextProperty, static (SignUpViewModel vm) => vm.UserPassword,
            static (SignUpViewModel vm, string text) => vm.UserPassword = text);

        SignUpButton = new Button()
        {
            Text = "Next",
            FontSize = 24,
            WidthRequest = 300,
            Command = ViewModel.RegisterUserCommand
        };

        EmailEntry.SetBinding(Entry.IsEnabledProperty, new Binding(nameof(ViewModel.IsBusy), converter: new InverseBooleanConverter()));
        PasswordEntry.SetBinding(Entry.IsEnabledProperty, new Binding(nameof(ViewModel.IsBusy), converter: new InverseBooleanConverter()));
        SignUpButton.SetBinding(Button.IsEnabledProperty, new Binding(nameof(ViewModel.IsBusy), converter: new InverseBooleanConverter()));
    }
}

// Converter to invert the boolean value
public class InverseBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool boolValue) return !boolValue;
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool boolValue) return !boolValue;
        return value;
    }
}