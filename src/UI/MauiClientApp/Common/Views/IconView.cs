namespace MauiClientApp.Common.Controls;

public class IconView : Label
{
    //Construction
    public IconView(string fontIcon)
    {
        Text = fontIcon;
        this.DynamicResource(View.StyleProperty, "IconLabelStyle");
    }
}
