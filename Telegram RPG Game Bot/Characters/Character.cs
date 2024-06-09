using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Core;

namespace Telegram_RPG_Game_Bot.Characters;

[JsonObject(MemberSerialization.OptIn)]
public class Character
{
    [JsonConstructor]
    private Character() { }
    
    public Character(int id, NewCharacterData data)
    {
        Id = id;
        MapIcon = data.MapIcon;
        
        Name = data.Name;

        Level = 1;
        Experience = 0;
        Coins = 100;

        _characteristics = new Characteristics(10, 10, 10);

        _inventory = new Inventory();
    }
    
    [JsonProperty]
    public int Id { get; private set; }
    
    [JsonProperty] 
    public string MapIcon { get; private set; }
    
    [JsonProperty]
    public string Name { get; private set; }
    
    #region LEVEL LOGIC
    
    [JsonProperty]
    public int Level { get; private set; }
    
    [JsonProperty]
    public int Experience { get; private set; }
    
    #endregion
    
    [JsonProperty]
    public int Coins { get; private set; }

    #region INVENTORY LOGIC

    [JsonProperty]
    private Inventory _inventory;

    public void AddItemToInventory(Item item)
    {
        _inventory.AddItem(item);
    }

    public bool TryGetItemFromInventory(string itemName, out Item? item)
    {
        return _inventory.TryGetItem(itemName, out item);
    }

    public string InventoryInfo()
    {
        return $"=== *ИНВЕНТАРЬ* ===" +
               $"\n{_inventory.Info()}";
    }

    #endregion

    #region CHARACTERISTICS LOGIC

    [JsonProperty] 
    private Characteristics _characteristics;

    public async void TryLevelUpCharacteristic(string characteristicName)
    {
        await Bot.SendTextMessageAsync($"{Name}, {_characteristics.TryLevelUp(characteristicName)}");
    }
    
    public string CharacteristicsInfo()
    {
        return _characteristics.Info();
    }
    
    #endregion
    
    public string MainInfo()
    {
        return $"=== *ПЕРСОНАЖ* ====" +
               $"\n" +
               $"\n*Имя*: {Name}" +
               $"\n*Уровень*: {Level}" +
               $"\n*Опыт*: {Experience}" +
               $"\n*Монеты*: {Coins}";
    }
}
