using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Saidus2
{
    public class NavMeshStartAndStopConditions : MonoBehaviour
    {
        public NavMeshManager navMeshManager;
        new public Renderer renderer;
        // Start is called before the first frame update
        void Start()
        {
            if (GameManager.Instance.IsLevelGenerated)
            {
                renderer = GetComponent<Renderer>();
                navMeshManager = GetComponentInParent<NavMeshManager>();
            }
        }
        private void Update()
        {
            if (!GameManager.Instance.IsLevelGenerated) return;
            if (renderer.isVisible)
            {
                
                navMeshManager.agent.isStopped = true;
                navMeshManager.inVision = true;
            }
            else
            {
                if(navMeshManager.agent.isActiveAndEnabled)
                    navMeshManager.agent.isStopped = false;

                
                navMeshManager.inVision = false;
            }
        }

        //private void OnBecameVisible()
        //{

        //}

        //private void OnBecameInvisible()
        //{

        //}


    }
}
