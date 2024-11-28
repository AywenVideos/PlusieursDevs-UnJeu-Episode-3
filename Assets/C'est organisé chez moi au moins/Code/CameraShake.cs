using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Variables publiques pour ajuster la durée et l'intensité du tremblement
    public float shakeDuration = 2f;  // Durée du tremblement en secondes (x)
    public float shakeIntensity = 0.5f;  // Intensité du tremblement (y)

    private float shakeTimeRemaining;  // Temps restant pour le tremblement
    private Vector3 originalPosition;  // Position d'origine de la caméra

    void Start()
    {
        // Sauvegarde de la position d'origine de la caméra
        originalPosition = transform.localPosition;
        shakeTimeRemaining = shakeDuration;
    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            // Génère un déplacement aléatoire autour de la position d'origine avec l'intensité spécifiée
            Vector3 shakeOffset = UnityEngine.Random.insideUnitSphere * shakeIntensity;

            // Applique l'offset à la caméra
            transform.localPosition = originalPosition + shakeOffset;

            // Réduit le temps restant
            shakeTimeRemaining -= Time.deltaTime;

            // Si le temps de tremblement est écoulé, réinitialise la position de la caméra
            if (shakeTimeRemaining <= 0f)
            {
                transform.localPosition = originalPosition;
            }
        }
    }

    // Méthode pour démarrer le tremblement avec des paramètres personnalisés
    public void StartShake(float duration, float intensity)
    {
        shakeDuration = duration;
        shakeIntensity = intensity;
        shakeTimeRemaining = shakeDuration;
    }
}
