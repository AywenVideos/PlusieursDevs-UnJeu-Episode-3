using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlenderTrigger : MonoBehaviour
{
    public GameObject slenderman;

    private bool hasBeenActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenActivated)
        {
            // Activate the Slenderman
            slenderman.SetActive(true);
            print("Slenderman is now chasing the player");
            hasBeenActivated = true; 
        }
    }
}
