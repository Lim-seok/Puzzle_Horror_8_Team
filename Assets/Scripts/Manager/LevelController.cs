using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelController : Singleton<LevelController>
{
    [SerializeField] private string[] LevelArray;
    private int currentLevel;

    private void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        SaveData saveData = SaveLoadManager.Instance?.LoadGame(SaveLoadManager.Instance.currentSlotIndex);
        currentLevel = saveData?.level ?? 0; 
        SceneManager.LoadScene(LevelArray[currentLevel]);
    }

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel < LevelArray.Length)
        {
            SceneManager.LoadScene(LevelArray[currentLevel]);
            SaveLoadManager.Instance?.SaveCurrentProgress(currentLevel);
        }

    }
}