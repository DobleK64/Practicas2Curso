using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "ChaseState(S)", menuName = "ScriptableObjects/States/ChaseState")] // nos habilita la opcion de cogerlo en el create

public class ChaseState : State
{
    // Start is called before the first frame update
    public override State Run(GameObject owner)
    {
        State nextState = CheckActions(owner); //ejecutemos el checkactions 
        NavMeshAgent navMeshAgent = owner.GetComponent<NavMeshAgent>(); // el owner es el que tiene el objeto 
        GameObject target = owner.GetComponent<TargetReference>().target; // para que persiga al objetivo
        navMeshAgent.SetDestination(target.transform.position); // dice el agente que su destino es el transform del objetivo y que esquivara los obstaculos para llegar al objetivo

        return nextState;
        
    }
}
