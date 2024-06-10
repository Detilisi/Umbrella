﻿using Application.Common.Abstractions.Services;
using Infrastructure.Common.Services;
using MauiClientApp.Common.ChatHistory.Emums;
using MauiClientApp.Common.ChatHistory.Models;
using System.Diagnostics;

namespace MauiClientApp.Common.Base;

public abstract partial class ViewModel : ObservableObject
{
    //Services
    private readonly IAppTextToSpeech _textToSpeech = new AppTextToSpeech();
    private readonly IAppSpeechRecognition _speechRecognition = new AppSpeechRecognition();

    //Fields
    private static bool MicrophoneUsable { get; set; } = false;
    [ObservableProperty] public bool isListening = false;
    internal ObservableCollection<ChatHistoryModel> ChatHistory { get; private set; } = [];

    //ViewModel lifecylce
    public virtual async void OnViewModelStarting(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is starting");

        if(MicrophoneUsable) return;
        MicrophoneUsable = await _speechRecognition.RequestPermissions(token);
        await SpeakAsync("Elevate your email experince with Umbrella.");
    }
    public virtual async void OnViewModelClosing(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is closing");

        await _speechRecognition.StopListenAsync(token);
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
        await _textToSpeech.SpeakAsync(messageText);
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

            var listenResult = await _speechRecognition.ListenAsync();
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