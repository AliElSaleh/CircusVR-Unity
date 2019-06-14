using System;
using JetBrains.Annotations;
using TMPro;
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

        public Canvas FadeCanvas = null;

        private Image ButtonImage;

        private float AlphaFadeValue;

        private bool StartPressed;

        [UsedImplicitly]
        private void Awake()
        {
            ButtonImage = GetComponent<Image>();
        }

        [UsedImplicitly]
        private void Update()
        {
            if (StartPressed)
            {
                // Fade out
                AlphaFadeValue += Time.deltaTime;

                FadeCanvas.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, AlphaFadeValue);
                
                if (AlphaFadeValue >= 1.0f)
                {
                    AlphaFadeValue = 1.0f;
                    StartPressed = false;
                    SceneManager.LoadScene(1, LoadSceneMode.Single);
                }
            }
        }

        public void OnPointerEnter(PointerEventData EventData)
        {
            ButtonImage.color = HoverColor;
        }

        public void OnPointerExit(PointerEventData EventData)
        {
            ButtonImage.color = NormalColor;
        }

        public void OnPointerDown(PointerEventData EventData)
        {
            ButtonImage.color = DownColor;
        }

        public void OnPointerUp(PointerEventData EventData)
        {
        }

        public void OnPointerClick(PointerEventData EventData)
        {
            ButtonImage.color = HoverColor;

            switch (Type)
            {
                case ButtonType.Start:
                    StartPressed = true;
                break;

                case ButtonType.Options:
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You lost";
                break;

                case ButtonType.Exit:
                    Application.Quit();
                break;
            }
        }
    }
}
