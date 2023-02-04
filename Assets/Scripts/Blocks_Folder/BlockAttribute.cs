using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Blocks_Folder
{
    [Serializable]
    public class BlockAttribute
    {
        [FormerlySerializedAs("curState")] 
        [Tooltip("图块可选择状态（参照状态对照表）")] public int selectState;
        [Tooltip("图块类型")] public int type;
        [Tooltip("图块贴图")] public Image image;
        [Tooltip("玩家踩上时的步数奖励")] public int stepReward;
        [Tooltip("图块所在方格位置")] public Vector2 locate;

    }
}