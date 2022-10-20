using UnityEngine;
using Finark.Events;

[CreateAssetMenu(menuName = "EventChannels/ItemDatabaseChannel")]
public class ItemDatabaseChannel : EventChannelBase
{

    public delegate Item DatabaseDelegate(int id);

    public DatabaseDelegate FetchItemFromDatabaseWithID;

}