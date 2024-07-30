using System.Diagnostics;

namespace MauiClientApp.Common.Base;

/// <summary>
/// BasePage ViewModel-bound Type
/// </summary>
/// <typeparam name="TViewModel"></typeparam>
internal abstract class Page<TViewModel>(TViewModel viewModel) : Page(viewModel) where TViewModel : ViewModel
{
    //Properties
    protected TViewModel ViewModel => (TViewModel)base.BindingContext;

    //Life-cylce
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        LogOperation(nameof(this.OnBindingContextChanged));

        SetBinding(IsBusyProperty, new Binding(nameof(ViewModel.IsBusy)));
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LogOperation(nameof(this.OnAppearing));

        ViewModel.ViewAppearingCommand.Execute(this);
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        LogOperation(nameof(this.OnDisappearing));

        ViewModel.ViewDisappearingCommand.Execute(this);
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        LogOperation(nameof(this.OnNavigatedTo));

        ViewModel.ViewNavigatedToCommand.Execute(this);
    }

    protected override bool OnBackButtonPressed()
    {
        if(ViewModel.IsRootViewModel) return false;

        var result = base.OnBackButtonPressed();
        LogOperation(nameof(this.OnNavigatedTo));

        return result;
    }

    //Helper
    private void LogOperation(string functionName) => 
        Debug.WriteLine($"APP-INFO: {GetType().Name}.{functionName} at {DateTime.Now}");
}

/// <summary>
/// BasePage Content Type
/// </summary>
internal abstract class Page : ContentPage
{
    //Construction
    protected Page(object? viewModel = null)
    {
        BindingContext = viewModel;
        Padding = 12;

        if (string.IsNullOrWhiteSpace(Title))
        {
            Title = GetType().Name;
        }
    }
}