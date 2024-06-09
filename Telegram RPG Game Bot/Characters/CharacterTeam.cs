using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Managers;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Characters;

[JsonObject(MemberSerialization.OptIn)]
public class CharacterTeam
{
    [JsonConstructor]
    private CharacterTeam(){}
    
    public CharacterTeam(Chat chat, Character leader, Character firstMember)
    {
        SetupTeam(chat, leader, firstMember);
    }

    public CharacterTeam(Chat chat, Character leader)
    {
        SetupTeam(chat, leader);
    }
    
    [JsonProperty]
    public string Name { get; private set; }
     
    [JsonProperty]
    private int _leaderCharacterId;
    [JsonProperty]
    private List<int> _memberCharactersId;

    [JsonProperty]
    private long _chatId;

    public void ChangeLeader(Character newLeader)
    {
        _leaderCharacterId = newLeader.Id;
    }
    
    public void ChangeTeamName(string teamName)
    {
        Name = teamName;
    }
    
    public void AddMember(Character member)
    {
        _memberCharactersId.Add(member.Id);
    }

    public void EpelMember(Character member)
    {
        _memberCharactersId.Remove(member.Id);
    }
    
    public bool CompareLeader(Character leader)
    {
        return _leaderCharacterId == leader.Id;
    }

    public bool ContainsMember(Character member)
    {
        return _memberCharactersId.Any(m => m == member.Id);
    }
    
    private void SetupTeam(Chat chat, Character leader, Character? firstMember = null)
    {
        Name = "ИМЯ КОМАНДЫ";
        
        _leaderCharacterId = leader.Id;
        _memberCharactersId = new List<int> { _leaderCharacterId };

        if (firstMember != null)
            _memberCharactersId.Add(firstMember.Id);

        _chatId = chat.Id;
    }
    
    public string Info()
    {
        var membersInfo = _memberCharactersId.Aggregate("",
            (current, member) => current + $"{CharacterManager.GetCharacterById(_chatId, member).Name}, ");

        return $"== *{Name}* ==" +
               $"\n" +
               $"\n*Лидер*: {CharacterManager.GetCharacterById(_chatId, _leaderCharacterId).Name}" +
               $"\n*Состав*: {membersInfo}";
    }
}