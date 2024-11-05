using System.IO;
using UnityEngine;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private string saveFolderPath;
    private int currentSlotIndex = 0;

    private void Start()
    {
        saveFolderPath = @"C:\PuzzleHorror\Saves";
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }
    }

    public void SaveGame(SaveData data, int slotIndex)
    {
        string filePath = Path.Combine(saveFolderPath, $"save_slot_{slotIndex}.json");
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, jsonData);
    }

    public void SaveCurrentProgress(int level)
    {
        SaveData data = new SaveData
        {
            level = level
        };

        SaveGame(data, currentSlotIndex);
    }

    public SaveData LoadGame(int slotIndex)
    {
        string filePath = Path.Combine(saveFolderPath, $"save_slot_{slotIndex}.json");
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SaveData>(jsonData);
        }
        return null;
    }

    public void DeleteGame(int slotIndex)
    {
        string filePath = Path.Combine(saveFolderPath, $"save_slot_{slotIndex}.json");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public void SetCurrentSlotIndex(int slotIndex)
    {
        currentSlotIndex = slotIndex;
    }
}
