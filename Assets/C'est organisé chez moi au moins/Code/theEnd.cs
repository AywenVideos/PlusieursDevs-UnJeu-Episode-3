using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class theEnd : MonoBehaviour
{
    [SerializeField] AudioClip tpp;
    [SerializeField] AudioSource ass;
    [SerializeField] GameObject player, cam;
    private void OnTriggerEnter(Collider other)
    {
        ass.enabled = false;
        cam.SetActive(true);
        player.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(tpp);
        StartCoroutine(Ta());
    }
    IEnumerator Ta()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("StartMenu");
    }
}
