using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Variables publiques pour ajuster la dur�e et l'intensit� du tremblement
    public float shakeDuration = 2f;  // Dur�e du tremblement en secondes (x)
    public float shakeIntensity = 0.5f;  // Intensit� du tremblement (y)

    private float shakeTimeRemaining;  // Temps restant pour le tremblement
    private Vector3 originalPosition;  // Position d'origine de la cam�ra

    void Start()
    {
        // Sauvegarde de la position d'origine de la cam�ra
        originalPosition = transform.localPosition;
        shakeTimeRemaining = shakeDuration;
    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            // G�n�re un d�placement al�atoire autour de la position d'origine avec l'intensit� sp�cifi�e
            Vector3 shakeOffset = UnityEngine.Random.insideUnitSphere * shakeIntensity;

            // Applique l'offset � la cam�ra
            transform.localPosition = originalPosition + shakeOffset;

            // R�duit le temps restant
            shakeTimeRemaining -= Time.deltaTime;

            // Si le temps de tremblement est �coul�, r�initialise la position de la cam�ra
            if (shakeTimeRemaining <= 0f)
            {
                transform.localPosition = originalPosition;
            }
        }
    }

    // M�thode pour d�marrer le tremblement avec des param�tres personnalis�s
    public void StartShake(float duration, float intensity)
    {
        shakeDuration = duration;
        shakeIntensity = intensity;
        shakeTimeRemaining = shakeDuration;
    }
}
