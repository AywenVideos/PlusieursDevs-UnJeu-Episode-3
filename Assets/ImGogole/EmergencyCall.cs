using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyCall : MonoBehaviour
{
    private static EmergencyCall instance;
    public static EmergencyCall Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip bipSound;
    [SerializeField] AudioClip pickUpSound;
    [SerializeField] AudioClip screamerSound;

    public event Action OnExtinction;

    private void Awake()
    {
        instance = this;
    }

    public void PickUp()
    {
        audioSource.PlayOneShot(pickUpSound);
    }

    public void Screamer()
    {
        audioSource.PlayOneShot(screamerSound);
        OnExtinction?.Invoke();
    }

    public void Bip(float duration)
    {
        StartCoroutine(Bip_Coroutine(duration));
    }

    IEnumerator Bip_Coroutine(float duration)
    {
        audioSource.clip = bipSound;
        audioSource.loop = true;
        audioSource.Play();

        yield return new WaitForSeconds(duration);

        audioSource.Stop();
    }
}
