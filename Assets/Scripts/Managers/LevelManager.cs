using Assets.Scripts.Player;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static GameObject PauseMenu;
        public static GameObject GameOver;
        public static Canvas FadeCanvas;

        public GameObject GameOverParticlePrefab;

        private float AlphaFadeValue = 1.0f;
        private bool FinishedFade;

        public static bool ShouldRestart;
        public static bool IsPaused;
        public static bool IsInGame;
        public static bool IsGameOver;

        [UsedImplicitly]
        private void Start()
        {
            PauseMenu = GameObject.FindWithTag("PauseMenu");
            PauseMenu.SetActive(false);

            GameOver = GameObject.FindWithTag("GameOver");
            GameOver.SetActive(false);

            FadeCanvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        }

        [UsedImplicitly]
        private void Update()
        {
            if (FinishedFade)
                return;

            FadeIn(0.0f);
        }

        public static void Pause()
        {
            Time.timeScale = 0.0f;
            IsPaused = true;
            FadeCanvas.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
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
            FadeCanvas.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0.0f);
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

        public void DisplayEndGame()
        {
            Timer.Paused = true;
            IsGameOver = true;
            IsPaused = true;

            SpawnConfetti();

            ShowGameOver();
        }

        private void SpawnConfetti()
        {
            GameOverParticlePrefab = Instantiate(GameOverParticlePrefab, new Vector3(-4.7f, 3.65f, 0.05f), Quaternion.identity);
            GameOverParticlePrefab.GetComponent<ParticleSystem>().Play();
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

        private static void HideGameOver()
        {
            GameOver.SetActive(false);
        }

        private static void ShowGameOver()
        {
            GameOver.SetActive(true);
        }
    }
}
