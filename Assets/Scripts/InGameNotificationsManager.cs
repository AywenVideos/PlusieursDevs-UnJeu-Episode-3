using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameNotificationsManager : MonoBehaviour
{

    public GameObject NotifPrefab;
    public GameObject UICanvas;

    public List<GameObject> ActiveNotif = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    private readonly static float INITIAL_TIME_NEEDED = 3.0f;
    private float timer = 0;

    void Update()
    {
        if (!ActiveNotif.Any())
        {
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= INITIAL_TIME_NEEDED)
            {
                KillNotif();
                timer = 0;
            }
        }
    }

    public void SendNotif(string Title, string Content, Sprite Icon)
    {
        GameObject Notif = Instantiate(NotifPrefab, UICanvas.transform);

        TextMeshProUGUI titleComponent = Notif.GetComponentsInChildren<TextMeshProUGUI>().ToList().Find(x => x.name.Contains("Title"));
        TextMeshProUGUI contentComponent = Notif.GetComponentsInChildren<TextMeshProUGUI>().ToList().Find(x => x.name.Contains("Description"));
        Image iconComponent = Notif.GetComponentsInChildren<Image>().ToList().Find(x => x.name.Contains("Image"));

        titleComponent.text = Title;
        contentComponent.text = Content;
        iconComponent.sprite = Icon;

        ActiveNotif.Add(Notif);
    }

    public void KillNotif()
    {
        if (ActiveNotif.Count > 0)
        {
            GameObject.Destroy(ActiveNotif[0]);
            ActiveNotif.RemoveAt(0);
        }
    }
}
