using System.Text.RegularExpressions;
using Telegram_RPG_Game_Bot.Managers;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command.Commands.CharacterTeam;

public class ExpelCharacterTeamMemberCommand : CommandBase
{
    protected override Regex CommandPattern { get; set; } =
        new(@"^изгнать\s+(?<memberName>\w+)\s+из\s+команды\s*$", RegexOptions.IgnoreCase);
    public override void Execute(Chat chat, User user)
    {
        if(!CharacterManager.TryGetCharacter(chat, user, out var character))
            return;
        
        if(Match == null)
            return;
        
        if(!CharacterManager.TryGetCharacterByName(chat, Match.Groups["memberName"].Value, out var member))
            return;

        CharacterTeamManager.TryEpelMember(character, member);
    }
}