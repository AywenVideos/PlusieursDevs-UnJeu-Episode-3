using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Saidus2
{

    public class HeartAudio : MonoBehaviour
    {
        public AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            audioSource.volume = GameManager.Instance.SanityNormalized;
        }
    }
}
