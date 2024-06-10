using Application.Common.Abstractions.Services;
using Infrastructure.Common.Services;
using MauiClientApp.Common.ChatHistory.Emums;
using MauiClientApp.Common.ChatHistory.Models;
using System.Diagnostics;

namespace MauiClientApp.Common.Base;

public abstract partial class ViewModel : ObservableObject
{
    //Services
    private AppTextToSpeech TextToSpeech { get; } = new AppTextToSpeech();
    private AppSpeechRecognition SpeechRecognition { get; } = new AppSpeechRecognition();

    //Fields
    [ObservableProperty] public bool isListening = false;
    private static bool MicrophoneUsable { get; set; } = false;
    internal static ObservableCollection<ChatHistoryModel> ChatHistory { get; private set; } = [];

    //ViewModel lifecylce
    public virtual async void OnViewModelStarting(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is starting");

        if(MicrophoneUsable) return;
        MicrophoneUsable = await SpeechRecognition.RequestPermissions(token);
        await SpeakAsync("Elevate your email experince with Umbrella.");
    }
    public virtual async void OnViewModelClosing(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is closing");

        await SpeechRecognition.StopListenAsync(token);
    }

    //Text-to-speech methods
    public async Task SpeakAsync(string messageText)
    {
        if (IsListening || string.IsNullOrEmpty(messageText)) return;

        ChatHistory.Add(new() 
        {
            Sender = ChatSender.Bot,
            Message = messageText
        });
        await TextToSpeech.SpeakAsync(messageText);
    }

    public async Task ListenAsync()
    {
        if (IsListening) return;

        try
        {
            IsListening = true;
            if (!MicrophoneUsable)
            {
                await SpeakAsync("Permission not granted to use microphone.");
                return;
            }

            var listenResult = await SpeechRecognition.ListenAsync();
            if (listenResult.IsFailure)
            {
                await SpeakAsync(listenResult.Error.Message);
            }

            ChatHistory.Add(new()
            {
                Sender = ChatSender.Bot,
                Message = listenResult.Value
            });
        }
        catch
        {
            await SpeakAsync("Speech recognition has failed!");
        }
        finally
        {
            IsListening = false;
        }
    }
}