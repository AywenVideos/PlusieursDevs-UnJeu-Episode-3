using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Saidus2
{

    public class ScreamArea : MonoBehaviour
    {
        SphereCollider sphereCollider;
        NavMeshManager manager;
        NavMeshAgent agent;
        // Start is called before the first frame update
        void Start()
        {
            sphereCollider = GetComponent<SphereCollider>();
            manager = GetComponentInParent<NavMeshManager>();
            agent = GetComponentInParent<NavMeshAgent>();
            sphereCollider.radius = agent.stoppingDistance + 0.5f;
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerControls>();
            if(player != null)
            {
                if (!manager.inVision)
                {
                    manager.Scream();
                }
            }
        }
        //private void OnTriggerExit(Collider other)
        //{
        //    var player = other.GetComponent<PlayerControls>();
        //    if (player != null)
        //    {
        //        //StopAllCoroutines();
        //    }
        //}

        private IEnumerator LaunchScream()
        {
            while (true)
            {
                manager.Scream();
                yield return new WaitForSeconds(manager.waitBetweenScreams);
            }
        }
    }
}