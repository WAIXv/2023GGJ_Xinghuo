using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utils.UIFrameWork {
    /// <summary>
    /// <para>事件驱动型画布基类，本画布的更新由事件驱动</para>
    /// <para>本类同时为画布基类</para>
    /// <para>在不显示时，逻辑处理依旧正常运行</para>
    /// </summary>
    [DisallowMultipleComponent] [RequireComponent(typeof(RectTransform), typeof(Canvas))]
    public abstract class EventDrivenCanvasBase : MonoBehaviour {
        private bool _isUpdateDrivenCanvas = false; // 子类UpdateDrivenCanvasBase的派生类的Update在这里调用
        protected Canvas _thisCanvas; // 画布组件的引用
        protected GraphicRaycaster _raycaster; // 画布射线检测组件的引用
        private readonly Dictionary<string, List<EventDrivenCanvasBase>> _canvasBaseDic = new Dictionary<string, List<EventDrivenCanvasBase>>(); // 存储本Canvas下嵌套的拥有管理器的Canvas，第一个参数为GameObjectName
        private readonly Dictionary<string, List<UIBehaviour>> _controlDic = new Dictionary<string, List<UIBehaviour>>(); // 存储组件的字典，第一个参数为GameObjectName

        [Tooltip("启用画布内元素控制")] [SerializeField] private bool activeElementControl = true; // 启动元素控制开关，当启动时，将会填充UI元素的字典

        private void Awake() {
            _isUpdateDrivenCanvas = this is UpdateDrivenCanvasBase;
            EnsureFields();
            ToggleEnableStatus(false); // 这里关一下，默认关闭，如果需要打开，重写OnStart

            if (TryGetComponent<CanvasScaler>(out var cs)) { // 先移除掉，嵌套模式不需要CanvasScaler
                Destroy(cs);
            }

            // 面板类的delegation现象（面向对象概念）
            FindChildrenCanvasCtrl();

            // 组件配置
            if (activeElementControl) {
                FindChildrenControl<Button>(); // 按钮类型
                FindChildrenControl<Dropdown>(); // 下拉菜单
                FindChildrenControl<Image>(); // 图像
                FindChildrenControl<InputField>(); // 输入区
                FindChildrenControl<ScrollRect>(); // 可滚动面板类型
                FindChildrenControl<Scrollbar>(); // 滚动条
                FindChildrenControl<Slider>(); // 滑块
                FindChildrenControl<Toggle>(); // 开关
                FindChildrenControl<ToggleGroup>(); // 开关组
                FindChildrenControl<Text>(); // 文本
                FindChildrenControl<TextMeshProUGUI>(); // TMP文本必须是这个类
            }
        }

        /// <summary>
        /// 生命周期函数Start，使用IEnumerator类型方便做特殊操作
        /// </summary>
        /// <returns></returns>
        private IEnumerator Start() {
            // 调用自定义的Start函数
            yield return StartCoroutine(OnStart());

            // 启用画布
            EnableCanvas();
        }

        #region 面板控制

        /// <summary>
        /// 启用画布
        /// </summary>
        public void EnableCanvas() {
            if (_thisCanvas.enabled) return; // 避免重复调用

            if (!_thisCanvas.gameObject.activeSelf) _thisCanvas.gameObject.SetActive(true);

            ToggleEnableStatus(true);
            OnEnableCanvas();
            // 为UpdateDrivenCanvas提供扩展，使用协程做到更新
            if (_isUpdateDrivenCanvas) {
                StartCoroutine("PretendUpdate");
            }
        }

        /// <summary>
        /// 禁用画布
        /// </summary>
        public void DisableCanvas(bool applyToGameObject = false) {
            if (!_thisCanvas.enabled) return;

            OnDisableCanvas();
            StopAllCoroutines();
            ToggleEnableStatus(false);

            if (applyToGameObject) _thisCanvas.gameObject.SetActive(false);
        }

        #endregion

        #region 虚函数

        /// <summary>
        /// <para>在生命周期函数Start中调用</para>
        /// <para>因为迭代器返回类型的特殊性，如果是当作执行一次的函数写时，末尾需要加上<c>yield break;</c>以满足拥有迭代表达式的要求</para>
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator OnStart() {
            yield break;
        }

        /// <summary>
        /// <para>虚函数，启用面板后执行的操作</para>
        /// <para>父类函数为空</para>
        /// </summary>
        protected virtual void OnEnableCanvas() { }

        /// <summary>
        /// <para>虚函数，关闭面板前执行的操作</para>
        /// <para>父类函数为空</para>
        /// </summary>
        protected virtual void OnDisableCanvas() { }

        #endregion

        #region 对象相关

        /// <summary>
        /// 确保引用
        /// </summary>
        protected void EnsureFields() {
            _thisCanvas = GetComponent<Canvas>();
            _raycaster = GetComponent<GraphicRaycaster>();
        }

        /// <summary>
        /// 开关字段
        /// </summary>
        /// <param name="state"></param>
        protected void ToggleEnableStatus(bool state) {
            _thisCanvas.enabled = state;
            if (_raycaster != null) _raycaster.enabled = state;
        }

        /// <summary>
        /// 搜寻物体，仅在画布初始化时调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void FindChildrenControl<T>() where T : UIBehaviour {
            var controls = this.GetComponentsInChildren<T>();
            foreach (var ctrl in controls) {
                var objName = ctrl.gameObject.name;
                if (_controlDic.ContainsKey(objName)) {
                    _controlDic[objName].Add(ctrl);
                } else {
                    _controlDic.Add(objName, new List<UIBehaviour>() { ctrl });
                }
            }
        }

        /// <summary>
        /// 找到所有的动态画布，均应该挂载继承自EventDrivenCanvasBase的组件
        /// </summary>
        private void FindChildrenCanvasCtrl() {
            var controls = this.GetComponentsInChildren<EventDrivenCanvasBase>();
            foreach (var ctrl in controls) {
                var objName = ctrl.gameObject.name;
                if (_canvasBaseDic.ContainsKey(objName)) {
                    _canvasBaseDic[objName].Add(ctrl);
                } else {
                    _canvasBaseDic.Add(ctrl.gameObject.name, new List<EventDrivenCanvasBase>() { ctrl });
                }
            }
        }

        /// <summary>
        /// 获取某个对象中的特定类型控件
        /// </summary>
        /// <param name="gameObjectName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetControl<T>(string gameObjectName) where T : UIBehaviour {
            if (_controlDic.ContainsKey(gameObjectName)) {
                var ls = _controlDic[gameObjectName];
                foreach (var behaviour in ls) {
                    if (behaviour is T) return behaviour as T;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取动态画布，以基类获取
        /// </summary>
        /// <param name="canvasName">画布名称</param>
        /// <returns>基类EventDrivenCanvasBase</returns>
        public EventDrivenCanvasBase GetCanvasControl(string canvasName) {
            return _canvasBaseDic.ContainsKey(canvasName) ? _canvasBaseDic[canvasName][0] : null;
        }

        /// <summary>
        /// 获取动态画布数组，如果有多个相同类型的画布，请使用此方法
        /// </summary>
        /// <param name="canvasName">画布名称</param>
        /// <returns></returns>
        public EventDrivenCanvasBase[] GetCanvasControls(string canvasName) {
            return _canvasBaseDic.ContainsKey(canvasName) ? _canvasBaseDic[canvasName].ToArray() : null;
        }

        /// <summary>
        /// 获取动态画布，以实际类型获取
        /// </summary>
        /// <typeparam name="T">必须继承自EventDrivenCanvasBase或UpdateDrivenCanvasBase</typeparam>
        /// <returns>传入类型的对象</returns>
        public T GetCanvasControl<T>() where T : EventDrivenCanvasBase {
            return _canvasBaseDic.ContainsKey(typeof(T).Name) ? _canvasBaseDic[typeof(T).Name][0] as T : null;
        }

        #endregion
    }
}