using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "ChaseState(S)", menuName = "ScriptableObjects/States/ChaseState")] // nos habilita la opcion de cogerlo en el create

public class ChaseState : State
{
    public string blendParameter;
    // Start is called before the first frame update
    public override State Run(GameObject owner)
    {
        State nextState = CheckActions(owner); // Ejecutemos el CheckActions.
        NavMeshAgent navMeshAgent = owner.GetComponent<NavMeshAgent>(); // El owner es el que tiene el objeto.
        Animator animator = owner.GetComponent<Animator>();
        GameObject target = owner.GetComponent<TargetReference>().target; // Para que persiga al objetivo.

        // Calculamos la distancia al objetivo.
        float distanceToTarget = Vector3.Distance(owner.transform.position, target.transform.position);

        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            // Si estamos lejos, seguimos moviéndonos hacia el objetivo.
            navMeshAgent.SetDestination(target.transform.position);
            animator.SetFloat(blendParameter, navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        }
        else
        {
            // Si estamos dentro del rango, detenemos el movimiento.
            navMeshAgent.SetDestination(owner.transform.position);
            animator.SetFloat(blendParameter, 0); // Detenemos la animación de movimiento.
        }

        return nextState;
    }
}
