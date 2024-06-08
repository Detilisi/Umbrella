using MauiClientApp.Common.ChatHistory.Emums;

namespace MauiClientApp.Common.ChatHistory.Models;

internal class ChatHistoryModel
{
    public required ChatSender Sender { get; set; }
    public required string Message { get; set; }
}
