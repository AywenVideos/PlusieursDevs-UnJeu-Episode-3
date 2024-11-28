using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Saidus2 {
    public class closetScript : Interactable
    {
        public GameObject getKeyPanel;
        public GasterScreamer closetScreamer;
        public override void Interact()
        {
            if (GameManager.Instance.hasKey)
            {
                closetScreamer.ScreamerStart();
            }
        }
        new private void Update()
        {
            if (informationPanel.activeSelf)
            {
                informationPanel.transform.LookAt(Camera.main.transform);
            }
            if (getKeyPanel.activeSelf)
            {
                getKeyPanel.transform.LookAt(Camera.main.transform);
            }
        }
        PlayerControls playerControls;
        private void OnTriggerEnter(Collider other)
        {
            playerControls = other.gameObject.GetComponent<PlayerControls>();
            if (playerControls != null)
            {
                playerControls.InteractableNearMe = this;
                if (GameManager.Instance.hasKey)
                {
                    informationPanel.SetActive(true);
                }
                else
                {
                    getKeyPanel.SetActive(true);
                }

            }
        }
        private void OnTriggerExit(Collider other)
        {
            playerControls = other.gameObject.GetComponent<PlayerControls>();
            if (playerControls != null)
            {
                playerControls.InteractableNearMe = null;
                if (GameManager.Instance.hasKey)
                {
                    informationPanel.SetActive(false);
                }
                else
                {
                    getKeyPanel.SetActive(false);
                }
            }
        }
    }
}
