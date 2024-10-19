using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HearAction(A)", menuName = "ScriptableObject/Action/HearAction")]
public class HearAction : Action
{
    public float radius = 20f;
    public override bool Check(GameObject owner)
    {

        RaycastHit[] hits = Physics.SphereCastAll(owner.transform.position, radius, Vector3.up);//castear el rayo de la esfera con el radio que se le haya dado
        GameObject target = owner.GetComponent<TargetReference>().target;//accedemos al target


        foreach (RaycastHit hit in hits)//recorremos todos los objetos que esten el el circulo y si estan el target devuelve true 
        {
            if (hit.collider.gameObject == target)
            {
                return true;
                //le hemnos escuchado/oler
            }
        }
        return false;

    }
 public override void DrawGizmo(GameObject owner)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(owner.transform.position, radius);
    }



}
