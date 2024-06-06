using System.Text.RegularExpressions;
using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Command;
using Telegram_RPG_Game_Bot.Managers;
using Telegram_RPG_Game_Bot.Map;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram_RPG_Game_Bot.Core
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            MapManager.GenerateOrLoad();
            CharacterManager.LoadCharacters();
            
            Bot.Start(UpdateHandler, ErrorHandler);

            Console.ReadLine();
            
            CharacterManager.SaveCharacters();
            MapManager.Save();
            
            Bot.Stop();

            Console.ReadLine();
        }

        private static async Task UpdateHandler(ITelegramBotClient bot, Update update,
            CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                var chat = message.Chat;
                var user = message.From;

                Bot.SetCurrentChat(chat);

                if (message.Type == MessageType.Text)
                {
                    var text = message.Text;
                    
                    CommandHandler.Handle(text, chat, user);
                }
            }
        }

        private static async Task ErrorHandler(ITelegramBotClient bot, Exception exception,
            CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
        }
    }
}
