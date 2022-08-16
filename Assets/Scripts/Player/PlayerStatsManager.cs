using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{

    [SerializeField] private PlayerStats currentPlayerStats;

    [SerializeField] private PlayerCombatChannel playerCombatChannel;
    [SerializeField] private InventoryChannel inventoryChannel;
    
    public PlayerStats CurrentPlayerStats { get { return currentPlayerStats; } }

    private void OnEnable()
    {
        playerCombatChannel.GetPlayerDamage += GetPlayerDamage;
        inventoryChannel.InventoryEat += OnInventoryEatListener;
    }

    private void OnDisable()
    {
        playerCombatChannel.GetPlayerDamage -= GetPlayerDamage;
        inventoryChannel.InventoryEat -= OnInventoryEatListener;
    }

    private void Start()
    {
        currentPlayerStats.HealthSystem.SetupHealthSystem(OnHealthZero);
    }

    private float GetPlayerDamage(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        return currentPlayerStats.Damage;
    }

    private void OnInventoryEatListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        var consumable = inventoryChannel?.FetchInventoryItemWithIndex(args).Item as ItemConsumable;

        foreach (var item in consumable.ConsumableValues)
        {
            switch (item.ConsumableType)
            {
                case ConsumableType.Hunger:
                    currentPlayerStats.PlayerVitalsSystem.AddFullness(item.Value);
                    break;
                case ConsumableType.Thrist:
                    currentPlayerStats.PlayerVitalsSystem.AddHydration(item.Value);
                    break;
            }
        }

    }

    private void OnHealthZero()
    {

    }

}