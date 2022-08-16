using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player Stats", fileName = "Player Stats")]
public class PlayerStats : ScriptableObject
{

    [SerializeField] private HealthSystem healthSystem = new HealthSystem();

    [SerializeField] private PlayerVitalsSystem playerVitalsSystem = new PlayerVitalsSystem(); 

    [SerializeField] private int armorRating;

    [SerializeField] private float damage;

    public HealthSystem HealthSystem { get { return healthSystem; } }

    public PlayerVitalsSystem PlayerVitalsSystem { get { return playerVitalsSystem; } }

    public int ArmorRating { get { return armorRating; } set { armorRating = value; } }

    public float Damage { get { return damage; } set { damage = value; } }

}
