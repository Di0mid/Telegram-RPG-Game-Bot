using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;
using Telegram_RPG_Game_Bot.Database;

namespace Telegram_RPG_Game_Bot.Managers;

public static class CharacterTeamManager
{
    private static List<CharacterTeam> _teams = new();

    public static async void TryCreateTeamWithoutFirstMember(Character leader)
    {
        if (_teams.Any(t => t.CompareLeader(leader)))
        {
            await Bot.SendTextMessageAsync($"*{leader.Name}*, ты уже являешься лидером одной команды");
            return;
        }
        
        var newTeam = new CharacterTeam(leader);
        _teams.Add(newTeam);

        await Bot.SendTextMessageAsync($"Поздравляю, *{leader.Name}*, твоя команда создана!");
    }
    
    /*public static async void TryCreateTeamWithFirstMember(Character leader, Character member)
    {
        if (_teams.Any(t => t.CompareLeader(leader)))
        {
            await Bot.SendTextMessageAsync($"*{leader.Name}*, ты уже являешься лидером одной команды");
            return;
        }

        if (_teams.Any(t => t.ContainsMember(member)))
        {
            await Bot.SendTextMessageAsync($"*{member.Name}* - этот персонаж уже является членом другой команды");
            return;
        }

        var newTeam = new CharacterTeam(leader, member);
        _teams.Add(newTeam);

        await Bot.SendTextMessageAsync($"Поздравляю, *{leader.Name}*, твоя команда создана!");
    }*/

    public static async void TryAddMember(Character leader, Character member)
    {
        if (!TryGetTeamByLeader(leader, out var team))
        {
            await Bot.SendTextMessageAsync($"*{leader.Name}*, у тебя нет своей команды");
            return;
        }

        if (_teams.Any(t => t.ContainsMember(member)))
        {
            await Bot.SendTextMessageAsync($"*{member.Name}* - этот персонаж уже является членом другой команды");
            return;
        }
        
        if (team.ContainsMember(member))
        {
            await Bot.SendTextMessageAsync($"*{member.Name}* - этот персонаж уже состоит в твоей команде");
            return;
        }

        team.AddMember(member);
        await Bot.SendTextMessageAsync($"*{member.Name}* стал членом команды *{team.Name}*!");
    }
    
    public static async void ShowTeamInfo(Character character)
    {
        if (!TryGetTeamByLeaderOrMember(character, out var team))
        {
            await Bot.SendTextMessageAsync($"*{character.Name}*, у тебя нет своей команды или ты не состоишь ни в одной");
            return;
        }

        await Bot.SendTextMessageAsync(team.Info());
    }
    
    public static async void ChangeTeamName(Character leader, string newTeamName)
    {
        if (!TryGetTeamByLeader(leader, out var team))
        {
            await Bot.SendTextMessageAsync($"*{leader.Name}*, у тебя нет своей команды");
            return;
        }
        
        team.ChangeTeamName(newTeamName);
        await Bot.SendTextMessageAsync($"**{leader.Name}, имя твоей команды успешно изменено на *{newTeamName}*");
    }

    private static bool TryGetTeamByLeaderOrMember(Character leaderOrMember, out CharacterTeam? team)
    {
        team = _teams.FirstOrDefault(t => t.CompareLeader(leaderOrMember) || t.ContainsMember(leaderOrMember));
        return team != null;
    }

    private static bool TryGetTeamByLeader(Character leader, out CharacterTeam? team)
    {
        team = _teams.FirstOrDefault(t => t.CompareLeader(leader));
        return team != null;
    }
    
    public static async void Save()
    {
        await SaveAndLoadManager.Save(_teams, SavePaths.TeamsSavePath, nameof(_teams));
    }

    public static void Load()
    {
        var teams = SaveAndLoadManager.Load<List<CharacterTeam>>(
            SavePaths.TeamsSavePath, nameof(_teams));

        if (teams == null) return;
        
        _teams = teams;
    }
}