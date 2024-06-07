using System.Text.RegularExpressions;
using Telegram_RPG_Game_Bot.Managers;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command.Commands;

public class ShowMapCommand : CommandBase
{
    protected override Regex CommandPattern { get; set; } = new(@"^карта\s*$", RegexOptions.IgnoreCase);
    public override void Execute(Chat chat, User user)
    {
        if (!CharacterManager.TryGetCharacterByUser(chat, user, out var character))
            return;

        MapManager.ShowMapRegion(character);
    }
}