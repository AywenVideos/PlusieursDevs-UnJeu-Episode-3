using System.Collections;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip audioToPlay;
    public float repeatEach = 15.0f;
    public float firstEmission = 0.0f;

    private AudioSource audioSource;
    private bool isPlaying = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(repeatEach + firstEmission);
        while (true)
        {
            if (audioToPlay != null)
            {
                audioSource.PlayOneShot(audioToPlay);
            }

            yield return new WaitForSeconds(repeatEach);
        }
    }
}
