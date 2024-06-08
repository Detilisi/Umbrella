using MauiClientApp.Common.ChatHistory.Emums;

namespace MauiClientApp.Common.ChatHistory.Models;

internal class ChatHistoryModel
{
    public ChatSender Sender { get; set; }
    public string Message { get; set; } = string.Empty;
}
