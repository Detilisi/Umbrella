using MauiClientApp.Email.EmailList.ViewModels;
using Umbrella.Maui.Email.EmailListing.Templates;

namespace MauiClientApp.Email.EmailList.Pages;

public class EmailListPage(EmailListViewModel viewModel) : EmailPage<EmailListViewModel>(viewModel, new VerticalStackLayout())
{
    protected override ScrollView PageContent => new()
    {
        Content = new CollectionView
        {
            SelectionMode = SelectionMode.Single,
            ItemTemplate = new EmailDataTemplate(),
            ItemsSource = BindingContext.EmailMessageList,

        }
        .Invoke(collectionView => collectionView.SelectionChanged += HandleSelectionChanged)
    };

    private void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection;
        //await Shell.Current.GoToAsync(nameof(EmailDetailPage));
    }
}
