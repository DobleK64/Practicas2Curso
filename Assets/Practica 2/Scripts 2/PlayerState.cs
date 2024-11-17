using UnityEngine;
using UnityEngine.AI;

public abstract class PlayerState
{
    //clase obstracta de playerState con las funciones abstractas
    protected NavMeshAgent agent;
    protected Transform player;

    public PlayerState(NavMeshAgent agent, Transform player)
    {
        this.agent = agent;
        this.player = player;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
