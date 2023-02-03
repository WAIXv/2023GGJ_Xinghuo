using UnityEngine;

namespace Utils.SingleTon
{
    public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T GetInstance()
        {
            if( instance == null )
            {
                GameObject obj = new GameObject
                {
                    //设置对象的名字为脚本名
                    name = typeof(T).ToString()
                };
                //让这个单例模式对象 过场景不移除
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<T>();
            }
            return instance;
        }
    }
}