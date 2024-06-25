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
    ForwardEmail,

    Ok, Cancel, Yes, No
}
