using System.Collections.Generic;
using System.Linq;
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

        [UsedImplicitly]
        private void Start()
        {

        }

        [UsedImplicitly]
        private void Update()
        {
		
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
