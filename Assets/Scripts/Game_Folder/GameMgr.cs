using System;
using System.Collections;
using System.Collections.Generic;
using Blocks_Folder;
using Map_Folder;
using UnityEngine;
using Utils.EventCenter;

public class GameMgr : MonoBehaviour
{
    private static GameMgr instance;
    
    public int moveStep;

    public Vector2 startPoint;
    public Vector2 finalPoint;

    private void Start()
    {
        instance = this;
        EventCenter.GetInstance().AddEventListener(EventTypes.RootMove,OnRootMove);
    }

    public static GameMgr GetInstance()
    {
        return instance;
    }

    public void SetFinalandStartPoint()
    {
        var matrix = MapCreator.GetInstance()._mapMatrix;
        matrix[(int)startPoint.x][(int)startPoint.y].GetComponent<BlockBase>().info.moveState = 0;
    }
    
    void OnRootMove()
    {
        moveStep--;
    }
}
