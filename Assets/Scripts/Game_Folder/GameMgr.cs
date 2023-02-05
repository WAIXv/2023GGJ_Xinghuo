using System;
using System.Collections;
using System.Collections.Generic;
using Blocks_Folder;
using Game_Folder.UIScripts;
using Map_Folder;
using UnityEngine;
using UnityEngine.Serialization;
using Utils.EventCenter;

public class GameMgr : MonoBehaviour
{
    private static GameMgr instance;

    public RectTransform basePoint;
    private float originY;

    [SerializeField] private int originStep;
    public int moveStep;
    public bool gameOn;

    public Vector2 startPoint;

    public float endY;

    [SerializeField] private float midY;

    [SerializeField] private float scrollSpeed;
    
    private int curLevel = 0;

    [SerializeField] private Animator treeAnim;

    public GameObject leafObj;

    private void Start()
    {
        instance = this;
        moveStep = originStep;
        originY = basePoint.anchoredPosition.y;
        EventCenter.GetInstance().AddEventListener(EventTypes.RootMove,OnRootMove);
        EventCenter.GetInstance().AddEventListener(EventTypes.LevelFinish,OnLevelFinish);
        leafObj.SetActive(false);
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

        var localPosition = basePoint.localPosition;
        localPosition = new Vector3(localPosition.x, localPosition.y + delta.y, localPosition.z);
        basePoint.localPosition = localPosition;
    }
    
    public void OnLevelFinish()
    {
        gameOn = false;
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
    }

    public IEnumerator Level0Enter()
    {
        while (basePoint.anchoredPosition.y < midY)
        {
            SlideToMid();
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Level0Finish()
    {
        leafObj.SetActive(false);
        while (basePoint.anchoredPosition.y > originY)
        {
            basePoint.transform.Translate(Vector3.down * scrollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.2f);
        PlayAnim(curLevel);
        yield return new WaitForSeconds(1f);
        ReloadMap(Resources.Load<TextAsset>("LevelMaptxt/level_1"));
        endY = originY + (MapCreator.GetInstance()._mapMatrix.Length - 1) * 80f;
        moveStep = originStep;
        curLevel++;
        while (basePoint.anchoredPosition.y < midY)
        {
            SlideToMid();
            yield return new WaitForSeconds(0.01f);
        }
        leafObj.GetComponent<LeafScript>().textComp.text = moveStep.ToString();
        leafObj.SetActive(true);
        gameOn = true;
    }

    IEnumerator Level1Finish()
    {
        leafObj.SetActive(false);
        while (basePoint.anchoredPosition.y > originY)
        {
            basePoint.transform.Translate(Vector3.down * scrollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.2f);
        PlayAnim(curLevel);
        yield return new WaitForSeconds(1f);
        ReloadMap(Resources.Load<TextAsset>("LevelMaptxt/level_2"));
        endY = originY +  (MapCreator.GetInstance()._mapMatrix.Length - 1) * 80f;
        moveStep = originStep;
        curLevel++;
        while (basePoint.anchoredPosition.y < midY)
        {
            SlideToMid();
            yield return new WaitForSeconds(0.01f);
        }
        leafObj.GetComponent<LeafScript>().textComp.text = moveStep.ToString();
        leafObj.SetActive(true);
        gameOn = true;
    }

    IEnumerator Level2Finish()
    {
        yield return null;
    }

    void SlideToMid()
    {
        basePoint.transform.Translate(Vector3.up * scrollSpeed);
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
