using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasterEgg : MonoBehaviour
{
    public GameObject informationPanel;
    public string levelToLoad;
    bool isIn = false;
    void Update()
    {
        if(isIn&& Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            informationPanel.SetActive(true);
            isIn =true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            informationPanel.SetActive(false);
            isIn = false;
        }
    }
}
