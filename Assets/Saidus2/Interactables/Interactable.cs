using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Saidus2
{
    public abstract class Interactable : MonoBehaviour
    {
        public GameObject informationPanel;

        public void Update()
        {
            if (informationPanel.activeSelf)
            {
                informationPanel.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            PlayerControls playerControls = other.gameObject.GetComponent<PlayerControls>();
            if (playerControls != null)
            {
                playerControls.InteractableNearMe = this;
                informationPanel.SetActive(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            PlayerControls playerControls = other.gameObject.GetComponent<PlayerControls>();
            if (playerControls != null)
            {
                playerControls.InteractableNearMe = null;
                informationPanel.SetActive(false);
            }
        }

        public virtual void Interact()
        {
            //print("interactable Activated");
        }
    }
}
