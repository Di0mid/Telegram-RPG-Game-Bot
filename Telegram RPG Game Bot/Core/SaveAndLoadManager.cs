using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot.Core;

public static class SaveAndLoadManager
{
    public static async Task Save<T>(T saveObject, string savePath, string fileName)
    {
        await File.WriteAllTextAsync(savePath + fileName + ".json",
            JsonConvert.SerializeObject(saveObject, Formatting.Indented));
    }

    public static T? Load<T>(string loadPath, string fileName)
    {
        var filePath = loadPath + fileName + ".json";
        if (!File.Exists(filePath))
            return default;

        var file = File.ReadAllTextAsync(filePath);
        return JsonConvert.DeserializeObject<T>(file.Result);
    }
}