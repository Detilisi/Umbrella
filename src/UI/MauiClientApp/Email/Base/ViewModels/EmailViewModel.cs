namespace Application.Email.Base;

internal partial class EmailViewModel(IMediator mediator) : ViewModel
{
    //Fields
    protected readonly IMediator _mediator = mediator;

    //ViewModel lifecylce
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);
    }

    public override void OnViewModelClosing(CancellationToken cancellationToken = default)
    {
        base.OnViewModelClosing(cancellationToken);
    }
}
