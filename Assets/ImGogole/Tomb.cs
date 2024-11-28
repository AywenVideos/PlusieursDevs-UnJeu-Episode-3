using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tomb : MonoBehaviour
{
    public TMP_Text messageTMPText;
    [TextArea]
    public string messageText;

    private void OnValidate()
    {
        if (messageTMPText != null)
        {
            messageTMPText.text = messageText;
        }
    }
}
