using UnityEngine;

public interface ILaserParts
{
    public void OnLaserHit();
    public void OnLaserMiss();
}

public class LaserReceiver : MonoBehaviour, ILaserParts
{
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

    public void OnLaserHit()
    {
        if (PuzzleManager.Instance.puzzleSwitch.ContainsKey("Laser"))
        {
            if (!PuzzleManager.Instance.puzzleSwitch["Laser"].state)
            {
                PuzzleManager.Instance.SetPuzzleSwitchState(cell.key, true);
                clearPaticle = ParticleManager.Instance.SpawnParticle("LaserReceiver", particlePosition, Quaternion.identity);
            }
        }
    }

    public void OnLaserMiss()
    {
        if (PuzzleManager.Instance.puzzleSwitch.ContainsKey("Laser"))
        {
            if (PuzzleManager.Instance.puzzleSwitch["Laser"].state)
            {
                PuzzleManager.Instance.SetPuzzleSwitchState(cell.key, false);
                Destroy(clearPaticle);
            }
        }
    }
}
