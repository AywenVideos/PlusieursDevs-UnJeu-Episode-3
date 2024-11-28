using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomDetection : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] AudioClip momWord;
    [SerializeField] AudioSource momMouse;
    bool spoke = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!spoke)
        {
            spoke = true;
            momMouse.PlayOneShot(momWord);
            anim.SetBool("talk", true);
        }
    }
}
