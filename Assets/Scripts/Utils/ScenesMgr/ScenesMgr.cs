using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utils.@abstract;
using Utils.EventCenter;

namespace Utils.ScenesMgr
{
    /// <summary>
    /// 场景管理模块
    /// </summary>
    /// <author>Wai.</author>
    public class ScenesMgr : BaseManager<ScenesMgr>
    {
    
        /// <summary>
        /// 同步切换场景
        /// </summary>
        /// <param name="name">目标场景名，需要在BulidSetting中添加</param>
        /// <param name="fun">加载完成后的回调方法</param>
        public void LoadScene(string name, UnityAction fun)
        {
            //场景同步加载
            SceneManager.LoadScene(name);
            //加载完成过后 才会去执行fun
            fun();
        }
        
        /// <summary>
        /// 异步切换场景
        /// </summary>
        /// <param name="name">目标场景</param>
        /// <param name="fun">加载完成后的回调方法</param>
        public void LoadSceneAsyn(string name, UnityAction fun)
        {
            MonoMgr.MonoMgr.GetInstance().StartCoroutine(ReallyLoadSceneAsyn(name, fun));
        }
    
        // 协程异步加载场景
        private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction fun)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name);
            //可以得到场景加载的一个进度
            while(!ao.isDone)
            {
                //事件中心 向外分发 进度情况  外面想用就用
                EventCenter.EventCenter.GetInstance().EventTrigger(EventTypes.UI_UpdateProgressBar, ao.progress);
                //这里面去更新进度条
                yield return ao.progress;
            }
            //加载完成过后 才会去执行fun
            fun();
        }
    }
}