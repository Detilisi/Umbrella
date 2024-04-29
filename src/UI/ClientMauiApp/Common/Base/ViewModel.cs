using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace MauiClientApp.Common.Base;

public abstract partial class ViewModel : ObservableObject
{
    //ViewModel lifecylce
    public virtual void OnViewModelStarting(CancellationToken cancellationToken = default)
    {
        Debug.WriteLine($"{nameof(ViewModel)} is starting");
    }
    public virtual void OnViewModelClosing(CancellationToken cancellationToken = default)
    {
        Debug.WriteLine($"{nameof(ViewModel)} is closing");
    }
}