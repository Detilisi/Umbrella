namespace MauiClientApp.Common.Enums;

internal enum UserIntent
{
    Undefined,
    CancelOperation,
    GoBack,
    
    ReadEmails,
    OpenEmail,
    WriteEmail,
    SendEmail,

    ReplyEmail,
    DeleteEmail,

    Ok, Cancel, Yes, No
}
