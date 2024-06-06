using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Command;

public abstract class CommandBase
{
    protected abstract Regex CommandPattern { get; set; }

    protected Match? Match;
    
    public bool Check(string text)
    {
        Match = CommandPattern.Match(text);
        return Match.Success;
    }

    public abstract void Execute(Chat chat, User user);
}