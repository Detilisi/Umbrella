namespace MauiClientApp.Common.Views;

public class IconLabel : Label
{
    //Construction
    public IconLabel(string fontIcon)
    {
        Text = fontIcon;
        this.DynamicResource(StyleProperty, "IconLabelStyle");
    }
}
