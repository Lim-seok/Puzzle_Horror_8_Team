using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveSlotUI : MonoBehaviour
{
    public Button[] slotButtons;
    public TextMeshProUGUI[] slotTexts;
    public SaveLoadManager saveLoadManager;
    public TMP_InputField nameInputField;
    public GameObject nameEntryPanel;

    private int selectedSlot = -1;

    private void Start()
    {
        LoadSlotInfo();
    }

    private void LoadSlotInfo()
    {
        for (int i = 0; i < slotButtons.Length; i++)
        {
            var saveData = saveLoadManager.LoadGame(i);
            slotTexts[i].text = saveData != null ? $"이름 : {saveData.playerName} \n 진행도 : {saveData.level}" : "새로운 시작";
        }
    }

    public void SelectSlot(int slot)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        selectedSlot = slot;

        var saveData = saveLoadManager.LoadGame(slot);

        if (saveData != null)
        {
            saveLoadManager.SetCurrentSlotIndex(slot);
            StartSavedGame(saveData);
        }
        else
        {
            nameEntryPanel.SetActive(true);
            saveLoadManager.SetCurrentSlotIndex(slot);
        }
    }


    public void StartNewGame()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        if (selectedSlot < 0) return;

        string playerName = nameInputField.text;
        if (string.IsNullOrEmpty(playerName)) return;

        SaveData newSaveData = new SaveData
        {
            playerName = playerName,
            level = 1,
        };

        saveLoadManager.SaveGame(newSaveData, selectedSlot);

        nameEntryPanel.SetActive(false);
        LoadSlotInfo();

        SceneManager.LoadScene("Level1");
    }

    public void DeleteSlot(int slot)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        saveLoadManager.DeleteGame(slot);
        LoadSlotInfo();
    }

    private void StartSavedGame(SaveData saveData)
    {
        SceneManager.LoadScene($"Level{saveData.level}");
    }
}
