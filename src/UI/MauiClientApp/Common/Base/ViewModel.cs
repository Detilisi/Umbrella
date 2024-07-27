using System.Diagnostics;

namespace MauiClientApp.Common.Base;

internal abstract partial class ViewModel : ObservableObject
{
    //Fields
    internal CancellationTokenSource ViewActiveToken { get; set; } = new();

    //Properties
    [ObservableProperty] internal bool isBusy;
    internal bool IsRootViewModel { get; set; } = false;

    //Commands
    [RelayCommand]
    protected virtual void ViewAppearing()
    {
        ViewActiveToken = new();
        Debug.WriteLine($"{GetType().Name} is closing");
    }

    [RelayCommand]
    protected virtual void ViewDisappearing()
    {
        ViewActiveToken.Cancel();
        Debug.WriteLine($"{GetType().Name} is closing");
    }

    [RelayCommand]
    protected virtual void ViewNavigatedTo()
    {
        Debug.WriteLine($"{GetType().Name} has focus at {DateTime.Now}");
    }

    [RelayCommand]
    protected virtual void ViewBackButtonPressed()
    {
        Debug.WriteLine($"{GetType().Name} back button pressed at {DateTime.Now}");
    }

    //State changers
    protected void FireViewModelBusy() => IsBusy = true;
    protected void FireViewModelNotBusy() => IsBusy = false;

    //Navigation
    protected static async Task NavigateBackAsync() => await NavigationService.NavigateToPreviousViewModelAsync();
    protected static async Task NavigateToAsync<TNextViewModel>(Dictionary<string, object>? navigationParameters = default) where TNextViewModel : ViewModel
        => await NavigationService.NavigateToViewModelAsync<TNextViewModel>(navigationParameters);

}