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
            MapMgr.GetInstance();
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
                    obj.GetComponent<BlockBase>().info.locate = new Vector2(j, i);
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