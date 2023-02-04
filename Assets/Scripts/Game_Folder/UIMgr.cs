using System.Collections;
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
            GameMgr.GetInstance().gameOn = true;
            GameMgr.GetInstance().endY = -485f + (mapCreator._mapMatrix.Length - 1) * 80f;
        }

        void OnQuitClick()
        {
            
        }



    }
}