using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Input
{
    public class ButtonTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public Color32 NormalColor = Color.white;
        public Color32 HoverColor = Color.grey;
        public Color32 DownColor = Color.white;

        private Image Image;

        private void Awake()
        {
            Image = GetComponent<Image>();
        }

        public void OnPointerEnter(PointerEventData EventData)
        {
            Image.color = HoverColor;
        }

        public void OnPointerExit(PointerEventData EventData)
        {
            Image.color = NormalColor;
        }

        public void OnPointerDown(PointerEventData EventData)
        {
            Image.color = DownColor;
        }

        public void OnPointerUp(PointerEventData EventData)
        {
        }

        public void OnPointerClick(PointerEventData EventData)
        {
            Image.color = HoverColor;
        }
    }
}
