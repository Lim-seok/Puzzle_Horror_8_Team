using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float detectionRange = 10f;
    public float attackRange = 2f;

    private NavMeshAgent agent;
    private Transform player;
    private int currentPatrolIndex;
    private bool isChasing;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        EnemyManager.Instance.RegisterEnemy(this); 
        SetNextPatrolPoint();
    }

    private void OnDestroy()
    {
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.UnregisterEnemy(this); 
        }
    }

    private void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
            CheckPlayerDistance();
        }
    }

    private void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNextPatrolPoint();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < attackRange)
        {
            agent.isStopped = true;
            AttackPlayer();
        }
        else if (distance > detectionRange)
        {
            isChasing = false;
            SetNextPatrolPoint();
        }
    }

    private void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < detectionRange)
        {
            isChasing = true;
        }
    }

    private void SetNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void AttackPlayer()
    {
        
    }

    public void EnterAggressiveState()
    {
        isChasing = true;
    }

}
