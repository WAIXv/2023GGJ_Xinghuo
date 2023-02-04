using UnityEngine;
using UnityEngine.Serialization;

namespace Map_Folder
{
    [CreateAssetMenu(menuName = "关卡配置SO", fileName = "Level_Config")]
    public class LevelConfig : ScriptableObject
    {
        [FormerlySerializedAs("mapConfig")] [Header("地图设置")]
        public TextAsset levelConfig;
        public int originStep;
        public Vector2 startPoint;
        public Vector2 finalPoint;

        [Header("图块设置")] 
        public float blockLength;
        public float blockHeight;
        
    }
}