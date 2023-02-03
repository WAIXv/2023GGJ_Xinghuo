using UnityEngine;
using Utils.@abstract;
using Utils.EventCenter;

namespace Utils.InputMgr
{
    public class InputMgr : BaseManager<InputMgr>
    {
        private bool isStart = false;

        // 构造函数中 添加Updata监听
        public InputMgr()
        {
            MonoMgr.MonoMgr.GetInstance().AddUpdateListener(MyUpdate);
        }

        /// <summary>
        /// 是否开启按键检测
        /// </summary>
        /// <param name="isOpen"></param>
        public void StartOrEndCheck(bool isOpen)
        {
            isStart = isOpen;
        }

        // 用来检测按键抬起按下 并分发事件的方法
        private void CheckKeyCode(KeyCode key)
        {
            //事件中心模块 分发按下抬起事件
            if (Input.GetKeyDown(key))
                EventCenter.EventCenter.GetInstance().EventTrigger(EventTypes.Input_KeyDown, key);
            //事件中心模块 分发按下抬起事件
            if (Input.GetKeyUp(key))
                EventCenter.EventCenter.GetInstance().EventTrigger(EventTypes.Input_KeyUp, key);
        }

        private void MyUpdate()
        {
            //没有开启输入检测 就不去检测 直接return
            if (!isStart)
                return;
            CheckKeyCode(KeyCode.Space);
            CheckKeyCode(KeyCode.RightArrow);
            CheckKeyCode(KeyCode.LeftArrow);
            CheckKeyCode(KeyCode.UpArrow);
            CheckKeyCode(KeyCode.DownArrow);
            CheckKeyCode(KeyCode.LeftAlt);
            CheckKeyCode(KeyCode.T);//
        }
    }
}