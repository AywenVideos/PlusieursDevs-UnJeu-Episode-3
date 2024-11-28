using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Scriptable Object/Achievement")]

public class Achievement : ScriptableObject
{

    public string Name;
    public string Description;
    public bool isCompleted;
    public Sprite Icon;

}