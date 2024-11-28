
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FinalLevelManager : MonoBehaviour
{
    public GameObject startLevel;
    public GameObject player;
    void Start()
    {
        StartCoroutine(Crash());
    }
    IEnumerator Crash()
    {
        yield return new WaitForSeconds(10);
        player.SetActive(true);
        startLevel.SetActive(false);
        GetComponent<AudioSource>().enabled = false;
    }
}
