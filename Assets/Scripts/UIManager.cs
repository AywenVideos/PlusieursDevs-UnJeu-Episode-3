using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    public GameObject UICanvas;
    public GameObject InGameUi;

    public GameObject PauseMenu;
    public GameObject AchievementsMenu;

    public PauseManager Pause;
    public AchievementsManager Achievements;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        print(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Pause = GetComponent<PauseManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenPauseMenu()
    {
        Debug.Log("Opened");
        Pause.SetPause(true);
        InGameUi.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenu.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        Debug.Log("ClosePauseMenu");
        Pause.SetPause(false);
        InGameUi.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.SetActive(false);
    }

    public void OpenAchievementsMenu()
    {
        PauseMenu.SetActive(false);
        AchievementsMenu.SetActive(true);
        Achievements.RenderMenu(AchievementsMenu, PauseMenu);
    }

    public void CloseAchievementsMenu()
    {
        PauseMenu.SetActive(true);
        AchievementsMenu.SetActive(false);

        foreach (Transform child in AchievementsMenu.transform)
        { if (!(child.name == "NaA")) { GameObject.Destroy(child.gameObject); } }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
