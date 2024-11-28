using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Script : MonoBehaviour
{

    public AchievementsManager Achievements;

    public Achievement CrashCarAchievement;

    // Start is called before the first frame update
    void Start()
    {
        Achievements.ValidateAchievement(CrashCarAchievement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
