using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public NavMeshAgent agent; // Nos permite mover al jugador
    public Transform[] patrolPoints; // Puntos de patrulla
    public float detectionRange = 10f; // Rango de deteccion
    public float stopDistance = 0.5f; // Distancia para detenerse

    private PlayerStateMachine stateMachine;//referencia maquina de estado del enemigo

    private bool perseguir = false;//Indica si el enemigo debe perseguir al jugador.
    public int idIA;//Tipo de IA (1 para patrulla, 2 para persecucion)
    private float saveSpeed = 0;//guardamos speed para la id 2 de IA

    void Start()
    {
        // Comprobamos que haya puntos de patrulla
        if (patrolPoints.Length == 0)//si no tiene puntos de patrulla detiene todo
        {
            Debug.LogError("No se han asignado puntos de patrulla.");
            return;
        }
        // Obtenemos la máquina de estados y asignar referencias necesarias.
        stateMachine = GetComponent<PlayerStateMachine>();
        stateMachine.agent = agent; //Asignar el agente de navegacion.
        stateMachine.player = player; // Asignar la referencia al jugador.

        // Inicializar en estado de patrulla
        if (idIA == 1)
            stateMachine.SetState(new PatrolState(agent, player, patrolPoints));
        //configura la ia dos en estado de perseguir
        if (idIA == 2)
        {
            agent.speed = agent.speed * 0.2f; //Reducir la velocidad inicial.
            saveSpeed = agent.speed; //Guardar la velocidad para restaurarla luego.
            detectionRange = detectionRange * 200;  // Aumentar el rango de deteccion.
            perseguir = true;  //Activar el modo de persecucion.
            // Cambiar al estado de persecución si estamos fuera de este estado
            stateMachine.SetState(new ChaseState2(agent, player, stopDistance));
        }
    }

    void Update()
    {
        if (stateMachine == null) return; //Si la maquina de estados no está inicializada, salir.

        //vemos la distancia con el player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Verificamos si el jugador está dentro del rango de detección
        if (distanceToPlayer <= detectionRange && stateMachine.GetType() != typeof(ChaseState2) && idIA == 1)
        {
            perseguir = true;
            // Cambiar al estado de persecución si estamos fuera de este estado
            stateMachine.SetState(new ChaseState2(agent, player, stopDistance));
        }
        // Si el jugador no está cerca y estamos en el estado de persecución, volvemos a patrullar
        else if (distanceToPlayer > detectionRange && stateMachine.GetType() != typeof(PatrolState) && perseguir == true)
        {
            perseguir = false;
            Debug.Log(stateMachine.GetType());
            // Cambiar de nuevo al estado de patrullaje si estamos fuera de este estado
            stateMachine.SetState(new PatrolState(agent, player, patrolPoints));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //si colisionamos con el player detenemos el movimiento y si esta muy serca tambien le cargamos daño
        if (idIA == 2 && other.tag == "Player")
        {
            Debug.Log("PlayerEntro");
            agent.speed = 0; // Detener al agente.
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < 1f) // Verificar si el jugador está suficientemente cerca.
            {
                other.GetComponent<PlayerMovementRB>().life -= 10; // Reducir la vida del jugador en 10 puntos.
            }
        }
    }
    //si el player ya no nos mira y simos ID 2 seguimos persiguiendo
    private void OnTriggerExit(Collider other)
    {
        if (idIA == 2 && other.tag == "Player")
        {
            agent.speed = saveSpeed; // Restaurar la velocidad original del agente
        }
    }
}
