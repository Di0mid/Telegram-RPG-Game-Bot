﻿using Telegram_RPG_Game_Bot.Command.Commands;
using Telegram_RPG_Game_Bot.Command.Commands.CharacterTeam;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command;

public static class CommandHandler
{
    private static readonly HashSet<CommandBase> Commands = new()
    {
        new AddCharacterTeamMemberCommand(),
        new ChangeCharacterTeamLeaderCommand(),
        new ChangeCharacterTeamNameCommand(),
        new CharacteristicLevelUpCommand(),
        new CreateCharacterCommand(),
        new CreateCharacterTeamCommand(),
        new ExpelCharacterTeamMemberCommand(),
        new MoveOnMapCommand(),
        new ShowCharacterCommand(),
        new ShowCharacteristicsCommand(),
        new ShowCharacterTeamInfoCommand(),
        new ShowMapCommand(),
        new ShowMapSectorInfoCommand(),
        new ShowCharacterTeamSharedInventoryCommand(),
        new ShowCharacterInventoryCommand(),
    };

    public static void Handle(string text, Chat chat, User user)
    {
        foreach (var command in Commands.Where(command => command.Check(text)))
        {
            command.Execute(chat, user);
        }
    }
}