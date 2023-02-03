using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils.@abstract;
using Object = UnityEngine.Object;

namespace Utils.UIFrameWork {
    public class UIMgr : BaseManager<UIMgr> {
        private ResolutionType _resolutionType; // 分辨率类型
        private Transform _rootCanvas; // UI的根物体
        private Transform _operationLayer; // 操作层（第二层）
        private Transform _systemLayer; // 系统层（第一层）
        private Transform _informationLayer; // 信息层（第三层）
        private Transform _backLayer; // 背景层（第四层）
        private readonly Dictionary<string, EventDrivenCanvasBase> _loadedCanvasPrefabDic = new Dictionary<string, EventDrivenCanvasBase>();

        public UIMgr() {
            MatchScreenResolution();
            InitialEnvironment();
        }

        #region 初始化相关

        /// <summary>
        /// 匹配屏幕分辨率，使用对应的UI面板
        /// </summary>
        private void MatchScreenResolution() {
#if UNITY_EDITOR
            // 编辑状态下强制使用1920*1080测试
            _resolutionType = ResolutionType.Height1080;
#else
            if (Mathf.Abs(Screen.height - 1080) < 10) _resolutionType = ResolutionType.Height1080;
            else _resolutionType = ResolutionType.Height1080;
#endif
        }

        /// <summary>
        /// 初始化UI环境
        /// </summary>
        private void InitialEnvironment() {
            var resLoader = ResMgr.ResMgr.GetInstance();
            _rootCanvas = resLoader.Load<GameObject>($"UI/{_resolutionType}/RootCanvas").transform;
            Object.DontDestroyOnLoad(_rootCanvas);
            Object.DontDestroyOnLoad(resLoader.Load<GameObject>("UI/EventSystem"));

            _systemLayer = _rootCanvas.Find("System");
            _operationLayer = _rootCanvas.Find("Operation");
            _informationLayer = _rootCanvas.Find("Information");
            _backLayer = _rootCanvas.Find("Back");
        }

        #endregion

        #region 根Canvas加载卸载相关

        /// <summary>
        /// 加载画布
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="onCanvasLoadedCallback"></param>
        /// <typeparam name="TCanvasCtrl"></typeparam>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void LoadMainCanvas<TCanvasCtrl>(DisplayLayer layer, UnityAction onCanvasLoadedCallback = null) where TCanvasCtrl : EventDrivenCanvasBase {
            var target = ResMgr.ResMgr.GetInstance().Load<GameObject>($"UI/{_resolutionType}/{typeof(TCanvasCtrl).Name}").transform as RectTransform;
            if (target == null) throw new NullReferenceException();
            target.SetParent(layer switch {
                DisplayLayer.System => _systemLayer,
                DisplayLayer.Operation => _operationLayer,
                DisplayLayer.Information => _informationLayer,
                DisplayLayer.Back => _backLayer,
                _ => throw new ArgumentOutOfRangeException(nameof(layer), layer, null)
            });
            target.pivot = 0.5f * Vector2.one;
            target.offsetMax = Vector2.zero;
            target.offsetMin = Vector2.zero;
            target.anchorMax = Vector2.one;
            target.anchorMin = Vector2.zero;
            target.localScale = Vector3.one;
            target.anchoredPosition = Vector2.zero;
            _loadedCanvasPrefabDic.Add(typeof(TCanvasCtrl).Name, target.GetComponent<TCanvasCtrl>());

            onCanvasLoadedCallback?.Invoke();
        }

        /// <summary>
        /// 移除画布
        /// </summary>
        /// <param name="onCanvasDestroyedCallback"></param>
        /// <typeparam name="TCanvasCtrl"></typeparam>
        public void DestroyMainCanvas<TCanvasCtrl>(UnityAction onCanvasDestroyedCallback = null) {
            var canvasName = typeof(TCanvasCtrl).Name;
            if (_loadedCanvasPrefabDic.ContainsKey(canvasName)) {
                EventDrivenCanvasBase canvas = _loadedCanvasPrefabDic[canvasName];
                canvas.DisableCanvas();
                Object.Destroy(canvas.gameObject);
                _loadedCanvasPrefabDic.Remove(canvasName);
            } else {
                Debug.Log("正在尝试卸载一个不存在的画布");
            }
            onCanvasDestroyedCallback?.Invoke();
        }

        /// <summary>
        /// 移除所有画布
        /// </summary>
        public void DestroyAllMainCanvas(UnityAction onAllCanvasDestroyedCallback = null) {
            foreach (var canvas in _loadedCanvasPrefabDic.Values) {
                canvas.DisableCanvas();
                Object.Destroy(canvas.gameObject);
            }
            _loadedCanvasPrefabDic.Clear();
            onAllCanvasDestroyedCallback?.Invoke();
        }
        
        #endregion
    }
}