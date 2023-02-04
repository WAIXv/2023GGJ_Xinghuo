using System.Collections.Generic;
using Blocks_Folder;
using UnityEngine;

namespace Map_Folder
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField] private TextAsset _textAsset;
        [SerializeField] private RectTransform _basePoint;
        
        [SerializeField] private float _blockLength;
        [SerializeField] private float _blockHeight;

        [Header("图块预制体设定")]
        [SerializeField] private GameObject _rockPrefab;
        [SerializeField] private GameObject _emptyPrefab;
        [SerializeField] private GameObject _juciePrefab;

        public GameObject[][] _mapMatrix;

        private void Start()
        {
            LoadMapMatrix();
            CreateMap();
            MapMgr.GetInstance();
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
            
        }
    }
}