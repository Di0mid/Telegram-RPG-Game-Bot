using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot;

[JsonObject(MemberSerialization.OptIn)]
public class Inventory
{
    [JsonProperty]
    private List<Item> _inventory = new();

    public bool TryGetItem(string itemName, out Item? item)
    {
        item = _inventory.FirstOrDefault(i => i.Name.ToLower().Equals(itemName.ToLower()));

        if (item == null) 
            return false;
        
        _inventory.Remove(item);
        return true;
    }
    
    public void AddItem(Item item)
    {
        _inventory.Add(item);
    }
    
    public string Info()
    {
        var info = "";

        if (_inventory.Count == 0)
        {
            info = "\nПУСТО";
        }
        else
        {
            for (var i = 0; i < _inventory.Count; i++)
            {
                info += $"\n{i + 1}) {_inventory[i].Name}";
            }
        }

        return $"{info}";
    }
}