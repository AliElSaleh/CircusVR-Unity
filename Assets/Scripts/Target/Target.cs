﻿using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class Target : MonoBehaviour
    {
	    public int Tier1 = 100;
	    public int Tier2 = 250;
	    public int Tier3 = 500;

        [Range(1.0f, 10.0f)]
        public float RespawnDelay = 5.0f;

        private MeshRenderer MeshRenderer;
        private MeshCollider MeshCollider;

        private float TimeCounter;

        [HideInInspector]
        public bool Hit;

        [UsedImplicitly]
        private void Start()
        {
            tag = "Target";

            MeshRenderer = GetComponent<MeshRenderer>();
            MeshCollider = GetComponent<MeshCollider>();
        }

        [UsedImplicitly]
        private void Update()
        {
            if (!Hit)
                return;

            TimeCounter += Time.deltaTime;
            if (TimeCounter > RespawnDelay)
            {
                TimeCounter = 0.0f;
                Hit = false;
                Show();
            }
        }

        public void Hide()
        {
            MeshRenderer.enabled = false;
            MeshCollider.enabled = false;
        }

        public void Show()
        {
            MeshRenderer.enabled = true;
            MeshCollider.enabled = true;
        }
    }
}