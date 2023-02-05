using System.Collections;
using Game_Folder.UIScripts;
using Map_Folder;
using UnityEngine;
using Utils.@abstract;
using Utils.EventCenter;

namespace Game_Folder
{
    public class UIMgr : BaseManager<UIMgr>
    {

        public void OnStartClick()
        {
            var mapCreator = MapCreator.GetInstance();
            mapCreator.LoadMapMatrix(Resources.Load<TextAsset>("LevelMaptxt/level_0"));
            mapCreator.CreateMap();
            var gameMgr = GameMgr.GetInstance();
            gameMgr.gameOn = true;
            gameMgr.endY = -485f + (mapCreator._mapMatrix.Length - 1) * 80f;
            gameMgr.StartCoroutine(gameMgr.Level0Enter());
            var leaf = gameMgr.leafObj;
            leaf.SetActive(true);
            leaf.GetComponent<LeafScript>().textComp.text = gameMgr.moveStep.ToString();
        }

        void OnQuitClick()
        {
            
        }



    }
}