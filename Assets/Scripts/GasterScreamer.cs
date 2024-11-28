using System.Collections;
using Saidus2;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GasterScreamer : MonoBehaviour
{
    public GameObject canvas;
    [SerializeField] PlayerControls playerControls;
    public TMP_Text childText;
    public string fullText;
    public float shakeDuration = 4f;
    public GameObject objectToDisable;
    public GameObject objectToEnable;
    public Image image;
    public AudioClip screamerSound;

    //public AudioClip webDriverTorso;

    public string sceneToLoad;

    private AudioSource audioSource;
    private float delay = 0.1f;
    public float shakeMagnitude = 150f;
    private Color glitchColor = new Color(1, 0, 0, 0.5f);
    private string currentText = "";

    public bool enableText = true;

    [Space(10)]
    [SerializeField] private TextAsset dialogue0;

    [SerializeField] private GameObject bed;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }


    public void ScreamerStart()
    {
        playerControls.Freeze = true;
        objectToDisable.SetActive(false);
        //audioSource.PlayOneShot(webDriverTorso);
        canvas.SetActive(true);
        if (enableText == true)
        {
            StartCoroutine(ShowText());
        }
        StartCoroutine(Effects());
    }

    private IEnumerator Effects()
    {
        if (enableText == true)
        {
            yield return new WaitForSeconds(9);
        }

        audioSource.PlayOneShot(screamerSound, 0.5f);
        objectToEnable.SetActive(true);
        StartCoroutine(ShakeCoroutine());
        StartCoroutine(GlitchCoroutine());
        yield return new WaitForSeconds(shakeDuration+0.2f);
        objectToEnable.SetActive(false);
        canvas.SetActive(false);
        currentText = "";
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        //SceneManager.LoadSceneAsync(sceneToLoad);

        objectToDisable.SetActive(true);
        playerControls.Freeze = false;

        DialoguesManager.Instance.BeginDialogues(dialogue0);
        ObjectivesManager.Instance.OpenObjectives("Dormir");
        bed.SetActive(true);
    }

    private IEnumerator GlitchCoroutine()
    {
        while (true)
        {
            image.color = glitchColor;
            yield return new WaitForSeconds(0.1f);

            image.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            childText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalPosition = image.transform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = Random.Range(-shakeMagnitude, shakeMagnitude);

            
            image.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        image.transform.localPosition = originalPosition;
    }
}