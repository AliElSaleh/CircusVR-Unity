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

        public static bool Finished;

        private TextMeshProUGUI TextMeshProUGUIComponent;

        [UsedImplicitly]
        private void Start()
        {
            TextMeshProUGUIComponent = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TimeLeft = TimeInSeconds;
        }

        [UsedImplicitly]
        private void Update()
        {
            if (Application.isPlaying)
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
                Player.Events.DisableInput();
                Player.Pointer.HideLine();
                Player.Pointer.HideReticule();
                Finished = true;

                GameObject.Find("Leaderboard").GetComponent<LeaderboardManager>().AddEntry(ScoreManager.GetScore());
            }

            // Restart game
            if (TimeLeft < -WaitingTimeInSeconds)
            {
                TimeLeft = TimeInSeconds;
                Finished = false;

                Player.Events.EnableInput();
                Player.Pointer.ShowLine();
                Player.Pointer.ShowReticule();

                ScoreManager.ResetScore();
            }
        }

        public float GetTimeLeft()
        {
            return TimeLeft;
        }
    }
}
