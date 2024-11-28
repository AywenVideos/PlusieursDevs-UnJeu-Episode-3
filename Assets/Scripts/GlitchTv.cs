using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchTv : MonoBehaviour
{
    [SerializeField] private GameObject sprite;

    public bool ON = true;

    void Start()
    {
        StartCoroutine(Glitch());
    }

    IEnumerator Glitch()
    {
        if (ON)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
            sprite.SetActive(!sprite.activeSelf);
            StartCoroutine(Glitch());
        }
        else
        {
            sprite.SetActive(false);
        }
    }
}
