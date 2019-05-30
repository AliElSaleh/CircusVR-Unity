using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class TargetSpawner : MonoBehaviour
    {
        [Range(1.0f, 20.0f)]
        public float Radius = 1.0f;

        [Range(2, 50)]
        public int NumberOfObjects = 2;

        [Range(0.3f, 5.0f)]
        public float RotationSpeed = 3.0f;

        public Target ObjectToSpawn = null;

        private GameObject[] Targets;
        private float Angle;

        [UsedImplicitly]
        private void Awake()
        {
	        // Get all targets in world
	        Targets = GameObject.FindGameObjectsWithTag("Target");
        }

        [UsedImplicitly]
        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
			// Draw the radius
			Handles.DrawWireDisc(transform.position, transform.right, Radius);	

			Gizmos.color = Color.white;
			Gizmos.DrawSphere(transform.position, 0.1f);
            #endif
        }

        public void Generate()
        {
            // Get all targets in world
            Targets = GameObject.FindGameObjectsWithTag("Target");

            // Destroy them all
            foreach (var Target in Targets)
            {
                #if UNITY_EDITOR
				DestroyImmediate(Target);
                #endif
            }

            // Spawn new targets in a shape of a circle
            Vector3 SpawnPoint = Vector3.zero;
            float Spacing = (360.0f / NumberOfObjects);

            for (int i = 0; i < NumberOfObjects; i++)
            {
				// The Point on cirumference of circle
	            SpawnPoint.x = transform.position.x;
	            SpawnPoint.y = transform.position.y + Radius * Mathf.Cos(Angle*Mathf.Deg2Rad);
	            SpawnPoint.z = transform.position.z + Radius * Mathf.Sin(Angle*Mathf.Deg2Rad);

				// Spawn the target on the spawn point and initialize its variables
	            Target Target = Instantiate(ObjectToSpawn, SpawnPoint, ObjectToSpawn.transform.rotation);
	            Target.name = ObjectToSpawn.name + "_" + i;
	            Target.Centre = transform.position;
	            Target.Radius = Radius;
	            Target.Angle = Angle*Mathf.Deg2Rad;
	            Target.RotationSpeed = RotationSpeed;

				// Increase the angle for next iteration
	            Angle += Spacing;
            }
        }
    }
}