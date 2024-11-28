using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class FallingHandler : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Camera camera;

    private Volume postProcessVolume;
    private LensDistortion lensDistortion;
    private ChromaticAberration chromaticAberration;
    private PaniniProjection paniniProjection;
    private ColorAdjustments colorAdjustments;

    public float minY = 0f;
    public float maxY = -200f;
    public float minDistortion = 0f;
    public float maxDistortion = -1.1f;

    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume = camera.GetComponent<Volume>();
        postProcessVolume.profile.TryGet(out lensDistortion);
        postProcessVolume.profile.TryGet(out chromaticAberration);
        postProcessVolume.profile.TryGet(out paniniProjection);
        postProcessVolume.profile.TryGet(out colorAdjustments);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the normalized y position of the player between minY and maxY
        float normalizedY = Mathf.InverseLerp(minY, maxY, player.position.y);

        // Calculate the lens distortion value based on the normalized y position
        float highValue = Mathf.Lerp(minDistortion, maxDistortion, normalizedY);

        lensDistortion.intensity.value = highValue;
        chromaticAberration.intensity.value = highValue;
        paniniProjection.distance.value = highValue;
        colorAdjustments.postExposure.value = highValue;
        if (player.position.y <= maxY - 100)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadSceneAsync("StartMenu", LoadSceneMode.Single);
        }
    }
}
