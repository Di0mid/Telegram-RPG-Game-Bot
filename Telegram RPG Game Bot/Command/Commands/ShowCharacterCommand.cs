using System.Text.RegularExpressions;
using Telegram_RPG_Game_Bot.Core;
using Telegram_RPG_Game_Bot.Managers;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command.Commands;

public class ShowCharacterCommand : CommandBase
{
    protected override Regex CommandPattern { get; set; } =
        new(@"^мой\s+персонаж\s*$", RegexOptions.IgnoreCase);

    public override async void Execute(Chat chat, User user)
    {
        if (!CharacterManager.TryGetCharacter(chat, user, out var character))
            return;

        await Bot.SendTextMessageAsync(character.MainInfo());
    }
}