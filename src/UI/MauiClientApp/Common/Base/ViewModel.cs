using System.Diagnostics;

namespace MauiClientApp.Common.Base;

internal abstract partial class ViewModel : ObservableObject
{
    //ViewModel lifecylce
    public virtual void OnViewModelStarting(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is starting");
    }
    public virtual void OnViewModelClosing(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is closing");
    }
}