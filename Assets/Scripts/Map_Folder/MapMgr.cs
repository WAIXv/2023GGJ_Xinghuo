using Blocks_Folder;
using UnityEngine;
using Utils.@abstract;
using Utils.EventCenter;
using Utils.MonoMgr;

namespace Map_Folder
{
    public class MapMgr : BaseManager<MapMgr>
    {
        private BlockBase _blockCur;
        private BlockBase _blockPre;
        
        public MapMgr()
        {
            MonoMgr.GetInstance().AddUpdateListener(UpdateEvent);
            var eventCenter = EventCenter.GetInstance();
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseEnterUI,OnMouseEnter);
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseExitUI,OnMouseExit);
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseClickUI,OnMouseClick);
        }

        void UpdateEvent()
        {
            
        }

        void HandleRender()
        {
            
        }

        void HandleData()
        {
            
        }

        void OnMouseEnter(BlockBase cur)
        {
            _blockCur = cur;
            Debug.Log("Enter");
        }

        void OnMouseExit(BlockBase pre)
        {
            _blockPre = pre;
            Debug.Log("Exit");
        }

        void OnMouseClick(BlockBase cur)
        {
            Debug.Log("Clik");
        }
    }
}