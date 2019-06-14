using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public List<TargetSpawner> TargetSpawners;
        private List<Transform> TargetSpawnersOnTargetMovers;
        public List<TargetMover> TargetMovers;

        public Canvas FadeCanvas = null;

        private float AlphaFadeValue = 1.0f;
        private bool FinishedFade;

        public static bool IsPaused;

        [UsedImplicitly]
        private void Start()
        {
            gameObject.SetActive(false);
        }

        [UsedImplicitly]
        private void Update()
        {
            if (FinishedFade)
            {
                if (IsPaused)
                {
                    gameObject.SetActive(true);

                    FadeOut(0.5f);
                }

                gameObject.SetActive(false);

                return;
            }

            FadeIn(0.0f);
        }

        public static void Pause()
        {
            Time.timeScale = 0.0f;
            IsPaused = true;
        }

        public static void Resume()
        {
            Time.timeScale = 1.0f;
            IsPaused = false;
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

        public void ResetLevel()
        {
            SpawnTargets();
        }

        public void SpawnTargets()
        {
            foreach (var Spawner in TargetSpawners)
            {
                Spawner.ResetTargets();
            }
        }
    }
}
