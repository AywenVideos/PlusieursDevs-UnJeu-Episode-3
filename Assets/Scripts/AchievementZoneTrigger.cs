using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementZoneTrigger : MonoBehaviour
{

    public AchievementsManager Achievements;

    public Achievement Achievement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("New achievement : " + Achievement.Name);
            Achievements.ValidateAchievement(Achievement);
        }
    }
}
