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

        private float TimeLeft;

        private bool bTimerEnded;

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
            if (Application.isPlaying && !bTimerEnded)
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
            TimeLeft -= Time.deltaTime;

            int TimeLeftInt = (int) TimeLeft;
            TextMeshProUGUIComponent.text = TimeLeftInt.ToString();

            if (TimeLeft < 0.0f)
                bTimerEnded = true;
        }
    }
}
