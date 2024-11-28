using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Saidus2
{
    public class NavMeshManager : MonoBehaviour
    {
        bool walking;
        public event Action Walking;
        public event Action Stopping;
        public NavMeshAgent agent;
        private PlayerControls target;
        public bool targetAquired = false;

        [Header("NavMesh time calculations")]
        public float pathIntervalsTimeCalculations = 3f;
        public float timeBeforeStatueStopChasingPlayer = 15f;
        public bool inVision;


        [Header("Scream Propreties")]
        public float waitBetweenScreams;
        public  AudioClip screamClip;
        public float damages = 10f;
        [Range(0.5f,1.5f)]
        [SerializeField] float pitchMin, pitchMax;
        public AudioSource source;





        public bool Walk
        {
            get => walking;
            set
            {
                if(walking != value)
                {
                    walking = value;
                    if (value == true)
                    {
                        Walking?.Invoke();
                        GameManager.Instance.AddFollowingStatue();
                    }
                    else
                    {
                        Stopping?.Invoke();
                        StopAllCoroutines();
                        GameManager.Instance.SubstractFollowingStatue();
                    }
                }

            }
        }


        void Start()
        {
            GameManager.Instance.Statues.Add(this);
            if (!GameManager.Instance.IsLevelGenerated)
            {
                this.gameObject.SetActive(false);
                return;
            }
            //agent = GetComponent<NavMeshAgent>();
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
            //source = GetComponent<AudioSource>();
        }
        private void Update()
        {
            //agent.isStopped = inVision;
            Walk = !agent.isStopped;
        }

        public void Scream()
        {
            {
                print("bouh");
                GameManager.Instance.AddDamages(damages);         //TODO Decommenter apr�s debug
                source.pitch = Random.Range(pitchMin, pitchMax);
                source.PlayOneShot(screamClip);
            }
        }


        public IEnumerator PathCalculations()
        {
            StartCoroutine(timeBeforeStopChase());
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
            while (true)
            {
                if (targetAquired) agent.SetDestination(target.transform.position);
                yield return new WaitForSeconds(pathIntervalsTimeCalculations);
            }
        }

        private void OnDestroy()
        {
        }

        private IEnumerator timeBeforeStopChase()
        {
            yield return new WaitForSeconds(timeBeforeStatueStopChasingPlayer);
            targetAquired = false;
            Walk = false;

        }




    }
}
