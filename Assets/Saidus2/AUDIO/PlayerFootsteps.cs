using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Saidus2
{
    public class PlayerFootsteps : PlayFootSteps
    {

        CharacterController characterController;
        private void Start()
        {
            characterController = GetComponentInParent<CharacterController>();
        }


        bool audioIsPlaying = false;

        //oui c'est pas stonks comme solution d'utiliser l'update, mais bon a 6h du mat si ça marche je vais pas me prendre le chou ^^
        //l'autre dev: et bien dommage pour toi je vais detruire ce code pour l'améliorer (ps il est aussi 5h du sbah chez moi)
        //encore un autre dev: eheheheh, je l'ai encore modif, cheh
        private void Update()
        {
            if (characterController.isGrounded && new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude > 0.5f && !audioIsPlaying)
            {
                GetComponent<Animator>().speed = 1;
                audioIsPlaying = true;
            }
            else
            {
                GetComponent<Animator>().speed = 0;
                audioIsPlaying = false;
            }
        }
    }
}
