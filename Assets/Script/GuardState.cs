using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "GuardState (S)", menuName = "ScripteableObjects/States/GuardState")]
public class GuardState : State
{
    public Vector3 guardPoint;
    public string blendParameter;
    public override State Run(GameObject owner)
    {
        State nextState = CheckActions(owner); //ejecutamos el checkactions

        NavMeshAgent navMeshAgent = owner.GetComponent<NavMeshAgent>();
        Animator animator = owner.GetComponent<Animator>();
        navMeshAgent.SetDestination(guardPoint); // asignamos su destino al punto de guardia 
        animator.SetFloat(blendParameter, navMeshAgent.velocity.magnitude / navMeshAgent.speed);

        return nextState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
