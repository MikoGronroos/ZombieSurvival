using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player Stats", fileName = "Player Stats")]
public class PlayerStats : ScriptableObject
{

    [SerializeField] private HealthSystem healthSystem = new HealthSystem();

    [SerializeField] private PlayerVitalsSystem playerVitalsSystem = new PlayerVitalsSystem();

    [SerializeField] private PlayerWeightSystem playerWeightSystem = new PlayerWeightSystem();

    [SerializeField] private int armorRating;

    public HealthSystem HealthSystem { get { return healthSystem; } }

    public PlayerVitalsSystem PlayerVitalsSystem { get { return playerVitalsSystem; } }

    public PlayerWeightSystem PlayerWeightSystem { get { return playerWeightSystem; } }

    public int ArmorRating { get { return armorRating; } set { armorRating = value; } }

}
