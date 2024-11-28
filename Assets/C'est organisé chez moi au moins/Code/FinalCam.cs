using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCam : MonoBehaviour
{
    [SerializeField] AudioClip punch,coucou,tp;
    [SerializeField] GameObject bro;
    public void PlayTheSound()
    {
        GetComponent<AudioSource>().PlayOneShot(punch);
    }
    public void SpawnBro()
    {
        bro.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(tp);
        StartCoroutine(Broo());
    }
    IEnumerator Broo()
    {
        yield return new WaitForSeconds(1.3f);
        GetComponent<AudioSource>().PlayOneShot(coucou);
        SceneManager.LoadScene("lastLevel");
    }
}
