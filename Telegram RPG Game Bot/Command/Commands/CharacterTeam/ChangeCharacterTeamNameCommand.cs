using System.Text.RegularExpressions;
using Telegram_RPG_Game_Bot.Managers;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command.Commands.CharacterTeam;

public class ChangeCharacterTeamNameCommand : CommandBase
{
    protected override Regex CommandPattern { get; set; } =
        new(@"^изменить\s+имя\s+команды\s+(?<teamName>\w+)\s*$", RegexOptions.IgnoreCase);
    public override void Execute(Chat chat, User user)
    {
        if(!CharacterManager.TryGetCharacter(chat, user, out var character))
            return;
        
        if(Match == null)
            return;
        
        CharacterTeamManager.ChangeTeamName(character, Match.Groups["teamName"].Value);
    }
}