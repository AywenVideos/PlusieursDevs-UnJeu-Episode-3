using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gasterEnabler : MonoBehaviour
{
    public GasterScreamer gasterScript;

    private void OnTriggerEnter(Collider other)
    {
        gasterScript.ScreamerStart();
    }
}
