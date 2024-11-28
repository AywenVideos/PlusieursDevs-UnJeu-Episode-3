using System;
using UnityEngine;
using TMPro;

public class PlayerTombBehaviour : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"A la m�moire de\n{DialoguesManager.PlayerName}\n({DateTime.Now:dd/MM/yyyy})";
    }
}
