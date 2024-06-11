using Infrastructure.Common.Services;

namespace MauiClientApp.Common.ChatHistory.ViewModels;

internal partial class ChatHistoryViewModels : ViewModel
{
    //Services
    private AppTextToSpeech TextToSpeech { get; } = new AppTextToSpeech();
    private AppSpeechRecognition SpeechRecognition { get; } = new AppSpeechRecognition();

    //Fields
    [ObservableProperty] public bool isListening = false;
    private static bool MicrophoneUsable { get; set; } = false;
    internal static ObservableCollection<ChatHistoryModel> ChatHistory { get; private set; } = [];

    //ViewModel lifecylce
    public override async void OnViewModelStarting(CancellationToken token = default)
    {
        if (MicrophoneUsable) return;
        MicrophoneUsable = await SpeechRecognition.RequestPermissions(token);
        await SpeakCommand.ExecuteAsync("Elevate your email experince with Umbrella.");
    }
    public override async void OnViewModelClosing(CancellationToken token = default)
    {
        await SpeechRecognition.StopListenAsync(token);
    }

    //Commands
    [RelayCommand]
    public async Task Speak(string messageText)
    {
        if (IsListening || string.IsNullOrEmpty(messageText)) return;

        ChatHistory.Add(new()
        {
            Sender = ChatSender.Bot,
            Message = messageText
        });
        await TextToSpeech.SpeakAsync(messageText);
    }

    [RelayCommand]
    public async Task Listen()
    {
        if (IsListening) return;

        try
        {
            IsListening = true;
            if (!MicrophoneUsable)
            {
                await SpeakCommand.ExecuteAsync("Permission not granted to use microphone.");
                return;
            }

            var listenResult = await SpeechRecognition.ListenAsync();
            if (listenResult.IsFailure)
            {
                await Speak(listenResult.Error.Message);
            }

            ChatHistory.Add(new()
            {
                Sender = ChatSender.Bot,
                Message = listenResult.Value
            });
        }
        catch
        {
            await SpeakCommand.ExecuteAsync("Speech recognition has failed!");
        }
        finally
        {
            IsListening = false;
        }
    }

}
