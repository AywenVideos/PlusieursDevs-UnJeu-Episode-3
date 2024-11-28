using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Saidus2
{   

    public class Key : Interactable
    {
        public string newObjective;

        public override void Interact()
        {
            base.Interact();
            GameManager.Instance.hasKey = true;
            ObjectivesManager.Instance.OpenObjectives(newObjective);
            Destroy(this.gameObject);
        }
    }
}