namespace MauiClientApp.Common.Enums;

internal enum UserIntent
{
    Undefined,
    CancelOperation,
    
    ReadEmails,
    OpenEmail,
    WriteEmail,
    SendEmail,

    ReplyEmail,
    DeleteEmail,

    Ok, Cancel, Yes, No
}
