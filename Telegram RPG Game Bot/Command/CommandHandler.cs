using Telegram_RPG_Game_Bot.Command.Commands;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command;

public static class CommandHandler
{
    private static readonly HashSet<CommandBase> Commands = new()
    {
        new CharacteristicLevelUpCommand(),
        new CreateCharacterCommand(),
        new MoveOnMapCommand(),
        new ShowCharacterCommand(),
        new ShowCharacteristicsCommand(),
        new ShowMapCommand(),
        new ShowMapSectorInfoCommand(),
    };

    public static void Handle(string text, Chat chat, User user)
    {
        foreach (var command in Commands.Where(command => command.Check(text)))
        {
            command.Execute(chat, user);
        }
    }
}