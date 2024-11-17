using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState2 : PlayerState
{
    public float stopDistance; // Distancia a la que la IA dejara de perseguir al jugador

    public ChaseState2
        (NavMeshAgent agent, Transform player, float stopDistance) : base(agent, player) // Constructor que inicializa el agente de navegacion, el jugador y la distancia de detencion
    {
        this.stopDistance = stopDistance;
    }
    // Metodo que se llama cuando el estado comienza
    public override void EnterState()
    {
        agent.isStopped = false; // Asegura que el agente no este detenido al entrar en este estado
    }

    public override void UpdateState() // Metodo que se llama en cada frame mientras el estado esta activo
    {
        agent.SetDestination(player.position); // Actualiza la posicion de destino del agente hacia la del jugador
        // Si el agente esta a la distancia de parada o menos detiene al agente
        if (Vector3.Distance(agent.transform.position, player.position) <= stopDistance)
        {
            agent.isStopped = true; // Detiene el movimiento cuando se alcanza la distancia de parada
        }
    }
    // Metodo que se llama cuando el estado finaliza
    public override void ExitState()
    {
        agent.ResetPath(); // Resetea la ruta del agente cuando se sale de este estado
    } 
}
