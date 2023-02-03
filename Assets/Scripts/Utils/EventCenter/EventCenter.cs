using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils.@abstract;

namespace Utils.EventCenter
{
    /// <summary>
    /// 事件中心模块
    /// </summary>
    /// <author>Wai.</author>
    public class EventCenter : BaseManager<EventCenter>
    {
        private Dictionary<EventTypes, IEventInfo> eventDic = new Dictionary<EventTypes, IEventInfo>();
        
	    /// <summary>
	    /// 添加有参事件监听
	    /// </summary>
	    /// <param name="name">监听事件名称</param>
	    /// <param name="action">准备用来处理事件的 有参委托函数</param>
	    /// <typeparam name="T">委托参数类型</typeparam>
        public void AddEventListener<T>(EventTypes name, UnityAction<T> action)
        {
            //有没有对应的事件监听
            //有的情况
            if( eventDic.ContainsKey(name) )
            {
                if(typeof(T) == eventDic[name].type)
                    (eventDic[name] as EventInfo<T>).actions += action;
                else
                    Debug.LogError("事件参数类型不匹配");
            }
            //没有的情况
            else
            {
                eventDic.Add(name, new EventInfo<T>( action ));
            }
        }
        
        /// <summary>
        /// 添加无参事件监听
        /// </summary>
        /// <param name="name">监听事件枚举</param>
        /// <param name="action">准备用来处理事件的 无参委托函数</param>
        public void AddEventListener(EventTypes name, UnityAction action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo).actions += action;
            }
            else
            {
                eventDic.Add(name, new EventInfo(action));
            }
        }
        
        /// <summary>
        /// 移除有参事件监听
        /// </summary>
        /// <param name="name">监听事件枚举</param>
        /// <param name="action">需要移除的委托函数</param>
        /// <typeparam name="T">委托的参数类型</typeparam>
        public void RemoveEventListener<T>(EventTypes name, UnityAction<T> action)
        {
            if( eventDic.ContainsKey(name) )
            {
                if(typeof(T) == eventDic[name].type)
                    (eventDic[name] as EventInfo<T>).actions -= action;
                else
                    Debug.LogError("事件参数类型不匹配");
            }
        }
        
        /// <summary>
        /// 移除无参事件监听
        /// </summary>
        /// <param name="name">监听事件枚举</param>
        /// <param name="action">需要移除的委托函数</param>
        public void RemoveEventListener(EventTypes name, UnityAction action)
        {
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo).actions -= action;
        }
        
        /// <summary>
        /// 分发有参监听事件
        /// </summary>
        /// <param name="name">事件枚举</param>
        /// <param name="info">传入触发事件的参数</param>
        /// <typeparam name="T">传入参数类型</typeparam>
        public void EventTrigger<T>(EventTypes name, T info)
        {
            //有没有对应的事件监听
            //有的情况
            if (eventDic.ContainsKey(name))
            {
                if (typeof(T) == eventDic[name].type)
                {
                    if ((eventDic[name] as EventInfo<T>).actions != null)
                        (eventDic[name] as EventInfo<T>).actions.Invoke(info);
                }
                else
                    Debug.LogError("参数类型不匹配");
            }
        }
        
        /// <summary>
        /// 分发无参监听事件
        /// </summary>
        /// <param name="name">事件枚举</param>
        public void EventTrigger(EventTypes name)
        {
            if (eventDic.ContainsKey(name))
            {
                if ((eventDic[name] as EventInfo).actions != null)
                    (eventDic[name] as EventInfo).actions.Invoke();
            }
        }
        
        /// <summary>
        /// 清空事件中心中所有监听事件，切换场景时调用
        /// </summary>
        public void Clear()
        {
            eventDic.Clear();
        }
    }

}