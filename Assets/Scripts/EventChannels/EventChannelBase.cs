using System;
using System.Collections.Generic;
using UnityEngine;

namespace Finark.Events
{
    public abstract class EventChannelBase : ScriptableObject
    {

        public delegate void EventChannel(Dictionary<string, object> args, Action<Dictionary<string, object>> callback = null);

        public delegate bool EventChannelBool(Dictionary<string, object> args, Action<Dictionary<string, object>> callback = null);

        public delegate int EventChannelInt(Dictionary<string, object> args, Action<Dictionary<string, object>> callback = null);

        public delegate float EventChannelFloat(Dictionary<string, object> args, Action<Dictionary<string, object>> callback = null);

        public delegate string EventChannelString(Dictionary<string, object> args, Action<Dictionary<string, object>> callback = null);

        public delegate DatabaseItem EventChannelDatabaseItem(Dictionary<string, object> args, Action<Dictionary<string, object>> callback = null);

        public delegate Item EventChannelItem(Dictionary<string, object> args, Action<Dictionary<string, object>> callback = null);

    }
}
