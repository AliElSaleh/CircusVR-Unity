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
        private bool FinishedFade = false;

        [UsedImplicitly]
        private void Start()
        {

        }

        [UsedImplicitly]
        private void Update()
        {
            if (FinishedFade)
                return;

            // Fade in
            AlphaFadeValue -= Time.deltaTime;

            FadeCanvas.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, AlphaFadeValue);

            if (AlphaFadeValue < 0.0f)
            {
                FinishedFade = true;
                AlphaFadeValue = 0.0f;
            }
        }

        public void StartGame()
        {
            GameObject.Find("Start").GetComponent<Image>().enabled = false;
        }

        public void Quit()
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
