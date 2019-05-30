using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class Clown : MonoBehaviour
    {
        [Range(0.5f, 10.0f)]
        public float AcceptanceRadius = 1.0f;

        public int MoveSpeed = 5;

        public Vector3 Target = new Vector3(0.0f, 0.0f, 0.0f);

        private Vector3 Direction;
        private Vector3 Location;

        private void Start()
        {
            Location = transform.position;

            Target.y = transform.position.y;

            Direction = Target - Location;
        }

        private void Update()
        {
            Location = transform.position;

            if (Vector3.Distance(Location, Target) > AcceptanceRadius)
                transform.Translate(Direction.normalized * MoveSpeed * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            // Draw the spawn radius
            Handles.DrawWireDisc(Target, transform.up, AcceptanceRadius);
#endif
        }
    }
}