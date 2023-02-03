using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils.EventCenter;

namespace Blocks_Folder
{
    public class BlockBase : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        public BlockAttribute info;
        
        protected Queue<int> _stateQueue;

        protected EventCenter EventCenter;

        private void Start()
        {
            EventCenter = EventCenter.GetInstance();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            EventCenter.EventTrigger(EventTypes.MouseEnterUI,this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            EventCenter.EventTrigger(EventTypes.MouseExitUI,this);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            EventCenter.EventTrigger(EventTypes.MouseClickUI,this);
        }
    }
}
