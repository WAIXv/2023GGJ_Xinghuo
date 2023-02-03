namespace Utils.EventCenter
{
    [System.Serializable]
    public enum EventTypes
    {
        Input_KeyDown=0,
        Input_KeyUp=1,
        
        UI_UpdateProgressBar=99,

        #region 相机运动

        /// <summary>
        /// 传输相机相对信息
        /// </summary>
        Camera_UpdateRelativeInfo,
        
        /// <summary>
        /// 启用外部运动
        /// </summary>
        Camera_ExternMotionStart,
        
        /// <summary>
        /// 外部运动结束
        /// </summary>
        Camera_ExternMotionOver,

        #endregion
    }
}