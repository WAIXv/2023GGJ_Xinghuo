using UnityEngine;
using UnityEngine.Events;

namespace Utils.MonoMgr
{
    /// <summary>
    /// 公共Mono模块的控制器，运行时GO上实际挂载的脚本,提供外部添加移除帧更新事件的方法
    /// </summary>
    /// <author>Wai.</author>
    public class MonoController : MonoBehaviour
    {
        private event UnityAction updateEvent;
        private event UnityAction fixedUpdateEvent;

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            updateEvent?.Invoke();
        }

        private void FixedUpdate()
        {
            fixedUpdateEvent ?.Invoke();
        }

        /// <summary>
        /// 外部添加帧更新事件的方法
        /// </summary>
        /// <param name="fun">新增的帧事件</param>
        public void AddUpdateListener(UnityAction fun)
        {
            updateEvent += fun;
        }
        
        
        /// <summary>
        /// 外部移除帧更新事件的方法
        /// </summary>
        /// <param name="fun">移除的帧事件</param>
        public void RemoveUpdateListener(UnityAction fun)
        {
            updateEvent -= fun;
        }

        
        /// <summary>
        /// 外部添加物理帧更新事件的方法
        /// </summary>
        /// <param name="fun">新增的帧事件</param>
        public void AddFixedUpdateListener(UnityAction fun)
        {
            fixedUpdateEvent += fun;
        }

        
        /// <summary>
        /// 外部移除物理帧更新事件的方法
        /// </summary>
        /// <param name="fun">移除的帧事件</param>
        public void RemoveFixedUpdateListener(UnityAction fun)
        {
            fixedUpdateEvent -= fun;
        }
    }
}