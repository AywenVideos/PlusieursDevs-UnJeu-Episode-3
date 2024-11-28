using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguesArea : MonoBehaviour
{
    [SerializeField] private TextAsset dialoguesTextAsset;

    [Header("Dialogues Location")]
    [SerializeField] private DialoguesLocation dialoguesLocationMethod = DialoguesLocation.DontControl;
    [SerializeField] private Transform beginLookAt;

    [Header("Dialogues Time")]
    [SerializeField] private DialoguesTime dialoguesTime = DialoguesTime.Once;
    int time = 0;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("SomethingEntered");
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerEntered");
            if (dialoguesTime == DialoguesTime.Once && time > 0) return;

            time++;

            if (dialoguesLocationMethod == DialoguesLocation.ForceLookAt)
                DialoguesManager.Instance.BeginDialogues(dialoguesTextAsset, beginLookAt.position);
            else
                DialoguesManager.Instance.BeginDialogues(dialoguesTextAsset);
        }
    }
}

public enum DialoguesLocation
{
    ForceLookAt, // force instanement le regard
    DontControl // laissez les autres scripts le faire
}

public enum DialoguesTime
{
    Once,
    Infinite
}