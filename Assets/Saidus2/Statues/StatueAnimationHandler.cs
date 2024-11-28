using Saidus2;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class StatueAnimationHandler : MonoBehaviour
{
    public Animator animator;
    public float waitToStop = 0.5f;
    float animationTime;

    NavMeshAgent agent;
    private void Start()
    {
        if (GameManager.Instance.IsLevelGenerated)
        {
            agent = GetComponentInParent<NavMeshAgent>();
            animator = GetComponentInParent<Animator>();
            StartCoroutine(SetNewAnimation());
        }

    }
    private void OnBecameVisible()
    {
        StopAllCoroutines();
        if (gameObject.activeSelf) { StartCoroutine(PauseAnimation());
            animator.SetBool("Walking", false);
        }
        //print("on camera");
    }
    private void OnBecameInvisible()
    {
        //print("not on camera");
        StopAllCoroutines();
        if (gameObject.activeInHierarchy)
        {
            try
            {
                StartCoroutine(SetNewAnimation());
            }
            catch (Exception) { } // oui, a 21h le deuxieme jour, plus le temps de niaiser

        }
        animator.speed = 1;
    }

    private IEnumerator SetNewAnimation()
    {
        while (true)
        {
            animator.SetInteger("RandomAnim", Random.Range(0, 10));
            animator.SetBool("Walking", true);
            animationTime = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            yield return new WaitForSeconds(animationTime);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsLevelGenerated)
        {

                animator.SetBool("Walking", agent.velocity.magnitude > 0.5f);

        }
    }
    private IEnumerator PauseAnimation()
    {
        yield return new WaitForSeconds(waitToStop);
        animator.speed = 0;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
