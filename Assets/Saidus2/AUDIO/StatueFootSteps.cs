using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Saidus2
{
    public class StatueFootSteps : PlayFootSteps
    {
        public NavMeshManager navMeshManager;
        // Start is called before the first frame update
        void Start()
        {
            navMeshManager.Walking += StartAudio;
            navMeshManager.Stopping += StopAudio;
        }

    }
}
