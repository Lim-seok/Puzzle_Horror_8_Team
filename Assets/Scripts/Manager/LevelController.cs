using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelController : Singleton<LevelController>
{
    [SerializeField] private string[] LevelArray;
    public int currentLevel;

    private void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        SaveData saveData = SaveLoadManager.Instance?.LoadGame(SaveLoadManager.Instance.currentSlotIndex);
        currentLevel = saveData.level;
        SceneManager.LoadScene(LevelArray[currentLevel]);
    }

    public void NextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene($"Level{currentLevel}");

    }
}