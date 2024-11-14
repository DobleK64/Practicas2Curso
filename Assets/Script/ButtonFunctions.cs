using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void ExitGame()
    {
        
        GameManager.instance.ExitGame();// Llama al m�todo ExitGame en GameManager, si es necesario, para manejar l�gica adicional


        Application.Quit(); //Cierra la aplicaci�n 

        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Si est�s en el editor de Unity, muestra un mensaje para indicar que la aplicaci�n se cerr�
#endif
    }

    public void LoadScene(string sceneName)
    {
        GameManager.instance.LoadScene(sceneName); //Con esto cargamos la escena 
    }
}
