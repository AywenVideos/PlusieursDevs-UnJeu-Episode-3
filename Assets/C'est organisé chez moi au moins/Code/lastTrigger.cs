using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastTrigger : MonoBehaviour
{
    [SerializeField] AudioClip glasseBreak;
    [SerializeField] AudioSource ass;
    [SerializeField] GameObject toDespawn,toSpawn;
    bool enter = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!enter)
        {
            enter = true;
            ass.PlayOneShot(glasseBreak);
            toDespawn.SetActive(false);
            toSpawn.SetActive(true);
        }
    }
}
