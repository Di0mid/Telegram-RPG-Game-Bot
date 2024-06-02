using System.Text.RegularExpressions;
using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Managers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram_RPG_Game_Bot.Core
{
    internal static class Program
    {
        private static readonly Regex CreateCharacterCommand =
            new(@"^создать\s+персонажа\n\nимя:\s+(?<name>\w+)$",
                RegexOptions.IgnoreCase);

        private static void Main(string[] args)
        {
            Bot.Start(UpdateHandler, ErrorHandler);

            Console.ReadLine();
            
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

                if (message.Type == MessageType.Text)
                {
                    var text = message.Text;
                    
                    var match = CreateCharacterCommand.Match(text);

                    if (match.Success)
                    {
                        CharacterManager.TryCreateCharacter(chat, user,
                            new NewCharacterData(match.Groups["name"].Value));
                    }
                }
            }
        }
        
        private static async Task ErrorHandler(ITelegramBotClient bot, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
        }
    }
}
