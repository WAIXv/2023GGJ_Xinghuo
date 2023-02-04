using System.Collections.Generic;
using Blocks_Folder;
using UnityEngine;

namespace Map_Folder
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField] private LevelConfig _config;

        private static MapCreator instance;
        private TextAsset _textAsset;
        private RectTransform _basePoint;
        
        private float _blockLength;
        private float _blockHeight;

        [Header("图块预制体设定")]
        [SerializeField] private GameObject _rockPrefab;
        [SerializeField] private GameObject _emptyPrefab;
        [SerializeField] private GameObject _juciePrefab;
        [SerializeField] private GameObject _bigJuciePrefab;
        [SerializeField] private GameObject _hardSoulPrefab;

        public GameObject[][] _mapMatrix;

        private void Start()
        {
            instance = this;
            _textAsset = _config.levelConfig;
            _basePoint = GameObject.Find("MapBasePoint").GetComponent<RectTransform>();
            _blockHeight = _config.blockHeight;
            _blockLength = _config.blockLength;
            LoadMapMatrix();
            CreateMap();
            GameMgr.GetInstance().SetFinalandStartPoint();
        }

        public static MapCreator GetInstance()
        {
            return instance;
        }

        private void CreateMap()
        {
            Vector2 placer = _basePoint.position;
            var iOffset = 0f;
            var jOffset = 0f;
            for (var j = 0; j < _mapMatrix.Length; j++)
            {
                for (var i = 0; i < _mapMatrix[j].Length; i++)
                {
                    var obj = GameObject.Instantiate(_mapMatrix[j][i], placer + Vector2.right * iOffset + Vector2.down * jOffset, Quaternion.identity);
                    _mapMatrix[j][i] = obj;
                    var block = obj.GetComponent<BlockBase>();
                    block.info.locate = new Vector2(j, i);
                    block.info.image.sprite = block.info.SpritesSO.RandomSprite;

                    if (j == 0)
                    {
                        var sprite = Resources.Load<RandomSprite_SO>("Frist_Empty_随机Sprite配置");
                        block.info.image.sprite = sprite.RandomSprite;
                    }
                    
                    obj.transform.SetParent(_basePoint);
                    iOffset += _blockLength * 2;
                    if (i == _mapMatrix[j].Length - 1)
                    {
                        iOffset = 0f;
                        jOffset += 2 * _blockHeight;
                    }
                }
            }
        }

        private void LoadMapMatrix()
        {
            string[] lines = _textAsset.text.Split('\n');
            _mapMatrix = new GameObject[lines.Length - 1][];
            
            for (int j = 0; j < lines.Length - 1; j++)
            {
                _mapMatrix[j] = new GameObject[lines[j].Length - 1];
                for (int i = 0; i < lines[j].Length - 1; i++)
                {
                    switch (lines[j][i])
                    {
                        case '0':
                            _mapMatrix[j][i] = _emptyPrefab;
                            break;
                        case '1':
                            _mapMatrix[j][i] = _rockPrefab;
                            break;
                        case '2':
                            _mapMatrix[j][i] = _juciePrefab;
                            break;
                        case '3':
                            _mapMatrix[j][i] = _bigJuciePrefab;
                            break;
                        case '4':
                            _mapMatrix[j][i] = _hardSoulPrefab;
                            break;
                    }
                }
            }
            
        }

        public void ReloadMap()
        {
            foreach (var objs in _mapMatrix)
            {
                foreach (var obj in objs)
                {
                    Destroy(obj);
                }
            }
            LoadMapMatrix();
            CreateMap();
        }
    }
}