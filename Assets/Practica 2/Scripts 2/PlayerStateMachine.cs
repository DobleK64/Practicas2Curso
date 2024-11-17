using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState currentState;
    public NavMeshAgent agent;
    public Transform player;

    public void SetState(PlayerState newState) //Metodo para cambiar el estado del jugador.
    {
        if (currentState != null)
            currentState.ExitState(); //Si hay un estado actual activo llama a su método de salida.

        currentState = newState; //Cambia el estado actual al nuevo estado.

        if (currentState != null)
            currentState.EnterState(); //Si el nuevo estado no es nulo llama a su metodo de entrada.
    }

    void Update()
    {
        if (currentState != null)
            currentState.UpdateState(); //Si hay un estado actual llama a su metodo de actualizacion.
    }

}

