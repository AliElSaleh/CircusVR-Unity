using Assets.Scripts.Managers;
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
        Start, Options, Menu, Resume, Restart, Exit
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
            FadeCanvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        }

        [UsedImplicitly]
        private void Start()
        {
            FadeCanvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
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
                    SceneManager.LoadScene(1);
                    LevelManager.IsInGame = true;
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

                case ButtonType.Menu:
                    ResetMenu();
                    SceneManager.LoadScene(0);
                break;

                case ButtonType.Resume:
                    LevelManager.Resume();
                break;

                case ButtonType.Exit:
                    LevelManager.Quit();
                break;
            }
        }

        public void ResetMenu()
        {
            StartPressed = false;
            LevelManager.IsInGame = false;
            AlphaFadeValue = 0.0f;
            FadeCanvas.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, AlphaFadeValue);
        }
    }
}
