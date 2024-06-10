namespace Application.Email.Base;

public partial class EmailViewModel(IMediator mediator, ViewModel chatViewModel) : ViewModel
{
    //Fields
    protected readonly IMediator _mediator = mediator;
    public ViewModel ChildViewModel { get; private set; } = chatViewModel;

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
