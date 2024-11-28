using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDetectionTrigger : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject player, newCam;
    private void OnTriggerEnter(Collider other)
    {
        anim.enabled = true;
        player.SetActive(false);
        newCam.SetActive(true);
    }
}
