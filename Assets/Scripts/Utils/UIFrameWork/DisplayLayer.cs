namespace Utils.UIFrameWork {
    /// <summary>
    /// 显示层级
    /// </summary>
    public enum DisplayLayer {
        /// <summary>
        /// 系统层，即最顶层，各类重要通知，比如弹窗、完全挡住所有元素的加载动画等使用
        /// </summary>
        System,
        /// <summary>
        /// 操作层，可操作的UI全部放在这个层，比如ESC菜单、商店菜单等
        /// </summary>
        Operation,
        /// <summary>
        /// 信息层，仅为了显示信息，比如战斗时的血条
        /// </summary>
        Information,
        /// <summary>
        /// 背景层
        /// </summary>
        Back
    }
}