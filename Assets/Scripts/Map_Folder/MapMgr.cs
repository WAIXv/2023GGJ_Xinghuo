using Blocks_Folder;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Utils.@abstract;
using Utils.EventCenter;
using Utils.MonoMgr;

namespace Map_Folder
{
    public class MapMgr : MonoBehaviour
    {
        [SerializeField] private GameObject redCoverPrefab;
        [SerializeField] private GameObject blueCoverPrefab;
        [SerializeField] private GameObject rootPrefab;
        
        private BlockBase _blockCur;
        private BlockBase _blockPre;

        private void Start()
        {
            redCoverPrefab = Instantiate(redCoverPrefab, GameObject.Find("Canvas").transform, true);
            redCoverPrefab.SetActive(false);
            
            blueCoverPrefab = Instantiate(blueCoverPrefab, GameObject.Find("Canvas").transform, true);
            blueCoverPrefab.SetActive(false);
            
            var eventCenter = EventCenter.GetInstance();
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseEnterUI, OnMouseEnter);
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseExitUI, OnMouseExit);
            eventCenter.AddEventListener<BlockBase>(EventTypes.MouseClickUI, OnMouseClick);
        }

        void OnMouseEnter(BlockBase cur)
        {
            _blockCur = cur;
            Debug.Log("Enter");
            switch (cur.info.moveState)
            {
                case 0:
                    //玩家可走
                    blueCoverPrefab.transform.position = cur.transform.position;
                    blueCoverPrefab.SetActive(true);
                    blueCoverPrefab.transform.SetParent(cur.transform);
                    break;
                case 1:
                    //玩家不可走
                    redCoverPrefab.transform.position = cur.transform.position;
                    redCoverPrefab.SetActive(true);
                    redCoverPrefab.transform.SetParent(cur.transform);
                    break;
                case 2:
                    //玩家走过
                    break;
            }
        }

        void OnMouseExit(BlockBase pre)
        {
            redCoverPrefab.SetActive(false);
            blueCoverPrefab.SetActive(false);
            _blockPre = pre;
            Debug.Log("Exit");
        }

        void OnMouseClick(BlockBase cur)
        {
            Debug.Log("Clik");
            switch (cur.info.moveState)
            {
                case 0:
                    SetAround(cur);
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            
        }

        void SetAround(BlockBase block)
        {
            EventCenter.GetInstance().EventTrigger(EventTypes.RootMove);
            var obj = Instantiate(rootPrefab, GameObject.Find("Canvas").transform, true);
            obj.transform.SetParent(block.transform);
            obj.transform.position = block.transform.position;
            obj.GetComponent<Image>().sprite = Resources.Load<RandomSprite_SO>("Root_随机Sprite配置").RandomSprite;

            if (block.info.locate.x == 0)
            {
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("root(new)_0");
            }
            
            block.info.moveState = 2;
            blueCoverPrefab.SetActive(false);
            var locate = block.info.locate;
            var matrix = MapCreator.GetInstance()._mapMatrix;
            
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

            if ((int)locate.y != matrix[0].Length - 1)
            {
                rightobj = matrix[(int)right.x][(int)right.y];
            }

            if (locate.x != 0)
            {
                upobj = matrix[(int)up.x][(int)up.y];
            }

            if ((int)locate.x != matrix.Length - 1)
            {
                downobj = matrix[(int)down.x][(int)down.y];
            }

            switch (block.info.type)
            {
                case 2:
                    GameMgr.GetInstance().moveStep += block.info.stepAward;
                    block.info.image.sprite = Resources.Load<Sprite>("tlieset_block(new)_5");
                    break;
            }
            
            if(leftobj) HandleMoveState(leftobj.GetComponent<BlockBase>());
            if(rightobj) HandleMoveState(rightobj.GetComponent<BlockBase>());
            if(upobj) HandleMoveState(upobj.GetComponent<BlockBase>());
            if(downobj) HandleMoveState(downobj.GetComponent<BlockBase>());
        }

        void HandleMoveState(BlockBase block)
        {
            switch (block.info.moveState)
            {
                case 0:
                    block.info.moveState = 0;
                    break;
                case 1:
                    block.info.moveState = 0;
                    break;    
                case 2:
                    break;

            }

            switch (block.info.type)
            {
                case 1:
                    block.info.moveState = 1;
                    break;
            }
        }
    }
}