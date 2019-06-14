using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Input
{
    public enum ButtonType
    {
        Start, Options, Exit
    }

    public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public ButtonType Type;

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

            switch (Type)
            {
                case ButtonType.Start:
                    SceneManager.LoadScene("Game");
                break;

                case ButtonType.Options:

                break;

                case ButtonType.Exit:
                    Application.Quit();
                break;
            }
        }
    }
}
