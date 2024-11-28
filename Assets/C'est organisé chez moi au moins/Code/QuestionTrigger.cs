using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTrigger : MonoBehaviour
{
    [SerializeField] GameObject cam, player,toSpawn,todespawm;
    [SerializeField] AudioSource ass;
    [SerializeField] AudioClip monster,voi;
    [SerializeField] MeshRenderer[] allmr;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(OnEnter());
    }
    IEnumerator OnEnter()
    {
        foreach(MeshRenderer mr in allmr)
        {
            mr.enabled = false;
        }
        cam.SetActive(true);
        player.SetActive(false);
        ass.PlayOneShot(monster);
        ass.PlayOneShot(voi);
        todespawm.SetActive(false);
        yield return new WaitForSeconds(7);
        player.SetActive(true);
        cam.SetActive(false);
        Destroy(gameObject);
        toSpawn.SetActive(true);
    }
}
