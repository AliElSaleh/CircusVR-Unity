using UnityEngine;
using JetBrains.Annotations;
using UnityEditor;

namespace Assets.Scripts
{
    public class TargetMover : MonoBehaviour
    {
        [Range(10.0f, 50.0f)]
        public float RotationSpeed = 10.0f;

        [Range(1.0f, 20.0f)]
        public float Radius = 2.0f;

        private Transform Target;
        private float Angle;
        public bool Clockwise = true;

        [UsedImplicitly]
        private void Start()
        {
            Target = transform;
        }

        [UsedImplicitly]
        private void Update()
        {
		    transform.LookAt(Target);

            RotateAroundTarget();
        }

        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
            // Draw the movement radius
            Handles.DrawWireDisc(transform.position, transform.up, Radius);
            #endif
        }

        private void RotateAroundTarget()
        {
            if (Clockwise)
                Angle += RotationSpeed * Time.deltaTime;
            else
                Angle -= RotationSpeed * Time.deltaTime;

            Vector3 PointOnCircle = new Vector3(transform.position.x + Radius * Mathf.Cos(Angle * Mathf.Deg2Rad),
                transform.position.y,
                transform.position.z + Radius * Mathf.Sin(Angle * Mathf.Deg2Rad));

            transform.RotateAround(transform.position, transform.right, 0.0f);
            //transform.position = PointOnCircle;

            Angle = 0;
        }
    }
}
