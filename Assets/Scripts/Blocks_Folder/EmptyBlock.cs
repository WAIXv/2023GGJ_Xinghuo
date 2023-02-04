using Map_Folder;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils.EventCenter;

namespace Blocks_Folder
{
    public class EmptyBlock : BlockBase
    {
        protected override void Start()
        {
            base.Start();
            var eventCenter = EventCenter.GetInstance();
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseEnterUI,OnMouseEnter);
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseExitUI,OnMouseExit);
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseClickUI,OnMouseClick);
        }

        private void OnMouseEnter(BlockBase cur)
        {
            if(cur != this) return;
            
        }

        private void OnMouseExit(BlockBase pre)
        {
            
        }

        private void OnMouseClick(BlockBase cur)
        {
            
        }
    }
}