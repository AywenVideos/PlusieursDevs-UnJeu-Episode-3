using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEngine.UIElements.VisualElement;

public class AchievementsManager : MonoBehaviour
{
    public Sprite Validated;
    public Sprite Locked;

    public InGameNotificationsManager Notifications;

    public GameObject achievementPrefab;

    //InGame Data
    List<List<GameObject>> AchievementsPages = new List<List<GameObject>>();
    int CurrentPage = 0;
    int TotalPages = 0;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initalizing Achievements");
        Achievement[] achievements = Resources.LoadAll<Achievement>("Achievements/");

        foreach (Achievement a in achievements){
            Debug.Log(a.Name + " 『" + a.isCompleted + "』\n" + a.Description);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RenderMenu(GameObject MenuUI, GameObject PauseMenu)
    {

        AchievementsPages.Clear();
        TotalPages = 0;
        CurrentPage = 0;

        bool IsBottom = false;
        bool IsRight = false;

        PauseMenu.SetActive(false);
        MenuUI.SetActive(true);

        Achievement[] achievements = Resources.LoadAll<Achievement>("Achievements/");

        foreach (Achievement a in achievements)
        {

            GameObject achievementObject = Instantiate(achievementPrefab, MenuUI.transform);
            
            RectTransform rt = achievementObject.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(IsRight ? 482f : -482f, IsBottom ? 160f-300f : 160f);

            if (IsRight) { IsBottom = true; }
            IsRight = !IsRight;

            TextMeshProUGUI titleComponent = achievementObject.GetComponentsInChildren<TextMeshProUGUI>().ToList().Find(x => x.name.Contains("Title"));
            TextMeshProUGUI descriptionComponent = achievementObject.GetComponentsInChildren<TextMeshProUGUI>().ToList().Find(x => x.name.Contains("Description"));
            Image iconComponent = achievementObject.GetComponentsInChildren<Image>().ToList().Find(x => x.name.Contains("Image"));
            Image statutComponent = achievementObject.GetComponentsInChildren<Image>().ToList().Find(x => x.name.Contains("Unlocked?"));

            titleComponent.text = a.isCompleted ? a.Name : "???";
            descriptionComponent.text = a.isCompleted ? a.Description : "Tu n'as pas tout découvert";
            iconComponent.sprite = a.Icon;
            statutComponent.sprite = a.isCompleted ? Validated : Locked;

            if (AchievementsPages.Count <= TotalPages)
            {
                AchievementsPages.Add(new List<GameObject>());
            }

            if (AchievementsPages[TotalPages].Count == 4)
            {
                TotalPages++;
                if (AchievementsPages.Count <= TotalPages)
                {
                    AchievementsPages.Add(new List<GameObject>());
                }
            }
            AchievementsPages[TotalPages].Add(achievementObject);
        }

        RenderPage();
    }

    public void GiveAchievement(Achievement achievement)
    {

        achievement.isCompleted = true;

    }

    public void ValidateAchievement(Achievement achievement)
    {

        bool OldStatus = achievement.isCompleted;

        achievement.isCompleted = true;

        bool NewStatus = achievement.isCompleted;

        if (!(OldStatus == NewStatus))
        {
            Notifications.SendNotif("Succès débloqué", achievement.Name, achievement.Icon);
        }
    }


    public void GiveAchievementByName(string id)
    {

        Achievement[] achievements = Resources.LoadAll<Achievement>("Achievements/");

        foreach (Achievement a in achievements)
        {
            if (a.name == id) { a.isCompleted = true; }
        }

    }

    public void ValidateAchievementByName(string id)
    {

        Achievement[] achievements = Resources.LoadAll<Achievement>("Achievements/");

        foreach (Achievement a in achievements)
        {
            if (a.name == id) { 

                bool OldStatus = a.isCompleted;

                a.isCompleted = true;

                bool NewStatus = a.isCompleted;

                if (!(OldStatus == NewStatus))
                {
                    Notifications.SendNotif("Succès débloqué", a.Name, a.Icon);
                }
            }
        }
    }

    public void ResetAchievements()
    {
        Achievement[] achievements = Resources.LoadAll<Achievement>("Achievements/");

        foreach (Achievement a in achievements)
        {

            a.isCompleted = false;

        }
    }

    public void RenderPage()
    {
        foreach (List<GameObject> Page in AchievementsPages)
        {

            if (Page != AchievementsPages[CurrentPage])
            {
                foreach (GameObject a in Page)
                {
                    a.SetActive(false);
                }
            } else
            {
                foreach (GameObject a in Page)
                {
                    a.SetActive(true);
                }
            }

        }
    }

    public void GoPageUp()
    {
        if (CurrentPage > 0) // Ensure we don't go out of bounds
        {
            CurrentPage -= 1;
            RenderPage();
        }
    }

    public void GoPageDown()
    {
        if (CurrentPage < TotalPages) // Ensure we don't exceed the number of pages
        {
            CurrentPage += 1;
            RenderPage();
        }
    }

}
