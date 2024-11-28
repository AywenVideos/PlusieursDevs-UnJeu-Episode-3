using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
namespace Saidus2
{

    public class Door : Interactable
    {
        public GameObject getKeyPanel;
        public string levelToLoad;
        public override void Interact()
        {
            base.Interact();
            if (GameManager.Instance.hasKey)
            {
                GameManager.Instance.hasKey = false;
                SceneManager.LoadSceneAsync(levelToLoad);
            }
        }
      new  private void  Update()
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
            if (playerControls != null && getKeyPanel != null && informationPanel != null)
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
            if (playerControls != null && getKeyPanel != null && informationPanel != null)
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
