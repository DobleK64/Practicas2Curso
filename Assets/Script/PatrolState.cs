using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PatrolState : PlayerState
{
    private Transform[] patrolPoints; // Puntos de patrulla
    private int currentPointIndex = 0; // Índice del punto actual
    private bool isPatrolling = false; // Bandera para controlar el patrullaje

    public PatrolState(NavMeshAgent agent, Transform player, Transform[] patrolPoints) : base(agent, player)
    {
        this.patrolPoints = patrolPoints;
    }

    public override void EnterState()
    {
       // Debug.Log("Arrancamos");
        if (patrolPoints.Length > 0)
        {
            // Empezar el patrullaje al primer punto
            isPatrolling = true;
            agent.SetDestination(patrolPoints[currentPointIndex].position);

            // Avanza al siguiente punto, regresando al inicio si es el último
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    public override void UpdateState()
    {
        if (!isPatrolling) return;
        //Debug.Log(agent.remainingDistance + "punto: " + currentPointIndex);
        // Si ha llegado al punto actual, avanzamos al siguiente
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            //Debug.Log("Llegó al punto de patrullaje " + currentPointIndex);
            agent.SetDestination(patrolPoints[currentPointIndex].position);

            // Avanza al siguiente punto, regresando al inicio si es el último
            currentPointIndex = 2;
        }
    }

    public override void ExitState()
    {
        agent.ResetPath(); // Detenemos el agente al salir del estado
        //Debug.Log("Saliendo del estado de patrullaje.");
    }

}
