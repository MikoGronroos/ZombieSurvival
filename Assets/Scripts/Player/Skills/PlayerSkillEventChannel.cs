using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/Player Skill")]
public class PlayerSkillEventChannel : ScriptableObject
{
    public delegate void ProgressSkill(PlayerSkill skill);

    public ProgressSkill ProgressSkillEvent { get; set; }

}