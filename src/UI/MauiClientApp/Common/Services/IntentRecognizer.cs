using MauiClientApp.Common.Enums;

namespace MauiClientApp.Common.Services;

internal class IntentRecognizer
{
    private static readonly Dictionary<UserIntent, HashSet<string>> IntentCommands = new Dictionary<UserIntent, HashSet<string>>
    {
        { UserIntent.ReadEmails, new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Read", "Check", "Review", "Scan", "Skim", "Look over", "Browse", "Peruse", "Inspect",
                "Examine", "Glance at", "Go through", "Scroll through", "Study", "Monitor", "Look through",
                "Analyze", "Verify", "Observe", "Take a look"
            }
        },
        { UserIntent.WriteEmail, new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Compose", "Draft", "Write", "Send", "Type", "Create", "Prepare", "Pen down", "Construct",
                "Craft", "Formulate", "Produce", "Develop", "Email", "Message", "Shoot off", "Scribble",
                "Jot down", "Fire off"
            }
        },
        { UserIntent.OpenEmail, new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Open", "View", "Access", "Display", "Show", "Reveal", "Expose", "Unveil", "Present",
                "Uncover", "Look at", "Inspect", "Check", "See", "Examine", "Peruse", "Browse"
            }
        },
        { UserIntent.Cancel, new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Cancel", "Abort", "Stop", "Terminate", "Discontinue", "Halt", "Cease", "End", "Annul",
                "Call off", "Scrap", "Drop", "Dismiss", "Nullify", "Revoke", "Rescind", "Abort mission",
                "Scrap", "Cut short", "Put an end to", "Break off", "Give up"
            }
        },
        { UserIntent.No, new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "No", "Nope", "Nah", "Not at all", "No way", "Negative", "Absolutely not", "Never", "By no means",
                "Not really", "Nuh-uh", "No thanks", "Not quite", "No sir", "Not interested", "Refuse",
                "Decline", "Reject", "Disagree", "Not now"
            }
        },
        { UserIntent.Yes, new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Yes", "Yeah", "Yup", "Sure", "Absolutely", "Definitely", "Indeed", "Affirmative",
                "Correct", "Right", "True", "Agreed", "Confirm", "I agree", "I confirm", "Of course",
                "Certainly", "Okay", "Ok", "All right", "Alright", "Yep", "I do", "Exactly", "For sure",
                "Without a doubt", "Of course", "Sure thing", "By all means", "Naturally"
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

        return UserIntent.Undefined;
    }
}
