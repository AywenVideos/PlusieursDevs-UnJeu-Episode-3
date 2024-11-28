using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpiritSpawn : MonoBehaviour
{
    private bool isIn;
    [SerializeField] private AudioClip[] soundList;
    [SerializeField] private AudioSource sourceAudio;
    [SerializeField] private GameObject spirit;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            isIn = true;
            StartCoroutine(SpawnSpirit());
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            isIn = false;
        }
    }
    IEnumerator SpawnSpirit()
    {
        sourceAudio.clip = soundList[Random.Range(0,soundList.Length)];
        sourceAudio.Play();
        Instantiate(spirit,new Vector3(gameObject.transform.position.x+ Random.Range(-15,15),1.5f,gameObject.transform.position.z+Random.Range(-15,15)),Quaternion.identity,this.gameObject.transform);
        yield return new WaitForSeconds(Random.Range(7,12));
        StartCoroutine(SpawnSpirit());
    }
}
