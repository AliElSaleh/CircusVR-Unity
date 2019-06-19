using System;
using UnityEngine;
using JetBrains.Annotations;
using UnityEditor;

namespace Assets.Scripts
{
    public class TargetMover : MonoBehaviour
    {
        [Range(10.0f, 100.0f)]
        public float RotationSpeed = 10.0f;

        [Range(1.0f, 200.0f)]
        public float Radius = 2.0f;

        [Range(0.0f, 360.0f)]
        public float Offset = 0.0f;

		public Vector3 Normal = Vector3.up;

        public Transform TargetSpawner = null;

        private float PositionAngle;
        private float RotationAngle;
        public bool Clockwise = true;

        [UsedImplicitly]
        private void Start()
        {
            TargetSpawner.parent = transform;
            Vector3 Temp = Normal;
            Normal = transform.up;
            if (Temp.x >= 1.0f)
            {
	            Normal = transform.right;
            }
			else if (Temp.y >= 1.0f)
            {
	            Normal = transform.up;
            }
            else if (Temp.z >= 1.0f)
            {
	            Normal = transform.forward;
            }

            ResetPosition();
        }

        public void ResetPosition()
        {
            PositionAngle = Offset;

            Vector3 PointOnCircle = Vector3.zero;

            if (Normal.x >= 1.0f)
            {
	            PointOnCircle = new Vector3(
		            transform.position.x,
		            transform.position.y + Radius * Mathf.Cos(PositionAngle * Mathf.Deg2Rad),
		            transform.position.z + Radius * Mathf.Sin(PositionAngle * Mathf.Deg2Rad));
            }
			else if (Normal.y >= 1.0f)
            {
	            PointOnCircle = new Vector3(
		            transform.position.x + Radius * Mathf.Cos(PositionAngle * Mathf.Deg2Rad),
		            transform.position.y,
		            transform.position.z + Radius * Mathf.Sin(PositionAngle * Mathf.Deg2Rad));
            }
            else if (Normal.z >= 1.0f)
            {
	            PointOnCircle = new Vector3(
		            transform.position.x + Radius * Mathf.Cos(PositionAngle * Mathf.Deg2Rad),
		            transform.position.y + Radius * Mathf.Sin(PositionAngle * Mathf.Deg2Rad),
		            transform.position.z);
            }

            TargetSpawner.transform.position = PointOnCircle;
        }

        [UsedImplicitly]
        private void Update()
        {
            MoveAroundCenter();
            Rotate();
        }

        [UsedImplicitly]
        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
            // Draw the movement radius
            Handles.DrawWireDisc(transform.position, Normal, Radius);

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

            Vector3 PointOnCircle = Vector3.zero;

            if (Normal.x >= 1.0f)
            {
	            PointOnCircle = new Vector3(
		            transform.position.x,
		            transform.position.y + Radius * Mathf.Cos(PositionAngle * Mathf.Deg2Rad),
		            transform.position.z + Radius * Mathf.Sin(PositionAngle * Mathf.Deg2Rad));
            }
            else if (Normal.y >= 1.0f)
            {
	            PointOnCircle = new Vector3(
		            transform.position.x + Radius * Mathf.Cos(PositionAngle * Mathf.Deg2Rad),
		            transform.position.y,
		            transform.position.z + Radius * Mathf.Sin(PositionAngle * Mathf.Deg2Rad));
            }
            else if (Normal.z >= 1.0f)
            {
	            PointOnCircle = new Vector3(
		            transform.position.x + Radius * Mathf.Cos(PositionAngle * Mathf.Deg2Rad),
		            transform.position.y + Radius * Mathf.Sin(PositionAngle * Mathf.Deg2Rad),
		            transform.position.z);
            }

            TargetSpawner.position = PointOnCircle;

			if (PositionAngle > 360.0f)
				PositionAngle = 0.0f;
        }

        private void Rotate()
        {
	        if (Normal.x >= 1.0f)
		        RotationAngle += RotationSpeed * Time.deltaTime;
			else if (Normal.y >= 1.0f)
		        RotationAngle -= RotationSpeed * Time.deltaTime;
	        else if (Normal.z >= 1.0f)
		        RotationAngle += RotationSpeed * Time.deltaTime;
			else
		        RotationAngle -= RotationSpeed * Time.deltaTime;

	        transform.Rotate(Normal, RotationAngle);
			RotationAngle = 0;
        }
    }
}
