using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Utils.@abstract;

namespace Utils.ResMgr
{
    /// <summary>
    /// Resource资源加载模块
    /// </summary>
    /// <author>Wai.</author>
    public class ResMgr : BaseManager<ResMgr>
    {
        
        /// <summary>
        /// 同步加载目标资源
        /// </summary>
        /// <param name="path">目标在Resource中的路径</param>
        /// <typeparam name="T">目标的类型</typeparam>
        /// <returns></returns>
        public T Load<T>(string path) where T : Object
        {
            T res = Resources.Load<T>(path);
            return res is GameObject ? GameObject.Instantiate(res) : res;
        }

        /// <summary>
        /// 异步加载目标资源
        /// </summary>
        /// <param name="name">目标在Resource中的路径</param>
        /// <param name="callback">加载完成后回调方法</param>
        /// <typeparam name="T">目标的类型</typeparam>
        public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
        {
            MonoMgr.MonoMgr.GetInstance().StartCoroutine(ReallyLoadAsync(name, callback));
        }


        private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
        {
            ResourceRequest r = Resources.LoadAsync<T>(name);
            yield return r;

            if (r.asset is GameObject)
                callback(GameObject.Instantiate(r.asset) as T);
            else
                callback(r.asset as T);
        }
    }
}