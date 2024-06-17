using MauiClientApp.Common.Enums;

namespace MauiClientApp.Common.Services;

internal class IntentRecognizer
{
    internal static SpeechCommands GetIntent(string text) 
    {
        
        return SpeechCommands.CancelOperation;
    }
}
