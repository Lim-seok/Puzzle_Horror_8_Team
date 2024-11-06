using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : Singleton<LevelController>
{
    public int currentLevel;

    public void NextLevel()
    {
        currentLevel++;
        SaveLoadManager.Instance.SaveCurrentProgress(currentLevel);
        SceneManager.LoadScene($"Level{currentLevel}");
        PuzzleManager.Instance.InitializePuzzle();
    }
}