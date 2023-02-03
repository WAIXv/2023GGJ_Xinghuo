using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using Utils.@abstract;

namespace Utils.MonoMgr
{
    public class MonoMgr : BaseManager<MonoMgr>
    {
        private MonoController controller;

        public MonoMgr()
        {
            GameObject obj = new GameObject("MonoController");
            controller = obj.AddComponent<MonoController>();
        }
        
        
        #region 添加Update事件
        /// <summary>
        /// 外部添加帧更新事件的方法
        /// </summary>
        /// <param name="fun">目标方法</param>
        public void AddUpdateListener(UnityAction fun)
        {
            controller.AddUpdateListener(fun);
        }
        
        
        /// <summary>
        /// 外部移除帧更新事件的方法
        /// </summary>
        /// <param name="fun">目标委托</param>
        public void RemoveUpdateListener(UnityAction fun)
        {
            controller.RemoveUpdateListener(fun);
        }
        #endregion

        
        #region 添加FixedUpdate事件

        /// <summary>
        /// 外部添加物理帧更新事件的方法
        /// </summary>
        /// <param name="fun">新增的帧事件</param>
        public void AddFixedUpdateListener(UnityAction fun)
        {
            controller.AddFixedUpdateListener(fun);
        }

        
        /// <summary>
        /// 外部移除物理帧更新事件的方法
        /// </summary>
        /// <param name="fun">移除的帧事件</param>
        public void RemoveFixedUpdateListener(UnityAction fun)
        {
            controller.RemoveFixedUpdateListener(fun);
        }

        #endregion

        
        #region 外部开启协程

        /// <summary>
        /// 外部开启协程的方法
        /// </summary>
        /// <param name="routine">目标委托</param>
        /// <returns></returns>
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return controller.StartCoroutine(routine);
        }

        
        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return controller.StartCoroutine(methodName, value);
        }

        
        public Coroutine StartCoroutine(string methodName)
        {
            return controller.StartCoroutine(methodName);
        }

        #endregion
        
    }
}