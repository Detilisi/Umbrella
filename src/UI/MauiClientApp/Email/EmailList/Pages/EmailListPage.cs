﻿using MauiClientApp.Email.EmailDetail.Pages;
using MauiClientApp.Email.EmailList.ViewModels;
using Umbrella.Maui.Email.EmailListing.Templates;

namespace MauiClientApp.Email.EmailList.Pages;

public class EmailListPage : EmailPage<EmailListViewModel>
{
    //Construction
    public EmailListPage(EmailListViewModel viewModel) : base(viewModel)
    {
        InitializeViewComponents();
    }

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

    //View component Initialization
    private void InitializeViewComponents()
    {
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
        {
            IsVisible = false,
            IsEnabled = false
        });
    }

    //Event handlers
    private async void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection;
        await Shell.Current.GoToAsync(nameof(EmailDetailPage));
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}
