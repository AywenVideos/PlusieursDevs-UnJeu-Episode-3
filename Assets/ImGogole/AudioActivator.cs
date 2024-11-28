using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioActivator : MonoBehaviour
{
    AudioSource audioSource;

    [Header("Audios")]
    [SerializeField] private AudioClip[] clips;

    [Header("Audios Time")]
    [SerializeField] private AudiosTime audiosTime = AudiosTime.Once;
    [SerializeField] private AudiosOrderPick audiosOrderPick = AudiosOrderPick.Random;
    int time = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audiosTime == AudiosTime.Once && time > 0) return;

            time++;

            if (audiosOrderPick == AudiosOrderPick.Random)
                audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
            else
                audioSource.PlayOneShot(clips[time % clips.Length]);
        }
    }
}

public enum AudiosTime
{
    Infinite,
    Once
}

public enum AudiosOrderPick
{
    Random,
    Increase
}