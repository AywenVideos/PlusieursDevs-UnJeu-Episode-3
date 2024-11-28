using Saidus2;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //textMeshProUGUI.text = "vous avez passé " + GameManager.Instance.levelNumber + " portes.";
        //GameManager.Instance.levelNumber = 0;
    }
    public void ChargeMenu()
    {
        SceneManager.LoadScene(0);
    }
}
