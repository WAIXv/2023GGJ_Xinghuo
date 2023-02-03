using System.Collections;

namespace Utils.UIFrameWork {
    /// <summary>
    /// <para>更新驱动型画布基类，本画布的更新主要由更新方法驱动</para>
    /// </summary>
    /// <remarks>
    /// <para>总的面板基类为EventDrivenCanvasBase</para>
    /// <para>更新的本质是协程，调用是在父类EventDrivenCanvasBase里</para>
    /// </remarks>
    public abstract class UpdateDrivenCanvasBase : EventDrivenCanvasBase {
        /// <summary>
        /// 用于父类EventDrivenCanvasBase启用协程
        /// </summary>
        /// <returns></returns>
        private IEnumerator PretendUpdate() {
            while (true) {
                OnUpdate();
                yield return null;
            }
        }

        #region 虚函数

        /// <summary>
        /// 界面的更新方法
        /// </summary>
        protected abstract void OnUpdate();

        #endregion
    }
}