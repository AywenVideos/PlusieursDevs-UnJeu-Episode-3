using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Saidus2
{

    public class SanityPill : Interactable
    {
        [SerializeField]
        int sanityPillValue = 20;
        public override void Interact()
        {
            base.Interact();
            GameManager.Instance.SanityPillGrabbed(sanityPillValue);
            Destroy(gameObject);
        }

    }
}
