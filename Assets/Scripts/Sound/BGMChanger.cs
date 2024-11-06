using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMChanger : MonoBehaviour
{
    void Start()
    {
        SetBGMForCurrentScene();
    }

    private void SetBGMForCurrentScene()
    {

        string sceneName = SceneManager.GetActiveScene().name;
        AudioClip newBgmClip = Resources.Load<AudioClip>($"Audio/BGM/{sceneName}");

        if (newBgmClip != null)
        {
            AudioManager.Instance.PlayBGM(newBgmClip);
        }

    }
}
