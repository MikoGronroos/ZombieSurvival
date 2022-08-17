using Finark.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/PlayerStatsChannel")]
public class PlayerStatsChannel : EventChannelBase
{

    public EventChannelFloat GetPlayerDamage;

    public EventChannel ChangePlayerWeight;

}