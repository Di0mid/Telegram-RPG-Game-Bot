using System.Text.RegularExpressions;
using Telegram_RPG_Game_Bot.Managers;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command.Commands.CharacterTeam;

public class ChangeCharacterTeamLeaderCommand : CommandBase
{
    protected override Regex CommandPattern { get; set; } =
        new(@"^изменить\s+лидера\s+команды\s+на\s+(?<memberName>\w+)\s*$", RegexOptions.IgnoreCase);
    public override void Execute(Chat chat, User user)
    {
        if(!CharacterManager.TryGetCharacter(chat, user, out var character))
            return;
        
        if(Match == null)
            return;
        
        if(!CharacterManager.TryGetCharacterByName(chat, Match.Groups["memberName"].Value, out var member))
            return;

        CharacterTeamManager.TryChangeLeader(character, member);
    }
}