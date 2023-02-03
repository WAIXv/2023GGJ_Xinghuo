using System;
using UnityEngine.Events;

namespace Utils.EventCenter
{
    public interface IEventInfo
    {
        public Type type { get; }
    }

    public class EventInfo<T> : IEventInfo
    {
        public Type type { get; }
        public UnityAction<T> actions;

        public EventInfo(UnityAction<T> action)
        {
            actions += action;
            type = typeof(T);
        }
        
    }

    public class EventInfo : IEventInfo
    {
        public Type type { get; }
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
            type = typeof(Type);
        }

    }
}