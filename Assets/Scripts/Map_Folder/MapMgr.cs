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
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseEnterUI, OnMouseEnter);
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseExitUI, OnMouseExit);
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseClickUI, OnMouseClick);
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
            switch (cur.info.selectState)
            {
                case 0:
                    //玩家可走
                    cur.info.image.color = new Color(255, 0, 0, 101);
                    break;
                case 1:
                    //玩家不可走
                    cur.info.image.color = new Color(221, 28, 28, 101);
                    break;
                case 2:
                    //玩家走过
                    break;
            }
        }

        void OnMouseExit(BlockBase pre)
        {
            _blockPre = pre;
            Debug.Log("Exit");
            pre.info.image.color = new Color(255, 255, 255, 255);
        }

        void OnMouseClick(BlockBase cur)
        {
            Debug.Log("Clik");
            switch (cur.info.type)
            {
                case 0:
                    ClickEmpty(cur);
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }

        void ClickEmpty(BlockBase block)
        {
            var locate = block.info.locate;
            GameObject[][] matrix = MapCreator.GetInstance()._mapMatrix;
            
            var left = locate + Vector2.down;
            var right = locate + Vector2.up;
            var down = locate + Vector2.right;
            var up = locate + Vector2.left;

            GameObject leftobj = null;
            GameObject rightobj= null;
            GameObject downobj= null;
            GameObject upobj= null;

            if (locate.y != 0)
            {
                leftobj = matrix[(int)left.x][(int)left.y];
            }

            if ((int)locate.y != matrix.Length)
            {
                rightobj = matrix[(int)right.x][(int)right.y];
            }

            if (locate.x != 0)
            {
                upobj = matrix[(int)up.x][(int)up.y];
            }

            if ((int)locate.x != matrix[0].Length)
            {
                downobj = matrix[(int)down.x][(int)down.y];
            }
            
        }
    }
}