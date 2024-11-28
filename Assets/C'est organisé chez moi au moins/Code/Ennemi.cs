using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Ennemi : MonoBehaviour
{
    [SerializeField] GameObject playerStat;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [Range(0, 100)]
    [SerializeField] float detectionRange;
    [Range(0, 180)]
    [SerializeField] float detectionAngle;
    private float distanceToPlayer;
    private bool chase = false;
    [SerializeField] float attackRange;
    [SerializeField] GameObject cam;
    bool playerdead = false;
    bool isDead = false;
    [SerializeField] AudioClip detectSound;
    [SerializeField] int health;
    [SerializeField] UnityEvent deaed;
    [SerializeField] AudioSource ass;
    bool fightStart = false;
    [SerializeField] GameObject fallCam;
    [SerializeField] Transform[] point;
    [SerializeField] Transform[] pointG;
    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerStat.transform.position);
        if (!isDead)
        {
            
            if (CheckAngle() && distanceToPlayer <= detectionRange && !chase)
            {
                anim.SetBool("Run", true);
                chase = true;
                
            }
            if (chase)
            {
                agent.SetDestination(playerStat.transform.position);
            }
            if (chase && distanceToPlayer <= attackRange)
            {
                if (!playerdead && !isDead)
                    StartCoroutine(Jumpscare());
            }
        }
        if (fightStart)
        {
            agent.SetDestination(playerStat.transform.position);
            if (distanceToPlayer <= attackRange)
            {
                 StartCoroutine(Jumpscare());
            }
        }
    }

     IEnumerator Jumpscare()
    {
        fightStart = false;
        if (detectSound != null)
        {
            ass.PlayOneShot(detectSound);
        }
        print("jumpscare");
        isDead = true;
        playerdead = true;
        cam.SetActive(true);
        playerStat.SetActive(false);
        
        yield return new WaitForSeconds(2);
        agent.SetDestination(point[Random.Range(0, point.Length)].position);
        chase = false;
        GameObject cm = Instantiate(fallCam, playerStat.transform.position + new Vector3(0, 0, 0), Quaternion.LookRotation(playerStat.transform.forward));
        cam.SetActive(false);
        yield return new WaitForSeconds(3);
        StartCoroutine(StartTheFight());
        Destroy(cm);
        playerStat.SetActive(true);
        detectionRange = 0;
        
    }
    IEnumerator StartTheFight()
    {
        agent.SetDestination(point[Random.Range(0, point.Length)].position);
        yield return new WaitForSeconds(1.5f);
        agent.SetDestination(pointG[Random.Range(0, pointG.Length)].position);
        yield return new WaitForSeconds(1.5f);
        agent.SetDestination(point[Random.Range(0, point.Length)].position);
        yield return new WaitForSeconds(1.5f);
        agent.SetDestination(pointG[Random.Range(0, pointG.Length)].position);
        yield return new WaitForSeconds(1.5f);
        /*
        agent.destination = point[Random.Range(0, point.Length)].position;
        yield return new WaitForSeconds(1.5f);
        agent.destination = pointG[Random.Range(0, pointG.Length)].position;
        yield return new WaitForSeconds(1.5f);
        agent.destination = point[Random.Range(0, point.Length)].position;
        yield return new WaitForSeconds(1.5f);
        agent.destination = pointG[Random.Range(0, pointG.Length)].position;
        yield return new WaitForSeconds(1.5f);*/
        fightStart = true;
    }
    public void TakeDamage(int theDamage)
    {
        if (!chase)
        {
            anim.SetBool("Run", true);
            chase = true;
            if (detectSound != null)
            {
                ass.PlayOneShot(detectSound);
            }
        }
        health -= 1;
        if (health <= 0)
        {
            deaed.Invoke();
            isDead = true;
            anim.SetTrigger("Dead");
            chase = false;
            agent.isStopped = true;
        }
    }
    bool CheckAngle()
    {

        float angle = Vector3.Angle(transform.forward, playerStat.transform.position - transform.position);
        if ((detectionAngle / 2) >= angle)
        {
            return true;
        }
        return false;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Vector3 rightBoundary = Quaternion.Euler(0, detectionAngle * 0.5f, 0) * transform.forward;
        Vector3 leftBoundary = Quaternion.Euler(0, -detectionAngle * 0.5f, 0) * transform.forward;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * detectionRange);
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * detectionRange);
    }
}
