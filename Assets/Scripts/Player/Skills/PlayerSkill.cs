using UnityEngine;

[CreateAssetMenu(menuName = "PlayerSkill", fileName = "Player Skill")]
public class PlayerSkill : ScriptableObject
{

    [SerializeField] private int level = 1;
    [SerializeField] private int experienceProgress;

    public int Level { get { return level; } set { level = value; } }

    public int ExperienceProgress { get { return experienceProgress; } set { experienceProgress = value; } }

}

