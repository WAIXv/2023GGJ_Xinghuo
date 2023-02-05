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

        private EventCenter EventCenter;

        protected virtual void Start()
        {
            EventCenter = EventCenter.GetInstance();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EventCenter.EventTrigger(EventTypes.MouseEnterUI,this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EventCenter.EventTrigger(EventTypes.MouseExitUI,this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            EventCenter.EventTrigger(EventTypes.MouseClickUI,this);
        }
    }
}
