using UnityEngine;

public class LaserReceiver : MonoBehaviour
{
    private bool isClear = false;
    private float timeSinceLastLaser = 0f;
    public float laserTimeout = 1f;

    private GameObject clearPaticle;
    private Vector3 particlePosition;

    [SerializeField] private PuzzleSwitchCell cell;

    private void Awake()
    {
        PuzzleManager.Instance.AddPuzzleSwitch(cell);
    }

    private void Start()
    {
        particlePosition = transform.position;
        particlePosition.y -= 1f;
    }

    private void Update()
    {
        timeSinceLastLaser += Time.deltaTime;
        if (timeSinceLastLaser > laserTimeout && isClear)
        {
            isClear = false;
            PuzzleManager.Instance.SetPuzzleSwitchState(cell.key, false);
            Destroy(clearPaticle);
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
        if (PuzzleManager.Instance.puzzleSwitch.ContainsKey("Laser") && PuzzleManager.Instance.puzzleSwitch["Laser"].state)
        {
            return;
        }

        PuzzleManager.Instance.SetPuzzleSwitchState(cell.key, true);
        clearPaticle = ParticleManager.Instance.SpawnParticle("LaserReceiver", particlePosition, Quaternion.identity);
    }
}
