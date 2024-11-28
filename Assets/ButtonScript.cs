using System.Collections;
using System.Collections.Generic;
using Saidus2;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : Interactable
{
    public GameObject getKeyPanel;
    public GameObject door;
    public bool needKey;
    public override void Interact()
    {
        print("Interact");
        base.Interact();
        if (needKey && GameManager.Instance.hasKey)
        {
            print("needkey");
            GameManager.Instance.hasKey = false;
            door.SetActive(!door.activeSelf);
        } else if(needKey == false)
        {
            print("donotneedkey");
            door.SetActive(!door.activeSelf);
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
