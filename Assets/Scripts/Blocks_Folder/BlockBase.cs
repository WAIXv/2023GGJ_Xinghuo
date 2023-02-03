using UnityEngine;
using UnityEngine.EventSystems;

namespace Blocks_Folder
{
    public class BlockBase : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        public BlockAttribute info;

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Enter " + name);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Exit " + name);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Click" + name);
        }
    }
}
