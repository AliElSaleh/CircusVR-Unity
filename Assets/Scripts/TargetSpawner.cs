using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class TargetSpawner : MonoBehaviour
    {
        [Range(1.0f, 20.0f)]
        public float SpawnRadius = 1.0f;

        [Range(2, 100)]
        public int NumberOfObjects = 2;

        [Range(0.1f, 20.0f)]
        public float RotationSpeed = 3.0f;

        public Target ObjectToSpawn = null;

        private GameObject[] Objects;

        private float Angle;

        private void Awake()
        {
	        // Get all objects of a certain tag
	        Objects = GameObject.FindGameObjectsWithTag("Target");
        }

        [UsedImplicitly]
        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
			// Draw the spawn radius
			Handles.DrawWireDisc(transform.position, transform.right, SpawnRadius);	

			Gizmos.color = Color.white;
			Gizmos.DrawSphere(transform.position, 0.1f);
            #endif
        }

        public void Generate()
        {
            // Get all spawn points
            Objects = GameObject.FindGameObjectsWithTag("Target");

            // Destroy them all
            foreach (var Object in Objects)
            {
                #if UNITY_EDITOR
				EditorApplication.delayCall += () =>
				{
					if (Application.isPlaying)
						Destroy(Object);
					else
						DestroyImmediate(Object);
				};
                #endif
            }

            // Spawn new objects
            Vector3 SpawnPoint = Vector3.zero;
            float Spacing = (360.0f / NumberOfObjects);

            for (int i = 0; i < NumberOfObjects; i++)
            {
	            SpawnPoint.x = transform.position.x;
	            SpawnPoint.y = transform.position.y + SpawnRadius * Mathf.Cos(Angle*Mathf.Deg2Rad);
	            SpawnPoint.z = transform.position.z + SpawnRadius * Mathf.Sin(Angle*Mathf.Deg2Rad);

	            var Object = Instantiate<Target>(ObjectToSpawn, SpawnPoint, ObjectToSpawn.transform.rotation);
	            Object.name = ObjectToSpawn.name + "_" + i;
	            Object.Centre = transform.position;
	            Object.Radius = SpawnRadius;
	            Object.Angle = Angle*Mathf.Deg2Rad;
	            Object.RotateSpeed = RotationSpeed;

	            Angle += Spacing;
            }
        }
    }
}