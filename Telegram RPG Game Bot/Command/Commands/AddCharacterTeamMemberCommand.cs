using System.Text.RegularExpressions;
using Telegram_RPG_Game_Bot.Managers;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command.Commands;

public class AddCharacterTeamMemberCommand : CommandBase
{
    protected override Regex CommandPattern { get; set; } = new(@"^добавить\s+в\s+команду\s+(?<characterName>\w+)\s*$",
        RegexOptions.IgnoreCase);
    public override void Execute(Chat chat, User user)
    {
        if(!CharacterManager.TryGetCharacter(chat, user, out var character))
            return;
        
        if(Match == null)
            return;
        
        if(!CharacterManager.TryGetCharacterByName(chat, Match.Groups["characterName"].Value, out var character1))
           return;
        
        CharacterTeamManager.TryAddMember(character, character1);
    }
}