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
            new(@"^создать\s+персонажа\nимя:\s+(?<name>\w+)$",
                RegexOptions.IgnoreCase);

        private static void Main(string[] args)
        {
            CharacterManager.LoadCharacters();
            
            Bot.Start(UpdateHandler, ErrorHandler);

            Console.ReadLine();
            
            CharacterManager.SaveCharacters();
            
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
                    
                    var match = CreateCharacterCommand.Match(text);

                    if (match.Success)
                    {
                        CharacterManager.TryCreateCharacter(chat, user,
                            new NewCharacterData(match.Groups["name"].Value));
                    }
                    else if (text.Equals("мой персонаж", StringComparison.OrdinalIgnoreCase))
                    {
                        if (CharacterManager.TryGetCharacter(chat, user, out var character))
                        {
                            await Bot.SendTextMessageAsync(character.MainInfo());
                        }
                    }
                }
            }
        }

        private static async Task ErrorHandler(ITelegramBotClient bot, Exception exception,
            CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
