using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    public GameManager.GameManagerVariables varieble;
    private TMP_Text textComponent;
    // Start is called before the first frame update
    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (varieble)
        {
            case GameManager.GameManagerVariables.TIME:
                textComponent.text = GameManager.instance.GetTime().ToString("0"); //el tostring para que devuelva solo 2 decimales en cadena
                break;
            default:
                break;
        }
    }
}
