using System.Collections;
using UnityEngine;

public enum AIState
{
    Idle,
    Wandering,
    Attacking,
    Fleeing
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
    public float safeDistance;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

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

        // CreatureManager¿¡ µî·Ï
        CreatureManager.Instance.RegisterCreature(this);
    }

    private void OnDestroy()
    {
        CreatureManager.Instance.UnregisterCreature(this);
    }

    public virtual void UpdateAI()
    {
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
            case AIState.Fleeing:
                FleeingUpdate();
                break;
        }
    }

    protected virtual void SetState(AIState state)
    {
        aiState = state;
    }

    protected virtual void PassiveUpdate()
    {
        if (playerDistance < detectDistance)
        {
            SetState(AIState.Attacking);
        }
    }

    protected virtual void AttackingUpdate() { }

    protected virtual void FleeingUpdate() { }

    protected virtual bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }
}
