using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utils.@abstract;
using Utils.MonoMgr;

namespace Map_Folder
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField] private RectTransform _basePoint;
        
        [SerializeField] private float _blockLength;
        [SerializeField] private float _blockHeight;
        [SerializeField] private int _count;

        public GameObject testObj;

        public List<List<GameObject>> _mapMatrix;

        private void Start()
        {
            CreateMap();
        }

        private void CreateMap()
        {
            Vector2 placer = _basePoint.position;
            var iOffset = 0f;
            var jOffset = 0f;
            for (var j = 0; j < _count; j++)
            {
                for (var i = 0; i < _count; i++)
                {
                    var obj = GameObject.Instantiate(testObj, placer + Vector2.right * iOffset + Vector2.down * jOffset, Quaternion.identity);
                    obj.transform.SetParent(_basePoint);
                    iOffset += _blockLength * 2;
                    if (i == _count - 1)
                    {
                        iOffset = 0f;
                        jOffset += 2 * _blockHeight;
                    }
                }
            }
        }
    }
}