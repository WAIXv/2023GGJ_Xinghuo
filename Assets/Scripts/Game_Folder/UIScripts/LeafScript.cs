using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils.EventCenter;

namespace Game_Folder.UIScripts
{
    public class LeafScript : MonoBehaviour
    {
        [SerializeField] private Animator effectAnim;
        public Text textComp;

        private GameMgr _gameMgr;

        private void Start()
        {
            EventCenter.GetInstance().AddEventListener(EventTypes.RootMove,onMove);
            EventCenter.GetInstance().AddEventListener<int>(EventTypes.OnJuice,OnJuice);
            _gameMgr = GameMgr.GetInstance();
        }

        void onMove()
        {
            if (_gameMgr == null)
                _gameMgr = GameMgr.GetInstance();
            textComp.text = _gameMgr.moveStep.ToString();
        }

        void OnJuice(int stepAward)
        {
            textComp.text = _gameMgr.moveStep.ToString();
            if (stepAward > 7)
            {
                effectAnim.Play("12",0,0f);
            }
            else
            {
                effectAnim.Play("6",0,0f);

            }
        }
    }
}