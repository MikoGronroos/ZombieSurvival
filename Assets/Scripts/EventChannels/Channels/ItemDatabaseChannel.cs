using UnityEngine;
using Finark.Events;

[CreateAssetMenu(menuName = "EventChannels/ItemDatabaseChannel")]
public class ItemDatabaseChannel : EventChannelBase
{

    public delegate Item DatabaseDelegate(string id);

    public DatabaseDelegate FetchItemFromDatabaseWithID;

}