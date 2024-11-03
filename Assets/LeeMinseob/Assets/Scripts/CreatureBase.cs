using UnityEngine;

public enum AIState
{
    Idle,
    Wandering,
    Attacking,
    Chasing
}

public class CreatureBase : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;

    [Header("AI")]
    protected AIState aiState;
    public float detectDistance;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;
    private float idleTime;

    [Header("Combat")]
    public int damage;
    public float attackRate;
    protected float lastAttackTime;
    public float attackDistance;

    protected float playerDistance;

    public float fieldOfView = 120f;

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        CreatureManager.Instance.RegisterCreature(this);
    }

    private void OnDestroy()
    {
        CreatureManager.Instance.UnregisterCreature(this);
    }

    public virtual void UpdateAI()
    {
        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);

        animator.SetBool("Moving", aiState != AIState.Idle);

        switch (aiState)
        {
            case AIState.Idle:
                PassiveUpdate();
                break;
            case AIState.Wandering:
                PassiveUpdate();
                break;
            case AIState.Attacking:
                AttackingUpdate();
                break;
        }
    }

    protected virtual void SetState(AIState state)
    {
        aiState = state;
    }

    protected virtual void PassiveUpdate()
    {
        if (aiState == AIState.Idle && Time.time >= idleTime)
        {
            SetState(AIState.Wandering);
        }

        if (playerDistance < detectDistance && IsPlayerInFieldOfView())
        {
            SetState(AIState.Chasing);
        }
    }

    protected virtual void AttackingUpdate() { }

    protected virtual bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }
}
