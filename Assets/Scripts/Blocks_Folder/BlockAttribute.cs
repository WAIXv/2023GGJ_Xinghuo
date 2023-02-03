using UnityEngine;

namespace Blocks_Folder
{
    public class BlockAttribute
    {
        [Tooltip("图块当前状态（参照状态对照表）")] public int curState;
        [Tooltip("图块贴图")] public Sprite image;
        [Tooltip("玩家踩上时的步数奖励")] public int stepReward;
        
    }
}