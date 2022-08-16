using UnityEngine;
using Finark.Events;

[CreateAssetMenu(menuName = "EventChannels/ItemDatabaseChannel")]
public class ItemDatabaseChannel : EventChannelBase
{

    public EventChannelItem FetchItemFromDatabaseWithID;

}