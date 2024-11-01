using UnityEngine;

public class LaserReceiver : MonoBehaviour
{
    private bool isClear = false;
    private float timeSinceLastLaser = 0f;
    public float laserTimeout = 1f;

    [SerializeField] private PuzzleSwitchCell cell;

    private void Awake()
    {
        PuzzleManager.Instance.AddPuzzleSwitch(cell);
    }


    private void Update()
    {
        timeSinceLastLaser += Time.deltaTime;
        if(timeSinceLastLaser > laserTimeout && isClear)
        {
            isClear = false;
            PuzzleManager.Instance.SetPuzzleSwitchState(cell.key, false);
        }
    }

    public void OnLaserReceived()
    {
        isClear = true;
        timeSinceLastLaser = 0f;
        CompleteQuest();
    }

    private void CompleteQuest()
    {
        PuzzleManager.Instance.SetPuzzleSwitchState(cell.key, true);
    }
}
