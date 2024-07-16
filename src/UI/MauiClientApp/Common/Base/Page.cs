﻿using System.Diagnostics;

namespace MauiClientApp.Common.Base;

/// <summary>
/// BasePage ViewModel-bound Type
/// </summary>
/// <typeparam name="TViewModel"></typeparam>
internal abstract class Page<TViewModel>(TViewModel viewModel) : Page(viewModel) where TViewModel : ViewModel
{
    //Properties
    public new TViewModel ViewModel => (TViewModel)base.BindingContext;

    //Life-cylce
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        SetBinding(IsBusyProperty, new Binding(nameof(ViewModel.IsBusy)));
        ViewModel.OnViewModelStarting();
        Debug.WriteLine($"OnBindingContextChanged: {Title}");
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        Debug.WriteLine($"OnAppearing: {Title}");
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        ViewModel.OnViewModelClosing();
        Debug.WriteLine($"OnDisappearing: {Title}");
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        ViewModel.OnViewModelHasFocus();
        Debug.WriteLine($"OnNavigatedTo: {Title}");
    }
}

/// <summary>
/// BasePage Content Type
/// </summary>
public abstract class Page : ContentPage
{
    //Construction
    protected Page(object? viewModel = null)
    {
        BindingContext = viewModel;
        Padding = 12;

        if (string.IsNullOrWhiteSpace(Title))
        {
            Title = GetType().Name;
        }
    }
}