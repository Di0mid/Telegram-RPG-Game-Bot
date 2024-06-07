using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot.Characters;

[JsonObject(MemberSerialization.OptIn)]
public class CharacterTeam
{
    [JsonConstructor]
    private CharacterTeam(){}
    
    public CharacterTeam(Character leader, Character firstMember)
    {
        SetupTeam(leader, firstMember);
    }

    public CharacterTeam(Character leader)
    {
        SetupTeam(leader);
    }
    
    [JsonProperty]
    public string Name { get; private set; }
     
    [JsonProperty]
    private Character _leader;
    [JsonProperty]
    private List<Character> _members;

    public bool ChangeLeader(string newLeaderName)
    {
        if (!TryGetMember(newLeaderName, out var member))
            return false;

        _leader = member;
        return true;
    }
    
    public void ChangeTeamName(string teamName)
    {
        Name = teamName;
    }
    
    public void AddMember(Character member)
    {
        _members.Add(member);
    }

    public bool TryEpelMember(string memberName)
    {
        if (!TryGetMember(memberName, out var member))
            return false;

        _members.Remove(member);
        return true;
    }
    
    public bool CompareLeader(Character leader)
    {
        return _leader.Id == leader.Id;
    }

    public bool ContainsMember(Character member)
    {
        return _members.Any(m => m.Id == member.Id);
    }
    
    private bool TryGetMember(string memberName, out Character? member)
    {
        member = _members.FirstOrDefault(m => m.Name.Equals(memberName));

        return member != null;
    }

    private void SetupTeam(Character leader, Character? firstMember = null)
    {
        Name = "ИМЯ КОМАНДЫ";
        
        _leader = leader;
        _members = new List<Character> { _leader };

        if (firstMember != null)
            _members.Add(firstMember);
    }
    
    public string Info()
    {
        var membersInfo = _members.Aggregate("", (current, member) => current + $"{member.Name}, ");

        return $"== *{Name}* ==" +
               $"\n" +
               $"\n*Лидер*: {_leader.Name}" +
               $"\n*Состав*: {membersInfo}";
    }
}