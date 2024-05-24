namespace MauiClientApp.Common.Services;

public static class NavigationService
{
    //Navigate
    public static async Task<Result> NavigateToViewModelAsync<TViewModel>(Dictionary<string, object> navigationParameters = default) where TViewModel : ViewModel
    {
        var pageRouteResult = GetPageRoute<TViewModel>();
        if (pageRouteResult.IsFailure)
        {
            return Result.Failure(pageRouteResult);
        }

        if (navigationParameters != null)
        {
            await Shell.Current.GoToAsync(pageRouteResult.Value + "?", navigationParameters);
        }
        else
        {
            await Shell.Current.GoToAsync(pageRouteResult.Value);
        }

        return Result.Success();
    }

    //Helpers
    private static IReadOnlyDictionary<Type, Type> ViewModelMappings => new Dictionary<Type, Type> //Create mapping viewmodel to page
    ([
        new KeyValuePair<Type, Type>(typeof(EmailSyncViewModel), typeof(EmailSyncPage)),
        new KeyValuePair<Type, Type>(typeof(EmailListViewModel), typeof(EmailListPage)),
        new KeyValuePair<Type, Type>(typeof(EmailDetailViewModel), typeof(EmailDetailPage)),
        new KeyValuePair<Type, Type>(typeof(EmailEditViewModel), typeof(EmailEditPage)),
    ]);

    private static Result<string> GetPageRoute<TViewModel>() where TViewModel : ViewModel //ViewModel to page routes
    {
        var viewModelType = typeof(TViewModel);
        if (!viewModelType.IsAssignableTo(typeof(ViewModel)))
        {
            return Result.Failure<string>(new Error("NavigationFailed", $"{nameof(viewModelType)} is invalid viewmodel type"));
        }

        if (!ViewModelMappings.TryGetValue(viewModelType, out var pageTypeMapping))
        {
            return Result.Failure<string>(new Error("NavigationFailed", $"ViewModelMapping for {viewModelType} not found"));
        }
        
        return Result.Success(pageTypeMapping.Name);
    }
}
