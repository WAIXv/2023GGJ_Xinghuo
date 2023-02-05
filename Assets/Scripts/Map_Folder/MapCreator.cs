using System.Collections.Generic;
using Blocks_Folder;
using Game_Folder;
using UnityEngine;

namespace Map_Folder
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField] private LevelConfig _config;

        private static MapCreator _instance;
        private RectTransform _basePoint;
        
        private float _blockLength;
        private float _blockHeight;

        [Header("图块预制体设定")]
        [SerializeField] private GameObject _rockPrefab;
        [SerializeField] private GameObject _emptyPrefab;
        [SerializeField] private GameObject _juciePrefab;
        [SerializeField] private GameObject _bigJuciePrefab;
        [SerializeField] private GameObject _hardSoulPrefab;
        [SerializeField] private GameObject _finalPrefab;

        public GameObject[][] _mapMatrix;

        private void Start()
        {
            _instance = this;
            _basePoint = GameObject.Find("MapBasePoint").GetComponent<RectTransform>();
            _blockHeight = _config.blockHeight;
            _blockLength = _config.blockLength;
        }

        public static MapCreator GetInstance()
        {
            return _instance;
        }

        public void CreateMap()
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
            GameMgr.GetInstance().SetFinalandStartPoint();
        }

        public void LoadMapMatrix(TextAsset txtFile)
        {
            string[] lines = txtFile.text.Split('\n');
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
                        case '*':
                            _mapMatrix[j][i] = _finalPrefab;
                            break;
                    }
                }
            }
            
        }

        public void ReloadMap(int levelIndex)
        {
            // foreach (var objs in _mapMatrix)
            // {
            //     foreach (var obj in objs)
            //     {
            //         Destroy(obj);
            //     }
            // }
            var txt = new TextAsset();
            switch (levelIndex)
            {
                case 0:
                    txt = Resources.Load<TextAsset>("LevelMaptxt/level_0");
                    break;
                case 1:
                    txt = Resources.Load<TextAsset>("LevelMaptxt/level_1");
                    break;
                case 2:
                    txt = Resources.Load<TextAsset>("LevelMaptxt/level_2");
                    break;
            }
            
            LoadMapMatrix(txt);
            CreateMap();
        }
    }
}