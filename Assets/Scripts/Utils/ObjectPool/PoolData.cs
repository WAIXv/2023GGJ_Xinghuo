using System.Collections.Generic;
using UnityEngine;

namespace Utils.ObjectPool
{
    /// <summary>
    /// 对象池的抽屉类，每存放一个新
    /// </summary>
    public class PoolData
    {
        public GameObject fatherObj;            //对象挂载的父节点，抽屉名
        public List<GameObject> poolList;       //对象容器
        
        public PoolData(GameObject obj, GameObject poolObj)
        {
            //给我们的抽屉 创建一个父对象 并且把他作为我们pool(衣柜)对象的子物体
            fatherObj = new GameObject(obj.name);
            fatherObj.transform.parent = poolObj.transform;
            poolList = new List<GameObject>() {};
            PushObj(obj);
        }
        
        /// <summary>
        /// 目标GO放回抽屉
        /// </summary>
        /// <param name="obj"></param>
        public void PushObj(GameObject obj)
        {
            //失活 让其隐藏
            obj.SetActive(false);
            //存起来
            poolList.Add(obj);
            //设置父对象
            obj.transform.parent = fatherObj.transform;
        }
        
        /// <summary>
        /// 取出抽屉中的物体
        /// </summary>
        /// <returns></returns>
        public GameObject GetObj()
        {
            GameObject obj = null;
            //取出第一个
            obj = poolList[0];
            poolList.RemoveAt(0);
            //激活 让其显示
            obj.SetActive(true);
            //断开了父子关系
            obj.transform.parent = null;

            return obj;
        }
    }
}