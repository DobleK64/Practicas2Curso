using System.Collections;
using System.Collections.Generic;
using TMPro; // Vamos a usar cosas del tmp
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    public GameManager.GameManagerVariables variable; // actualiza el texto a la variable de GameManager que le indiquemos
    private TMP_Text textComponent;
    public float fadeDuration = 0.5f;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        StartCoroutine(UpdateTextX());
    }

    private IEnumerator UpdateTextX()
    {
        while (true) //Bucle infinito para actualizar continuamente
        {
            string newText = "";

            switch (variable)
            {
                case GameManager.GameManagerVariables.POINTS:
                    newText = "Points: " + GameManager.instance.GetPoints();
                    break;
            }

            if (textComponent.text != newText)
            {
                
                yield return StartCoroutine(FadeTextTo(0, fadeDuration)); //Fade out


                textComponent.text = newText; //Actualiza el texto

                
                yield return StartCoroutine(FadeTextTo(1, fadeDuration)); //Fade in
            }

            yield return null; //Espera hasta el siguiente frame
        }
    }

    private IEnumerator FadeTextTo(float targetAlpha, float duration)
    {
        Color textColor = textComponent.color;
        float startAlpha = textColor.a;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            textColor.a = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            textComponent.color = textColor;
            yield return null; //Espera hasta el siguiente frame
        }

        textColor.a = targetAlpha; //Se asegura de que el color final sea el correcto
        textComponent.color = textColor;
    }
}

