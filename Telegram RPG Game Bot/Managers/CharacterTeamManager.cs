using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;
using Telegram_RPG_Game_Bot.Database;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Managers;

public static class CharacterTeamManager
{
    private static List<CharacterTeam> _teams = new();

    public static async void TryCreateTeamWithoutFirstMember(Chat chat, Character leader)
    {
        if (_teams.Any(t => t.CompareLeader(leader)))
        {
            await Bot.SendTextMessageAsync($"*{leader.Name}*, ты уже являешься лидером одной команды");
            return;
        }
        
        var newTeam = new CharacterTeam(chat, leader);
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

    public static async void ShowTeamSharedInventory(Character character)
    {
        if (!TryGetTeamByMember(character, out var team))
        {
            await Bot.SendTextMessageAsync($"*{character.Name}*, ты не состоишь в команде");
            return;
        }

        await Bot.SendTextMessageAsync(team.SharedInventoryInfo());
    }
    
    public static async void TryEpelMember(Character initiator, Character expelledMember)
    {
        if (!TryGetTeamByLeader(initiator, out var team))
        {
            await Bot.SendTextMessageAsync(
                $"*{initiator.Name}*, ты не являешься лидером команды, дабы совершить это действие");
            return;
        }

        if (!team.ContainsMember(expelledMember))
        {
            await Bot.SendTextMessageAsync(
                $"Персонаж *{expelledMember.Name}* не состоит в команде *{team.Name}*, дабы быть изгнанным из нее");
            return;
        }

        if (initiator.Id == expelledMember.Id)
        {
            await Bot.SendTextMessageAsync($"*{initiator.Name}*, ты не можешь изгнать самого себя");
            return;
        }

        team.EpelMember(expelledMember);
        await Bot.SendTextMessageAsync($"*{expelledMember.Name}* успешно изгнан из команды *{team.Name}*...");
    }
    
    public static async void TryChangeLeader(Character initiator, Character newLeader)
    {
        if (!TryGetTeamByLeader(initiator, out var team))
        {
            await Bot.SendTextMessageAsync(
                $"*{initiator.Name}*, ты не являешься лидером команды, дабы совершить это действие");
            return;
        }

        if (!team.ContainsMember(newLeader))
        {
            await Bot.SendTextMessageAsync(
                $"Персонаж *{newLeader.Name}* не состоит в команде *{team.Name}*, дабы стать ее новым лидером");
            return;
        }

        if (initiator.Id == newLeader.Id)
        {
            await Bot.SendTextMessageAsync($"*{initiator.Name}*, ты уже являешься лидером команды *{team.Name}*");
            return;
        }
        
        team.ChangeLeader(newLeader);
        await Bot.SendTextMessageAsync($"*{newLeader.Name}* теперь лидер команды *{team.Name}*!");
    }
    
    public static async void TryAddMember(Character leader, Character member)
    {
        if (!TryGetTeamByLeader(leader, out var team))
        {
            await Bot.SendTextMessageAsync($"*{leader.Name}*, у тебя нет своей команды");
            return;
        }

        if (leader.Id == member.Id)
        {
            await Bot.SendTextMessageAsync($"*{leader.Name}*, ты уже являешься членом своей команды - ты ее лидер...");
            return;
        }

        if (team.ContainsMember(member))
        {
            await Bot.SendTextMessageAsync($"*{member.Name}* - этот персонаж уже состоит в твоей команде");
            return;
        }
        
        if (_teams.Any(t => t.ContainsMember(member)))
        {
            await Bot.SendTextMessageAsync($"*{member.Name}* - этот персонаж уже является членом другой команды");
            return;
        }
        
        team.AddMember(member);
        await Bot.SendTextMessageAsync($"*{member.Name}* стал членом команды *{team.Name}*!");
    }
    
    public static async void ShowTeamInfo(Character character)
    {
        if (!TryGetTeamByMember(character, out var team))
        {
            await Bot.SendTextMessageAsync($"*{character.Name}*, ты не состоишь в команде");
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

        if (_teams.Any(t => t.Name.Equals(newTeamName)))
        {
            await Bot.SendTextMessageAsync($"К сожалению, имя \"*{newTeamName}*\" уже занято другой командой");
            return;
        }
        
        team.ChangeTeamName(newTeamName);
        await Bot.SendTextMessageAsync($"*{leader.Name}*, имя твоей команды успешно изменено на *{newTeamName}*");
    }

    private static bool TryGetTeamByMember(Character member, out CharacterTeam? team)
    {
        team = _teams.FirstOrDefault(t => t.ContainsMember(member));
        return team != null;
    }

    private static bool TryGetTeamByLeader(Character leader, out CharacterTeam? team)
    {
        team = _teams.FirstOrDefault(t => t.CompareLeader(leader));
        return team != null;
    }

    #region SAVE AND LOAD
    
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
    
    #endregion
}