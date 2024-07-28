namespace MauiClientApp.Common.Base;

internal abstract partial class ViewModel : ObservableObject
{
    //Fields
    internal CancellationTokenSource ActivityToken { get; set; } = new();

    //Properties
    [ObservableProperty] internal bool isBusy;
    internal bool IsRootViewModel { get; set; } = false;

    //Commands
    [RelayCommand] protected virtual void ViewAppearing() => ActivityToken = new();
    [RelayCommand] protected virtual void ViewDisappearing() => ActivityToken?.Cancel();
    
    [RelayCommand] protected virtual void ViewNavigatedTo() { }
    [RelayCommand] protected virtual void ViewBackButtonPressed() { }

    //State changers
    protected void FireViewModelBusy() => IsBusy = true;
    protected void FireViewModelNotBusy() => IsBusy = false;

    //Navigation
    protected static async Task NavigateBackAsync() => await NavigationService.NavigateToPreviousViewModelAsync();
    protected static async Task NavigateToAsync<TNextViewModel>(Dictionary<string, object>? navigationParameters = default) where TNextViewModel : ViewModel
        => await NavigationService.NavigateToViewModelAsync<TNextViewModel>(navigationParameters);
}