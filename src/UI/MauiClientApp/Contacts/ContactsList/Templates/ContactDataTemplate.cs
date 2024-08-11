namespace MauiClientApp.Contacts.ContactsList.Templates;

internal class ContactDataTemplate : DataTemplate
{
    //Fields
    private enum Column { Left = 0, Right = 1 }

    //Construction
    public ContactDataTemplate() : base(CreateTemplate) { }

    private static Grid CreateTemplate()
    {
        //Setup 
        const double leftColumn = 0.2;
        const double rightColumn = 0.8;

        return new Grid()
        {
            ColumnSpacing = 5,
            Padding = new Thickness(10),
            ColumnDefinitions =
            [
                new() { Width = new GridLength(leftColumn, GridUnitType.Star) },
                new() { Width = new GridLength(rightColumn, GridUnitType.Star) }
            ],
            Children =
            {
                new IconLabel(FontAwesomeIcons.AddressCard)
                {
                    FontSize = 28
                }.Column(Column.Left),
                new Label()
                {
                    MaxLines = 1,
                    FontSize = 28,
                    LineBreakMode = LineBreakMode.TailTruncation
                }
                .Column(Column.Right)
                .Bind(Label.TextProperty, static (ContactDto contact) => contact.Name, mode: BindingMode.OneWay)
            }
        }; 
    }

}
