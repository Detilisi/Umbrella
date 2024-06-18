using MauiClientApp.Common.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace MauiClientApp.Common.Services;

using System;
using System.Collections.Generic;
using System.Linq;

internal class IntentRecognizer
{
    private static readonly Dictionary<UserIntent, HashSet<string>> IntentCommands = new Dictionary<UserIntent, HashSet<string>>
    {
        { UserIntent.WriteEmail, new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Compose", "Draft", "Write", "Send", "Type", "Create", "Prepare", "Pen down", "Construct",
                "Craft", "Formulate", "Produce", "Develop", "Email", "Message", "Shoot off", "Scribble",
                "Jot down", "Fire off"
            }
        },
        { UserIntent.ReadEmails, new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Read", "Check", "Review", "Scan", "Skim", "Look over", "Browse", "Peruse", "Inspect",
                "Examine", "Glance at", "Go through", "Scroll through", "Study", "Monitor", "Look through",
                "Analyze", "Verify", "Observe", "Take a look"
            }
        }
    };

    internal static UserIntent GetIntent(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return UserIntent.Undefined;

        string evalString = text.ToLower();

        foreach (var intent in IntentCommands)
        {
            if (intent.Value.Any(command => evalString.Contains(command.ToLower())))
            {
                return intent.Key;
            }
        }

        return UserIntent.CancelOperation;
    }
}
