using System.Collections;
using System.Collections.Generic;
using Saidus2;
using UnityEngine;

public class ScreamerArea : MonoBehaviour
{
    public GasterScreamer Screamer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Screamer.ScreamerStart();
    }
}
