using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private Light buttonLight;
    private bool isOn = false;
    private int buttonIndex;

    private void Awake()
    {
        buttonLight = GetComponentInChildren<Light>();
    }

    private void Start()
    {
        UpdateLight();
    }

    public void ToggleState()
    {
        isOn = !isOn;
        UpdateLight();
        GameManager.Instance.UpdateButtonCount(isOn);
    }

    public void SetState(bool state)
    {
        isOn = state;
        UpdateLight();
    }

    private void UpdateLight()
    {
        buttonLight.enabled = isOn;
    }

    public void SetIndex(int index)
    {
        buttonIndex = index;
    }

    public void OnButtonInteract()
    {
        GameManager.Instance.ToggleAdjacentButtons(buttonIndex);
    }
}
