using UnityEngine;
using JetBrains.Annotations;
using UnityEditor;

namespace Assets.Scripts
{
    public class TargetMover : MonoBehaviour
    {
        [Range(10.0f, 100.0f)]
        public float RotationSpeed = 10.0f;

        [Range(1.0f, 20.0f)]
        public float Radius = 2.0f;

        public Transform TargetSpawner = null;

        private float PositionAngle;
        private float RotationAngle;
        public bool Clockwise = true;

        [UsedImplicitly]
        private void Start()
        {
            TargetSpawner.parent = transform;

            ResetPosition();
        }

        public void ResetPosition()
        {
            Vector3 PointOnCircle = new Vector3(
                transform.position.x + Radius * Mathf.Cos(PositionAngle * Mathf.Deg2Rad),
                transform.position.y,
                transform.position.z + Radius * Mathf.Sin(PositionAngle * Mathf.Deg2Rad));

            TargetSpawner.transform.position = PointOnCircle;
        }

        [UsedImplicitly]
        private void Update()
        {
	        if (!TargetSpawner)
	        {
				Destroy(gameObject);
				return;
	        }

            if (TargetSpawner.GetComponent<TargetSpawner>().Generating)
                return;

            MoveAroundCenter();
            Rotate();
        }

        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
            // Draw the movement radius
            Handles.DrawWireDisc(transform.position, transform.up, Radius);

			Gizmos.color = Color.white;
			Gizmos.DrawSphere(transform.position, 0.1f);
            #endif
        }

        private void MoveAroundCenter()
        {
            if (Clockwise)
                PositionAngle += RotationSpeed * Time.deltaTime;
            else
                PositionAngle -= RotationSpeed * Time.deltaTime;

            Vector3 PointOnCircle = new Vector3(
	            transform.position.x + Radius * Mathf.Cos(PositionAngle * Mathf.Deg2Rad),
	            transform.position.y,
	            transform.position.z + Radius * Mathf.Sin(PositionAngle * Mathf.Deg2Rad));

            TargetSpawner.position = PointOnCircle;

			if (PositionAngle > 360.0f)
				PositionAngle = 0.0f;
        }

        private void Rotate()
        {
	        RotationAngle -= RotationSpeed * Time.deltaTime;
			transform.Rotate(transform.up, RotationAngle);
			RotationAngle = 0;
        }
    }
}
