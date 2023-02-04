namespace Utils.EventCenter
{
    [System.Serializable]
    public enum EventTypes
    {
        Input_KeyDown=0,
        Input_KeyUp=1,
        
        UI_UpdateProgressBar=99,

        MouseEnterUI = 2,
        MouseExitUI = 3,
        MouseClickUI = 4,
        
        RootMove = 5,
    }
}