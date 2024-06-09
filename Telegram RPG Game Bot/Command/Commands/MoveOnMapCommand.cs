using System.Text.RegularExpressions;
using Telegram_RPG_Game_Bot.Managers;
using Telegram_RPG_Game_Bot.Map;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command.Commands;

public class MoveOnMapCommand : CommandBase
{
    protected override Regex CommandPattern { get; set; } =
        new(@"^на\s+(?<direction>((\w+)|(\w+-\w+)))\s+(?<stepCount>\d+)\s*$", RegexOptions.IgnoreCase);
    public override void Execute(Chat chat, User user)
    {
        if(Match == null)
            return;
        
        if (!CharacterManager.TryGetCharacter(chat, user, out var character))
            return;
                        
        if (!MapMovementDirectionMapping.TryGetMovementDirection(Match.Groups["direction"].Value,
                out var direction))
            return;

        MapManager.MoveCharacter(character, direction, int.Parse(Match.Groups["stepCount"].Value));    }
}