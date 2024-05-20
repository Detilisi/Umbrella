﻿namespace MauiClientApp.Email.EmailEdit.Pages;

internal class EmailEditPage : EmailPage<EmailEditViewModell>
{
    //View components
    private static Entry SenderEmailEntry = null!;
    private static Entry RecipientsEmailsEntry = null!;

    private static Entry SubjectLineEntry = null!;
    private static Editor BodyTextEditor = null!;

    //Construction
    public EmailEditPage(EmailEditViewModell viewModel) : base(viewModel)
    {
        InitializeShell();
        InitializeViewComponents();
    }
    protected override ScrollView PageContent => new()
    {
        Padding = 10,
        Content = new VerticalStackLayout()
        {
            SenderEmailEntry,
            RecipientsEmailsEntry,
            SubjectLineEntry,
            BodyTextEditor
        }
    };

    //View component Initialization
    private void InitializeShell()
    {
        Title = "Draft";
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
        {
            IsVisible = false,
            IsEnabled = false
        });

        var sendToolbarItem = new ToolbarItem
        {
            IconImageSource = new FontImageSource
            {
                Size = 30,
                FontFamily = "FontAwesomeSolid",
                Glyph = FontAwesomeIcons.PaperPlane
            }
        };

        ToolbarItems.Add(sendToolbarItem);
    }
    private void InitializeViewComponents()
    {   
        SenderEmailEntry = new()
        {
            Placeholder = "From:",
            Text = BindingContext.CurrentEditEmail.Sender,
        };

        RecipientsEmailsEntry = new()
        {
            Placeholder = "To:",
            Text = BindingContext.CurrentEditEmail.Sender,
        };

        SubjectLineEntry = new()
        {
            Placeholder = "Subject:",
            Text = BindingContext.CurrentEditEmail.Subject,
        };

        BodyTextEditor = new()
        {
            Placeholder = "Body:",
            AutoSize = EditorAutoSizeOption.TextChanges,
            Text = BindingContext.CurrentEditEmail.Body
        };

        SenderEmailEntry.DynamicResource(View.StyleProperty, "EmailEntry");
        RecipientsEmailsEntry.DynamicResource(View.StyleProperty, "EmailEntry");
        SubjectLineEntry.DynamicResource(View.StyleProperty, "EmailEntry");
        BodyTextEditor.DynamicResource(View.StyleProperty, "EmailEditor");
    }
}
