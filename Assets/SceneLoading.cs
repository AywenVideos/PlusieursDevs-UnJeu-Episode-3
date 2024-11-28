using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private List<string> tipsList;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text tipText;

    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("LevelScene");
        //operation.allowSceneActivation = false;

        tipText.text = tipsList[Random.Range(0, tipsList.Count)];

        while (operation.progress <= 1f)
        {
            int percentage = Mathf.RoundToInt(operation.progress * 100);
            progressText.text = percentage.ToString() + " %";

            yield return null;
        }
    }

}
