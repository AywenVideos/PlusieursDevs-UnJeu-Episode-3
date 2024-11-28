using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Saidus2
{

    public class PlayFootSteps : MonoBehaviour
    {
        public AudioClip[] footsteps;
        public AudioSource source;
        [Range(0.5f, 1f)]
        public float pitchMin;
        [Range(1f, 1.5f)]
        public float pitchMax;

        public float timeBetweenFootsteps;

        [SerializeField] PlayerControls controls;


        internal void StartAudio()
        {
            StartCoroutine(AudioStart());
        }
       internal void StopAudio()
        {
            StopAllCoroutines();
        }

        internal IEnumerator AudioStart()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenFootsteps);
                PlayFootStep();
            }

        }

        public void PlayFootStep()
        {
            if (controls.Freeze) return;

            source.pitch = Random.Range(pitchMin, pitchMax);
            source.PlayOneShot(footsteps[Random.Range(0, footsteps.Length - 1)]);
        }
    }
}