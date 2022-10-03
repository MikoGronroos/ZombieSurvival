using UnityEngine;

[CreateAssetMenu(menuName = "PlayerSkill", fileName = "Player Skill")]
public class PlayerSkill : ScriptableObject
{
    [SerializeField] private int value = 1;

    public int Value { get { return value; } private set { } }
}

