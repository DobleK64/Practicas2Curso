using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]


public struct StateParameters
{
    [Tooltip("Indicates if the action's check must be true or false")]
    public bool actionValue;
    [Tooltip("Action that is gonna be executed")]
    public Action action;
    [Tooltip("If the action's check equals actionValue,nextState is pushed")]
    public State NextState;

}
//Array de aciciones seria public Action[] actions;


public abstract class State : ScriptableObject
{
    public StateParameters[] stateParameters;
    protected State CheckActions(GameObject owner)
    {
        for (int i = 0; i < stateParameters.Length; i++)
        {
            if (stateParameters[i].actionValue == stateParameters[i].action.Check(owner))//el recorrer ese array chequeamos si se cumple la accion devolvemos el true si no se cumple ninguna de las acciones es false
            {
                return (stateParameters[i].NextState);
            }

        }
        return null;

        //devolvera true si alguna de sus acciones se cumple,o false si es al contrario
    }
    public abstract State Run(GameObject owner);//el run de la clase state no hace nada

    public void DrawAllActionsGizmos(GameObject  owner)
    {
        foreach (StateParameters parameter in stateParameters)
        {
            parameter.action.DrawGizmo(owner);
        }
    }

}

















