using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public List<TargetSpawner> TargetSpawners;
        private List<Transform> TargetSpawnersOnTargetMovers;

        public static GameObject PauseMenu = null;
        public Canvas FadeCanvas = null;

        private float AlphaFadeValue = 1.0f;
        private bool FinishedFade;

        public static bool IsPaused;
        public static bool IsInGame;

        [UsedImplicitly]
        private void Start()
        {
            PauseMenu = GameObject.FindWithTag("PauseMenu");
            PauseMenu.SetActive(false);
        }

        [UsedImplicitly]
        private void Update()
        {
            if (FinishedFade)
            {
                if (!IsPaused)
                {
                    FadeCanvas.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0.0f);
                }
                else
                {
                    FadeCanvas.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                }

                return;
            }

            FadeIn(0.0f);
        }

        public static void Pause()
        {
            Time.timeScale = 0.0f;
            IsPaused = true;
            ShowPauseMenu();

            if (GameObject.Find("Player").GetComponent<Player.Player>().Pointer)
            {
                string[] LayerStrings = { "UI" };
                GameObject.Find("Player").GetComponent<Player.Player>().Pointer.InteractableMask = LayerMask.GetMask(LayerStrings); // UI
            }
        }

        public static void Resume()
        {
            Time.timeScale = 1.0f;
            IsPaused = false;
            HidePauseMenu();

            if (GameObject.Find("Player").GetComponent<Player.Player>().Pointer)
            {
                string[] LayerStrings = {"Interactable"};
                GameObject.Find("Player").GetComponent<Player.Player>().Pointer.InteractableMask = LayerMask.GetMask(LayerStrings); // Interactable
            }
        }

        public void FadeIn(float TargetAlpha)
        {
            // Fade in
            AlphaFadeValue -= Time.deltaTime;

            FadeCanvas.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, AlphaFadeValue);

            if (AlphaFadeValue < TargetAlpha)
            {
                FinishedFade = true;
                AlphaFadeValue = TargetAlpha;
            }
        }

        public void FadeOut(float TargetAlpha)
        {
            // Fade in
            AlphaFadeValue += Time.deltaTime;

            FadeCanvas.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, AlphaFadeValue);

            if (AlphaFadeValue > TargetAlpha)
            {
                FinishedFade = true;
                AlphaFadeValue = TargetAlpha;
            }
        }

        public static void Quit()
        {
            Application.Quit();
        }

        private static void HidePauseMenu()
        {
            PauseMenu.SetActive(false);
        }

        private static void ShowPauseMenu()
        {
            PauseMenu.SetActive(true);
        }
    }
}
