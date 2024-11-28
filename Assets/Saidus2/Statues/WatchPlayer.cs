using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Saidus2
{
    public class WatchPlayer : MonoBehaviour
    {
        public NavMeshManager manager;

        private void Start()
        {
            if(GameManager.Instance.IsLevelGenerated)
            {
                manager = GetComponentInParent<NavMeshManager>();
            }

        }
        private void OnTriggerEnter(Collider other)
        {
           PlayerControls player = other.GetComponent<PlayerControls>();
            if(player != null)
            {
                print("target aquired");
                manager.targetAquired = true;
            }
        }
        private void OnTriggerExit(Collider other)  
        {
            PlayerControls player = other.GetComponent<PlayerControls>();
            if (player != null)
            {
                //print("target aquired exit");
                manager =  GetComponentInParent<NavMeshManager>();
                manager.targetAquired = true;
                manager.StartCoroutine(manager.PathCalculations());
            }
        }
    }
}
