﻿using UnityEngine;
using JetBrains.Annotations;
using TMPro;

namespace Assets.Scripts
{
    public class Score : MonoBehaviour
    {
        private static int ScoreValue;

        private static TextMeshPro TextMeshProUGUIComponent;

        [UsedImplicitly]
        private void Start()
        {
            TextMeshProUGUIComponent = transform.GetChild(1).GetComponent<TextMeshPro>();
        }
	
        [UsedImplicitly]
        private void Update()
        {
		
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

        public static void Reset(int Value)
        {
            ScoreValue = 0;

            UpdateUI();
        }

        private static void UpdateUI()
        {
            TextMeshProUGUIComponent.text = ScoreValue.ToString();
        }

        public static void SetText(string Text)
        {
            TextMeshProUGUIComponent.text = Text;
        }
    }
}
