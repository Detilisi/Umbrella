using MauiClientApp.Common.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace MauiClientApp.Common.Services;

internal class IntentRecognizer
{
    internal static UserIntent GetIntent(string text) 
    {
        string evalString = text.ToLower();
        if (string.IsNullOrEmpty(text)) return UserIntent.Undefined;

        var writeCommands = new List<string>
        {
            "Compose",
            "Draft",
            "Write",
            "Send",
            "Type",
            "Create",
            "Prepare",
            "Pen down",
            "Construct",
            "Craft",
            "Formulate",
            "Produce",
            "Develop",
            "Email",
            "Message",
            "Shoot off",
            "Scribble",
            "Jot down",
            "Fire off"
        };
        if (writeCommands.Any(evalString.Contains))
        {
            return UserIntent.WriteEmail;
        }

        var readEmailCommands = new List<string>
        {
            "Read",
            "Check",
            "Review",
            "Scan",
            "Skim",
            "Look over",
            "Browse",
            "Peruse",
            "Inspect",
            "Examine",
            "Glance at",
            "Go through",
            "Scroll through",
            "Study",
            "Monitor",
            "Look through",
            "Analyze",
            "Verify",
            "Observe",
            "Take a look"
        };


        return UserIntent.CancelOperation;
    }
}
