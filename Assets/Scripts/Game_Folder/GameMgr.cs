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

    public RectTransform rootPoint;
    
    public int moveStep;

    public Vector2 startPoint;
    public Vector2 finalPoint;

    private Vector2 mousecur;
    private Vector2 mousepre;
    private Vector2 delta;

    private void Start()
    {
        instance = this;
        EventCenter.GetInstance().AddEventListener(EventTypes.RootMove,OnRootMove);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mousecur = mousepre;
        }
        if (Input.GetMouseButton(1))
        {
            OnMouseRightClick();
        }

        if (Input.GetMouseButtonUp(1))
        {
            mousepre = mousecur;
        }
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

    void OnMouseRightClick()
    {
        mousecur = Input.mousePosition;
        delta = mousecur - mousepre;
        mousepre = mousecur;
        
        rootPoint.localPosition = new Vector3(rootPoint.localPosition.x, rootPoint.localPosition.y + delta.y,
            rootPoint.localPosition.z);
    }
}
