using Finark.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/PlayerCombatChannel")]
public class PlayerCombatChannel : EventChannelBase
{

    public EventChannelFloat GetPlayerDamage;

}