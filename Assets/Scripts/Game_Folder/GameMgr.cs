using System;
using System.Collections;
using System.Collections.Generic;
using Blocks_Folder;
using Map_Folder;
using UnityEngine;
using UnityEngine.Serialization;
using Utils.EventCenter;

public class GameMgr : MonoBehaviour
{
    private static GameMgr instance;

    public RectTransform basePoint;
    private float originY;
    
    public int moveStep;
    public bool gameOn;

    public Vector2 startPoint;

    public float endY;
    
    private int curLevel = 0;

    public Animator treeAnim;

    private void Start()
    {
        instance = this;
        originY = basePoint.anchoredPosition.y;
        EventCenter.GetInstance().AddEventListener(EventTypes.RootMove,OnRootMove);
        EventCenter.GetInstance().AddEventListener(EventTypes.LevelFinish,OnLevelFinish);
    }

    private void Update()
    {
        if(gameOn)
        {
            SlideMap();
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

    void SlideMap()
    {
        var delta = Input.mouseScrollDelta * -50f;

        if(delta.y + basePoint.transform.localPosition.y < -490f || delta.y + basePoint.transform.localPosition.y > endY) return;
        
        basePoint.localPosition = new Vector3(basePoint.localPosition.x, basePoint.localPosition.y + delta.y,
            basePoint.localPosition.z);
    }
    
    public void OnLevelFinish()
    {
        GameMgr.GetInstance().gameOn = false;
        switch (curLevel)
        {
            case 0:
                StartCoroutine(Level0Finish());
                break;
            case 1:
                StartCoroutine(Level1Finish());
                break;
            case 2:
                break;
        }

        curLevel++;
    }

    IEnumerator Level0Finish()
    {
        var scrollSpeed = 20f;
        Debug.Log(basePoint.anchoredPosition.y);
        while (basePoint.anchoredPosition.y > originY)
        {
            basePoint.transform.Translate(Vector3.down * scrollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.2f);
        PlayAnim(curLevel);
        yield return new WaitForSeconds(1f);
        ReloadMap(Resources.Load<TextAsset>("LevelMaptxt/level_1"));
        gameOn = true;
        endY = originY + (MapCreator.GetInstance()._mapMatrix.Length - 1) * 80f;

    }

    IEnumerator Level1Finish()
    {
        var scrollSpeed = 20f;
        Debug.Log(basePoint.anchoredPosition.y);
        while (basePoint.anchoredPosition.y > originY)
        {
            basePoint.transform.Translate(Vector3.down * scrollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.2f);
        PlayAnim(curLevel);
        yield return new WaitForSeconds(1f);
        ReloadMap(Resources.Load<TextAsset>("LevelMaptxt/level_2"));
        gameOn = true;
        endY = originY +  (MapCreator.GetInstance()._mapMatrix.Length - 1) * 80f;
    }

    IEnumerator Level2Finish()
    {
        yield return null;
    }

    void PlayAnim(int level)
    {
        switch (level)
        {
            case 0:
                treeAnim.Play("grow");
                break;
            case 1:
                treeAnim.Play("grow2");
                break;
            case 2:
                treeAnim.Play("grow3");
                break;
        }
    }

    void ReloadMap(TextAsset newFile)
    {
        MapCreator.GetInstance().ReloadMap(newFile);
        MapCreator.GetInstance().CreateMap();
    }
}
