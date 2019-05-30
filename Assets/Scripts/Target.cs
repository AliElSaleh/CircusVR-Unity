using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class Target : MonoBehaviour
    {
		[HideInInspector]
        public float Radius = 1.0f;
		[HideInInspector]
        public float RotateSpeed = 3.0f;
		[HideInInspector]
        public float Angle;
 
        [HideInInspector]
        public Vector3 Centre;

        [UsedImplicitly]
        private void Awake()
        {
            tag = "Target";
        }

        [UsedImplicitly]
        private void Update()
        {
	        Angle += RotateSpeed * Time.deltaTime;

			var Offset = new Vector3(0.0f, Mathf.Sin(Angle), Mathf.Cos(Angle)) * Radius;
			transform.position = Centre + Offset;
        }
    }
}