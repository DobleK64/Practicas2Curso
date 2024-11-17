using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void ExitGame()
    {
        
        GameManager.instance.ExitGame();// Llama al método ExitGame en GameManager, si es necesario, para manejar lógica adicional


        Application.Quit(); //Cierra la aplicación 

        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Si estás en el editor de Unity, muestra un mensaje para indicar que la aplicación se cerró
#endif
    }

    public void LoadScene(string sceneName)
    {
        GameManager.instance.LoadScene(sceneName); //Con esto cargamos la escena 
    }
}
