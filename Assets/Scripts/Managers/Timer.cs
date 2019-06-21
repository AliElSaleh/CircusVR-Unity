using Assets.Scripts.Managers;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class Timer : MonoBehaviour
    {
        [Range(1, 60)]
        public int TimeInSeconds = 60;

        [Range(1, 30)]
        public int WaitingTimeInSeconds = 2;

        private float TimeLeft;

        public static bool Paused;
        public static bool Finished;

        private LeaderboardManager LeaderboardManager;
        private LevelManager LevelManager;
        private TextMeshProUGUI TextMeshProUGUIComponent;

        [UsedImplicitly]
        private void Start()
        {
            TextMeshProUGUIComponent = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TimeLeft = TimeInSeconds;

            LeaderboardManager = GameObject.Find("Leaderboard").GetComponent<LeaderboardManager>();
            LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        }

        [UsedImplicitly]
        private void Update()
        {
            if (Application.isPlaying && !Paused)
                Countdown();
        }

        [UsedImplicitly]
        private void OnValidate()
        {
            TextMeshProUGUIComponent = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUIComponent.text = TimeInSeconds.ToString();
        }

        private void Countdown()
        {
            // Decrement the time left
            TimeLeft -= Time.deltaTime;

            // Update UI
            if (TimeLeft > 0.0f && !Finished)
            {
                var TimeLeftInt = (int)TimeLeft;
                TextMeshProUGUIComponent.text = TimeLeftInt.ToString();
            }

            // Game over
            if (TimeLeft <= 0.0f && !Finished)
            {
                Finished = true;

                LeaderboardManager.AddEntry(ScoreManager.GetScore());
                LevelManager.DisplayEndGame();
            }

            // Restart game
            if (TimeLeft < 0.0f && LevelManager.ShouldRestart)
            {
                TimeLeft = TimeInSeconds;
                Finished = false;

                ScoreManager.ResetScore();
            }
        }

        public float GetTimeLeft()
        {
            return TimeLeft;
        }
    }
}
