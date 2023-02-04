using UnityEngine;

namespace Map_Folder
{
    [CreateAssetMenu(menuName = "地图贴图配置SO",fileName = "Map_Config_SO")]
    public class MapConfig : ScriptableObject
    {
        public Sprite redCover;
        public Sprite greenCover;
        public Sprite cornerRootSprite;
        public Sprite rootSprite;
    }
}