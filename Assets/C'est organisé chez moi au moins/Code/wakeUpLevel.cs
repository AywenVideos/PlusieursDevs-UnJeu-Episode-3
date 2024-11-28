using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wakeUpLevel : MonoBehaviour
{
    [SerializeField] GameObject cam, player;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(6.3f);
        player.SetActive(true);
        cam.SetActive(false);
    }
}
