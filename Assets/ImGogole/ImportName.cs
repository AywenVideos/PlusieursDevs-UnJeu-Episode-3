using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportName : MonoBehaviour
{
    [SerializeField] string defautName;

    private void Start()
    {
        DialoguesManager.PlayerName = defautName;
    }

    public void OnNameChanged(string name)
    {
        if (name.Length > 0)
            DialoguesManager.PlayerName = name;
        else
            DialoguesManager.PlayerName = defautName;
    }
}
