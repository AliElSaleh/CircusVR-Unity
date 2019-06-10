using UnityEngine;
using JetBrains.Annotations;
using TMPro;

namespace Assets.Scripts
{
    public class ScoreManager : MonoBehaviour
    {
        private static int ScoreValue;

        private static TextMeshPro TextMeshProUGUIComponent;

        [UsedImplicitly]
        private void Start()
        {
            TextMeshProUGUIComponent = transform.GetChild(1).GetComponent<TextMeshPro>();
        }

        public static void Add(int Value)
        {
            ScoreValue += Value;

            UpdateUI();
        }

        public static void Subtract(int Value)
        {
            ScoreValue -= Value;

            if (ScoreValue < 0)
                ScoreValue = 0;

            UpdateUI();
        }

        public static void ResetScore()
        {
            SetScore(0);;
        }

        public static void SetScore(int Value)
        {
            ScoreValue = Value;

            UpdateUI();
        }

        private static void UpdateUI()
        {
            TextMeshProUGUIComponent.text = ScoreValue.ToString();
        }

        public static int GetScore()
        {
	        return ScoreValue;
        }
    }
}
