using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class Target : MonoBehaviour
    {
		[HideInInspector]
        public float Radius = 1.0f;
		[HideInInspector]
        public float RotationSpeed = 3.0f;
		[HideInInspector]
        public float Angle;
 
        [HideInInspector]
        public Vector3 Centre;

        public GameObject OwnedBy;

        [UsedImplicitly]
        private void Awake()
        {
            tag = "Target";
        }

        [UsedImplicitly]
        private void Update()
        {
        }
    }
}