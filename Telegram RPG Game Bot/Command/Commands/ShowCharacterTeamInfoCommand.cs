using System.Text.RegularExpressions;
using Telegram_RPG_Game_Bot.Managers;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command.Commands;

public class ShowCharacterTeamInfoCommand : CommandBase
{
    protected override Regex CommandPattern { get; set; } = new(@"^моя\s+команда\s*$", RegexOptions.IgnoreCase);
    public override void Execute(Chat chat, User user)
    {
        if(!CharacterManager.TryGetCharacter(chat, user, out var character))
            return;
        
        CharacterTeamManager.ShowTeamInfo(character);
    }
}