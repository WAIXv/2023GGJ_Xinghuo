using UnityEngine;

namespace Utils.@abstract
{
    /// <summary>
    /// 单例管理器泛型基类
    /// </summary>
    /// <typeparam name="T">管理器类型</typeparam>
    /// <author>Wai.</author>
    public class BaseManager<T> where T : new()
    {
        private static T instance;

        public static T GetInstance()
        {
            if (instance == null)
            {
                instance = new T();
                Debug.Log("Create " + typeof(T));
            }

            return instance;
        }
    }
}