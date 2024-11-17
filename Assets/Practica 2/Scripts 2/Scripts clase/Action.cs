using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{

    public abstract bool Check(GameObject owner); // en el check lo que va a hacer es ejecutar la accion 
    public abstract void DrawGizmo(GameObject owner);
}
