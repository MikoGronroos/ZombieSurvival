using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{

    [SerializeField] private int baseProgressAdvance = 1;

    [SerializeField] private int maxLevel;

    [SerializeField] private int[] neededXpForEachLevel;

    [SerializeField] private PlayerSkillEventChannel playerSkillEventChannel;

    private void OnEnable()
    {
        playerSkillEventChannel.ProgressSkillEvent += AdvanceExperienceProgress;
    }

    private void OnDisable()
    {
        playerSkillEventChannel.ProgressSkillEvent -= AdvanceExperienceProgress;
    }

    private void AdvanceExperienceProgress(PlayerSkill skill)
    {

        if (skill.Level >= maxLevel) return;

        skill.ExperienceProgress += baseProgressAdvance;

        if (neededXpForEachLevel.Length < skill.Level) return;

        if (skill.ExperienceProgress >= neededXpForEachLevel[skill.Level - 1])
        {
            int pointsLeft = skill.ExperienceProgress - neededXpForEachLevel[skill.Level - 1];
            skill.Level += 1;
            skill.ExperienceProgress = pointsLeft;
        }
    }
}
