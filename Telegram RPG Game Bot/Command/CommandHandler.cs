using Telegram_RPG_Game_Bot.Command.Commands;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command;

public static class CommandHandler
{
    private static readonly HashSet<CommandBase> Commands = new()
    {
        new CreateCharacterCommand(),
        new MoveOnMapCommand(),
        new ShowMapCommand(),
        new ShowMapSectorInfoCommand(),
        new ShowMyCharacterCommand(),
    };

    public static void Handle(string text, Chat chat, User user)
    {
        foreach (var command in Commands.Where(command => command.Check(text)))
        {
            command.Execute(chat, user);
        }
    }
}