using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public PlayerMovementRB player;
    public Slider slid;//slid con vida
    void Update()
    {
        slid.value = player.life;//representa el valor de la vida del jugador
    }
}
