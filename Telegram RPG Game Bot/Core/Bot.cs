using Telegram_RPG_Game_Bot.Database;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using File = Telegram.Bot.Types.File;

namespace Telegram_RPG_Game_Bot.Core;

public static class Bot
{
    private static readonly string BotToken = Core.BotToken.Token;
    private static readonly TelegramBotClient BotClient = new(BotToken);
    private static readonly CancellationTokenSource CancellationTokenSource = new();
    private static readonly CancellationToken CancellationToken = CancellationTokenSource.Token;
    private static readonly ReceiverOptions ReceiverOptions = new();

    private static List<Chat> _chats = new();
    private static Chat _currentChat = new();

    public static async void Start(Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler,
        Func<ITelegramBotClient, Exception, CancellationToken, Task> errorHandler)
    {
        Console.WriteLine("=== БОТ ЗАПУЩЕН ===");

        var chats = SaveAndLoadManager.Load<List<Chat>>(SavePaths.ChatsSavePath, nameof(_chats));
        if (chats != null)
            _chats = chats;
        
        await TrySendTextMessageToAllChats("БОТ ЗАПУЩЕН");

        BotClient.StartReceiving(
            updateHandler: updateHandler,
            pollingErrorHandler: errorHandler,
            receiverOptions: ReceiverOptions,
            cancellationToken: CancellationToken);
    }

    public static async void Stop()
    {
        await SaveAndLoadManager.Save(_chats, SavePaths.ChatsSavePath, nameof(_chats));
        
        await TrySendTextMessageToAllChats("БОТ ВЫКЛЮЧЕН");

        await CancellationTokenSource.CancelAsync();
    }

    public static void SetCurrentChat(Chat chat)
    {
        _currentChat = chat;
        TryAddNewChat(chat);
    }

    #region MESSAGE METHODS

    public static async Task<Message> SendTextMessageAsync(string text, IReplyMarkup? replyMarkup = null)
    {
        return await SendTextMessageAsync(_currentChat, text, replyMarkup);
    }

    private static async Task<Message> SendTextMessageAsync(Chat chat, string text, IReplyMarkup? replyMarkup = null)
    {
        Console.WriteLine($"--- БОТ в |{chat.Title}.{chat.Type}| написал: {text}");

        return await BotClient.SendTextMessageAsync(
            chatId: chat.Id,
            text: text,
            parseMode: ParseMode.Markdown,
            replyMarkup: replyMarkup,
            cancellationToken: CancellationToken);
    }

    public static async Task<Message> SendPhotoAsync(Chat chat, string photoPath, string? caption = null,
        IReplyMarkup? replyMarkup = null)
    {
        Console.WriteLine($"--- БОТ в |{chat.Title}.{chat.Type}| отправил фото с подписью: {caption}");

        await using var stream = System.IO.File.OpenRead(photoPath);
        return await BotClient.SendPhotoAsync(
            chatId: chat.Id,
            photo: InputFile.FromStream(stream, stream.Name),
            caption: caption,
            parseMode: ParseMode.Markdown,
            replyMarkup: replyMarkup,
            cancellationToken: CancellationToken);
    }

    public static async Task<Message> EditMessageTextAsync(int messageId, string newText,
        InlineKeyboardMarkup? replyMarkup = null)
    {
        return await BotClient.EditMessageTextAsync(
            chatId: _currentChat.Id,
            messageId: messageId,
            text: newText,
            parseMode: ParseMode.Markdown,
            replyMarkup: replyMarkup,
            cancellationToken: CancellationToken);
    }

    public static async Task<Message> EditMessageCaptionAsync(Chat chat, int messageId, string? caption = null)
    {
        return await BotClient.EditMessageCaptionAsync(
            chatId: chat.Id,
            messageId: messageId,
            caption: caption,
            parseMode: ParseMode.Markdown,
            cancellationToken: CancellationToken);
    }

    public static async Task<File> GetFileAsync(string fileId)
    {
        return await BotClient.GetFileAsync(fileId, CancellationToken);
    }

    public static async Task DownloadFileAsync(string filePath, FileStream stream)
    {
        await BotClient.DownloadFileAsync(filePath, stream, CancellationToken);
    }
        
    #endregion

    private static async Task TrySendTextMessageToAllChats(string text)
    {
        if(_chats.Count > 0)
        {
            foreach (var chat in _chats)
            {
                await SendTextMessageAsync(chat, text);
            }
        }
    }

    private static void TryAddNewChat(Chat chat)
    {
        if(_chats.Any(c => c.Id == chat.Id))
            return;
            
        _chats.Add(chat);
    }
}