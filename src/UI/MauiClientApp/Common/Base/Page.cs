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
        Debug.WriteLine($"OnBindingContextChanged: {Title}");

        SetBinding(IsBusyProperty, new Binding(nameof(ViewModel.IsBusy)));
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Debug.WriteLine($"OnAppearing: {Title}");
        
        ViewModel.ViewAppearingCommand.Execute(this);
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Debug.WriteLine($"OnDisappearing: {Title}");

        ViewModel.ViewDisappearingCommand.Execute(this);
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Debug.WriteLine($"OnNavigatedTo: {Title}");

        ViewModel.ViewNavigatedToCommand.Execute(this);
    }

    protected override bool OnBackButtonPressed()
    {
        if(ViewModel.IsRootViewModel) return false;

        var result = base.OnBackButtonPressed();
        Debug.WriteLine($"OnBackButtonPressed: {Title}");

        return result;
    }
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