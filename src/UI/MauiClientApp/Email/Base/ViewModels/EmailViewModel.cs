﻿namespace Application.Email.Base;

public partial class EmailViewModel(ViewModel chatViewModel) : ViewModel
{
    //Fields
    public ViewModel ChildViewModel { get; private set; } = chatViewModel;

    //ViewModel lifecylce
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        //await ChildViewModel.AuthorizeMicrophoneUsageAsync(cancellationToken);
    }

    public override void OnViewModelClosing(CancellationToken cancellationToken = default)
    {
        base.OnViewModelClosing(cancellationToken);

        ChildViewModel.OnViewModelClosing(cancellationToken);
    }
}