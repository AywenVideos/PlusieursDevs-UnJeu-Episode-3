using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalDoor : MonoBehaviour
{
    [SerializeField] AudioClip spotlightSE;
    [SerializeField] AudioSource ass;
    [SerializeField] Transform player;
    [SerializeField] GameObject spotLight;
    [SerializeField] float ecartMin;
    float distanceX;
    private void Update()
    {
        distanceX = player.position.z - transform.position.z;
        if(distanceX<= ecartMin)
        {
            transform.position += new Vector3(0, 0, -6f);
            Instantiate(spotLight, transform.position + new Vector3(5, 2, -2f), Quaternion.identity);
            Instantiate(spotLight, transform.position + new Vector3(0, 2, -4f), Quaternion.identity);
            Instantiate(spotLight, transform.position + new Vector3(0, 2, -5f), Quaternion.identity);
            ass.PlayOneShot(spotlightSE);
        }
    }
}
