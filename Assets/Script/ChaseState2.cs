using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState2 : PlayerState
{
    public float stopDistance;

    public ChaseState2
        (NavMeshAgent agent, Transform player, float stopDistance) : base(agent, player)
    {
        this.stopDistance = stopDistance;
    }

    public override void EnterState()
    {
        agent.isStopped = false;
    }

    public override void UpdateState()
    {
        agent.SetDestination(player.position);

        if (Vector3.Distance(agent.transform.position, player.position) <= stopDistance)
        {
            agent.isStopped = true;
        }
    }

    public override void ExitState()
    {
        agent.ResetPath();
    }
}
