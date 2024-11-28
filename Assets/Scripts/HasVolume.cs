using System;
using UnityEngine;

namespace Saidus2
{
    public class HasVolume : MonoBehaviour
    {

        private AudioSource[] audioSources;

        [SerializeField] private bool master;

        private void Start()
        {
            audioSources = GetComponents<AudioSource>();
        }

        private void Update()
        {
            foreach (AudioSource audioSource in audioSources)
            {
                if (master)
                {
                    audioSource.volume = VolumeManager.MasterVolume;
                }
                else
                {
                    audioSource.volume = VolumeManager.AmbientVolume;
                }
            }
        }
    }
}