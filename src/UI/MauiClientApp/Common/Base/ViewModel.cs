using System.Diagnostics;

namespace MauiClientApp.Common.Base;

internal abstract partial class ViewModel : ObservableObject
{
    internal static ObservableCollection<ChatHistoryModel> ChatHistory { get; private set; } = [];

    [RelayCommand]
    public virtual void OnViewModelStarting()
    {
        Debug.WriteLine($"{GetType().Name} is closing");
    }

    [RelayCommand]
    public virtual void OnViewModelClosing()
    {
        Debug.WriteLine($"{GetType().Name} is closing");
    }

    [RelayCommand]
    public virtual void OnViewModelHasFocus()
    {
        Debug.WriteLine($"{GetType().Name} has focus at {DateTime.Now}");
    }
}