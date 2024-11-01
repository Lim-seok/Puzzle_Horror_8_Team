using UnityEngine;
using UnityEngine.AI;

public class ChaseCreature : CreatureBase
{
    private NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void UpdateAI()
    {
        if (CharacterManager.Instance == null || CharacterManager.Instance.Player == null)
        {
            Debug.LogError("CharacterManager or Player is not initialized.");
            return;
        }

        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);
        base.UpdateAI();
    }

    protected override void SetState(AIState state)
    {
        base.SetState(state);

        switch (aiState)
        {
            case AIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                WanderToNewLocation();
                break;
            case AIState.Attacking:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
            case AIState.Fleeing:
                agent.speed = runSpeed;
                agent.isStopped = false;
                FleeToSafeLocation();
                break;
        }

        animator.speed = agent.speed / walkSpeed;
    }

    protected override void PassiveUpdate()
    {
        base.PassiveUpdate();

        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }
    }

    protected override void AttackingUpdate()
    {
        if (playerDistance > attackDistance || !IsPlayerInFieldOfView())
        {
            agent.isStopped = false;

            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(CharacterManager.Instance.Player.transform.position, path))
            {
                agent.SetDestination(CharacterManager.Instance.Player.transform.position);
            }
            else
            {
                SetState(AIState.Fleeing);
            }
        }
        else
        {
            agent.isStopped = true;

            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                animator.speed = 1;
                animator.SetTrigger("Attack");
            }
        }
    }

    protected override void FleeingUpdate()
    {
        if (agent.remainingDistance < 0.1f)
        {
            FleeToSafeLocation();
        }
        else
        {
            SetState(AIState.Wandering);
        }
    }

    private void WanderToNewLocation()
    {
        if (aiState != AIState.Idle)
        {
            return;
        }

        SetState(AIState.Wandering);
        agent.SetDestination(GetRandomWanderLocation());
    }

    private void FleeToSafeLocation()
    {
        agent.SetDestination(GetRandomFleeLocation());
    }

    private Vector3 GetRandomFleeLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
        return hit.position;
    }

    private Vector3 GetRandomWanderLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.insideUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
        return hit.position;
    }
}
