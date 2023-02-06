using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game_Folder.UIScripts
{
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
            GameMgr.GetInstance().OnStartClick();
            gameObject.SetActive(false);
        }
    }
}
