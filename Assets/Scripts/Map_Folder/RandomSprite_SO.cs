using System.Collections.Generic;
using UnityEngine;

namespace Map_Folder
{
    [CreateAssetMenu(menuName = "RandomSpriteSO配置" ,fileName = "_随机Sprite配置")]
    public class RandomSprite_SO : ScriptableObject
    {
        [SerializeField] private List<Sprite> _sprites;

        public Sprite RandomSprite
        {
            get { return _sprites[Random.Range(0, _sprites.Count - 1)]; }
        }
    }
}