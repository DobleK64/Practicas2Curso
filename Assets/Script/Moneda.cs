using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int value = 1;
    private int monedaTotal;
    public AudioClip monedaClip;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovementRB>()) //Al tocar el jugador la moneda se destruye
        {
            monedaTotal = GameManager.instance.GetPoints();
            monedaTotal = value + monedaTotal;
            GameManager.instance.SetPoints(monedaTotal);
            Destroy(this.gameObject);
        }
    }
}

