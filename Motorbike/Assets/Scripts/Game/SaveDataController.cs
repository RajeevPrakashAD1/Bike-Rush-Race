using System.IO;
using UnityEngine;
using HazzardGameworks.ProjectInfrastructure.Game;

/// <summary>
/// This class controls the saving and loading of game data.
/// </summary>
public static class SaveDataController
{
    /// <summary>
    /// Saves the current game settings data.
    /// </summary>
    /// <param name="gameSettingsData"></param>
    public static void SaveGameSettings(GameSettingsData gameSettingsData)
    {
        string json = JsonUtility.ToJson(gameSettingsData);
        System.IO.File.WriteAllText("SettingsData.json", json);
    }

    /// <summary>
    /// Loads the game settings data from disk.
    /// </summary>
    /// <returns></returns>
    public static GameSettingsData LoadGameSettings()
    {
        if(File.Exists("SettingsData.json"))
        {
            string json = File.ReadAllText("SettingsData.json");
            GameSettingsData settingsData = JsonUtility.FromJson<GameSettingsData>(json);
            return settingsData;
        }
        else
        {
            GameSettingsData settingsData = new(1f, 1f);
            return settingsData;
        }
    }
}
