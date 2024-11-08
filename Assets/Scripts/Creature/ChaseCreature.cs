using UnityEngine;
using UnityEngine.AI;

public class ChaseCreature : CreatureBase
{
    private NavMeshAgent agent;
    public JumpScare jumpScare;
    private Collider attackCollider;


    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();

        attackCollider = GetComponent<Collider>();
        attackCollider.enabled = false;
    }

    public override void UpdateAI()
    {
        if (CharacterManager.Instance == null || CharacterManager.Instance.Player == null)
        {
            return;
        }

        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);
        base.UpdateAI();

        if (aiState == AIState.Chasing)
        {
            ChasingUpdate();
        }
    }

    protected override void SetState(AIState state)
    {
        base.SetState(state);

        switch (aiState)
        {
            case AIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                animator.SetBool("Moving", false);
                animator.SetBool("Chase", false); 
                break;
            case AIState.Wandering:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                animator.SetBool("Moving", true);
                animator.SetBool("Chase", false);  
                WanderToNewLocation();
                break;
            case AIState.Attacking:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                animator.SetBool("Chase", false); 
                animator.SetTrigger("Attack"); 
                break;
            case AIState.Chasing:
                agent.speed = runSpeed;
                agent.isStopped = false;
                animator.SetBool("Moving", false);  
                animator.SetBool("Chase", true);    
                break;
        }

        animator.speed = agent.speed / walkSpeed;
    }

    private void ChasingUpdate()
    {
        if (playerDistance <= detectDistance && IsPlayerInFieldOfView())
        {
            agent.SetDestination(CharacterManager.Instance.Player.transform.position);

            if (playerDistance <= attackDistance)
            {
                SetState(AIState.Attacking);
            }
        }
        else
        {
            SetState(AIState.Idle);
        }
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
            agent.SetDestination(CharacterManager.Instance.Player.transform.position);
            SetState(AIState.Chasing);
        }
        else
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate)
            {
                IsObstacleInPath();
                lastAttackTime = Time.time;
                animator.speed = 1;
                animator.SetTrigger("Attack");

                jumpScare.TriggerJumpScare();
                GetComponent<ChaseCreatureSound>().PlayJumpScareSound(0);

                attackCollider.enabled = true;
                Invoke("DisableAttackCollider", 0.2f);
            }
        }
    }

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        agent.isStopped = true;

        if (other.CompareTag("Player") && aiState == AIState.Attacking)
        {
            agent.isStopped = true;
            SetState(AIState.Attacking);
            jumpScare.TriggerJumpScare();
        }
    }

    private void WanderToNewLocation()
    {
        if (aiState != AIState.Idle) return;
        SetState(AIState.Wandering);
        agent.SetDestination(GetRandomWanderLocation());
    }

    private Vector3 GetRandomWanderLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.insideUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
        return hit.position;
    }

    private bool IsObstacleInPath()
    {
        Ray ray = new Ray(transform.position, (CharacterManager.Instance.Player.transform.position - transform.position).normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return true;
            }
        }

        return false;
    }
}
