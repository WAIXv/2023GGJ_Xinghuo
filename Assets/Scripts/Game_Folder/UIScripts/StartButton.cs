using System;
using System.Collections;
using System.Collections.Generic;
using Game_Folder;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = onSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = offSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIMgr.GetInstance().OnStartClick();
        gameObject.SetActive(false);
    }
}
