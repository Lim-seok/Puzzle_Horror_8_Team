using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelController : Singleton<LevelController>
{
    [SerializeField] private string[] LevelArray;
    private int currentLevel;

    private void Start()
    {
        currentLevel = 0;
    }

    public void LoadLevel(int level)
    {
        currentLevel = level;
        SceneManager.LoadScene(level);
    }

    public void NextLevel()
    {
            currentLevel++;
            SceneManager.LoadScene(currentLevel);
            SaveLoadManager.Instance.SaveCurrentProgress(currentLevel);

    }
}