using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "GuardState (S)", menuName = "ScripteableObjects/States/GuardState")]
public class GuardState : State
{
    public Vector3 guardPoint;

    public override State Run(GameObject owner)
    {
        State nextState = CheckActions(owner); //ejecutamos el checkactions

        NavMeshAgent NavMeshAgent = owner.GetComponent<NavMeshAgent>();
        NavMeshAgent.SetDestination(guardPoint); // asignamos su destino al punto de guardia 

        return nextState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
