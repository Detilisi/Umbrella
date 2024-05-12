namespace MauiClientApp.Common.Views;

public class IconView : Label
{
    //Construction
    public IconView(string fontIcon)
    {
        Text = fontIcon;
        this.DynamicResource(StyleProperty, "IconLabelStyle");
    }
}
