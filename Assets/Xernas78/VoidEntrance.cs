using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Xernas78
{

    //By Xernas78
    public class VoidEntrance : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("NOCLIPPINNNGGGGGGGGGGGGGGGGGGGGGG");
            if (!other.CompareTag("Player")) return;
            var otherCollider = other.GetComponent<CapsuleCollider>();
            otherCollider.enabled = false;
            CharacterController controller = other.GetComponent<CharacterController>();
            controller.detectCollisions = false;
            StartCoroutine(GoToVoid());
        }

        private IEnumerator GoToVoid()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadSceneAsync("TheVoid", LoadSceneMode.Single);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

