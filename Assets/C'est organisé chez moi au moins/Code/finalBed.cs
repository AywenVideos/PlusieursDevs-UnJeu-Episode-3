using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBed : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject cam;
    bool enter = false;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip thunder;
    [SerializeField] GameObject deadTree, goodTree , alienDetectionTrigger,alien,mom;


    private void OnTriggerEnter(Collider other)
    {
        if (!enter)
        {
            enter = true;
            StartCoroutine(Enter());
        }
    }
    IEnumerator Enter()
    {
        player.SetActive(false);
        cam.SetActive(true);
        yield return new WaitForSeconds(3);
        mom.SetActive(false);
        alien.SetActive(true);
        player.SetActive(true);
        cam.SetActive(false);

        goodTree.SetActive(false);
        deadTree.SetActive(true);
        audioSource.enabled = false;
        AudioSource ass = GetComponent<AudioSource>();
        ass.enabled = true;
        ass.PlayOneShot(thunder);
        alienDetectionTrigger.SetActive(true);
    }
}
