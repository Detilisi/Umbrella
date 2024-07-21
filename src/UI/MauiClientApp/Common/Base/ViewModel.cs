using System.Diagnostics;

namespace MauiClientApp.Common.Base;

internal abstract partial class ViewModel : ObservableObject
{
    [ObservableProperty] internal bool isBusy;

    public virtual void OnViewModelStarting()
    {
        Debug.WriteLine($"{GetType().Name} is closing");
    }

    public virtual void OnViewModelClosing()
    {
        Debug.WriteLine($"{GetType().Name} is closing");
    }

    public virtual void OnViewModelHasFocus()
    {
        Debug.WriteLine($"{GetType().Name} has focus at {DateTime.Now}");
    }

    //Helper methods
    public void FireViewModelBusy()
    {
        IsBusy = true;
    }
    public void FireViewModelNotBusy()
    {
        IsBusy = false;
    }
}