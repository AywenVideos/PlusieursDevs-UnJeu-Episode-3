using System.Collections;
using UnityEngine;

public class CracklingLightBehaviour : MonoBehaviour
{
    private Light light_;
    private AudioSource audio_;

    [SerializeField] private MeshRenderer meshRenderer;

    Coroutine coroutine;

    void Start()
    {
        light_ = GetComponent<Light>();
        audio_ = GetComponentInChildren<AudioSource>();

        EmergencyCall.Instance.OnExtinction += OnExtinction;

        coroutine = StartCoroutine(CracklingCoroutine());
    }

    private void OnDestroy()
    {
        EmergencyCall.Instance.OnExtinction -= OnExtinction;
    }

    private void OnExtinction()
    {
        StopCoroutine(coroutine);
        light_.enabled = false;
        meshRenderer.enabled = false;
        audio_.enabled = false;
    }

    IEnumerator CracklingCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 3f));
            light_.enabled = false;
            meshRenderer.enabled = false;
            audio_.volume = 0f;
            yield return null;
            yield return null;
            light_.enabled = true;
            meshRenderer.enabled= true;
            audio_.volume = 1f;
        }
    }
}
