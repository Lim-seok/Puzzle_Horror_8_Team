using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public float viewAngle = 45f;
    public float viewDistance = 15f;
    private Transform player;
    private Enemy enemy;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        CheckPlayerInSight();
    }

    private void CheckPlayerInSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < viewAngle / 2 && directionToPlayer.magnitude < viewDistance)
        {
            enemy.EnterAggressiveState();
        }
    }
}
