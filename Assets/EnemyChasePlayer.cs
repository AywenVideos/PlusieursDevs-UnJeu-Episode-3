using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyChasePlayer : MonoBehaviour
{
    public Transform player; // Reference to the player’s transform
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private Animator animator; // Reference to the Animator component

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.enabled = false; // Disable NavMeshAgent until the object is enabled
    }

    private void OnEnable()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;

        agent.enabled = true; // Enable the NavMeshAgent when object is enabled
    }

    private void Update()
    {
        if (agent.enabled && player != null)
        {
            // Continuously update destination to follow the player
            agent.SetDestination(player.position);

            // Check distance to player and adjust animation
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > agent.stoppingDistance)
            {
                Debug.Log("isRunning set to " + animator.GetBool("isRunning"));
                animator.SetBool("isRunning", true); // Set running animation
            }
            else
            {
                animator.SetBool("isRunning", false); // Set idle animation when close
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            // End the game
            SceneManager.LoadSceneAsync("GameOverScene");
        }
    }

    private void OnDisable()
    {
        agent.enabled = false; // Disable NavMeshAgent if object is disabled
    }
}
