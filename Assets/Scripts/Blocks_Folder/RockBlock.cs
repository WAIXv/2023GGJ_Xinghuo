using UnityEngine.EventSystems;
using Utils.EventCenter;

namespace Blocks_Folder
{
    public class RockBlock : BlockBase
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
            
        }

        private void OnMouseExit(BlockBase pre)
        {
            
        }

        private void OnMouseClick(BlockBase cur)
        {
            
        }
    }
}