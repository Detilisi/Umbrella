using System.Diagnostics;

namespace MauiClientApp.Common.Base;

internal abstract partial class ViewModel : ObservableObject
{
    internal static ObservableCollection<ChatHistoryModel> ChatHistory { get; private set; } = [];

    public virtual void OnViewModelStarting(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is closing");
    }
    public virtual void OnViewModelClosing(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is closing");
    }

    public virtual void OnViewModelHasFocus(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} has focus at {DateTime.Now}");
    }
}