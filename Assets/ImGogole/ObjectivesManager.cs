using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    [SerializeField] TMP_Text objectivesDescriptionText;
    [SerializeField] GameObject objectivesPanel;

    private static ObjectivesManager instance;
    public static ObjectivesManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HideObjectives();
    }

    public void HideObjectives()
    {
        objectivesPanel.SetActive(false);
    }

    public void OpenObjectives(string message)
    {
        objectivesDescriptionText.text = message;
        objectivesPanel.SetActive(true);
    }
}
