using System.Text.RegularExpressions;
using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Managers;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command.Commands;

public class CreateCharacterCommand : CommandBase
{
    protected override Regex CommandPattern { get; set; } =
        new(@"^создать\s+персонажа\s*\nимя:\s+(?<name>\w+)\s*$", RegexOptions.IgnoreCase);

    public override void Execute(Chat chat, User user)
    {
        if(Match == null)
            return;
        
        CharacterManager.TryCreateCharacter(chat, user, new NewCharacterData(Match.Groups["name"].Value));
    }
}