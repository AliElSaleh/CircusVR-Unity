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

        public GameObject ObjectToSpawn;
        public Vector3 SpawnOffset = new Vector3(0.0f, 0.5f, 0.0f);

        [SerializeField]
        [HideInInspector]
        private int TotalObjects = 2;

        private float Angle;

        private void Update()
        {
            UpdateObjects();
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

        private void UpdateObjects()
        {
            // Get all spawn points
            var Objects = GameObject.FindGameObjectsWithTag("Target");

            float Spacing = (360.0f / TotalObjects);
            float T = Mathf.Deg2Rad * Spacing;

            Angle += 5 * Time.deltaTime;

            var Offset = new Vector3(0.0f, Mathf.Cos(Angle), Mathf.Sin(Angle)) * SpawnRadius;
            
            // Update them all
            foreach (var Object in Objects)
            {
                var NewPosition = new Vector3(
                    transform.position.x + SpawnOffset.x,
                    transform.position.y + SpawnOffset.y + SpawnRadius * Mathf.Cos(T),
                    transform.position.z + SpawnOffset.z + SpawnRadius * Mathf.Sin(T)
                    );

                Object.transform.SetPositionAndRotation(transform.position + SpawnOffset + Offset, Object.transform.rotation);
                
                T += Mathf.Deg2Rad * Spacing;
            }
        }

        public void Generate()
        {
            // Get all spawn points
            var Clowns = GameObject.FindGameObjectsWithTag("Target");

            // Destroy them all
            foreach (var Clown in Clowns)
            {
                #if UNITY_EDITOR
				EditorApplication.delayCall += () =>
				{
					if (Application.isPlaying)
						Destroy(Clown);
					else
						DestroyImmediate(Clown);
				};
                #endif
            }

            // Spawn new objects
            Vector3 SpawnPoint = Vector3.zero;
            float AngleInDegrees = (360.0f / NumberOfObjects);
            float T = Mathf.Deg2Rad * AngleInDegrees;

            for (int i = 0; i < NumberOfObjects; i++)
            {
                SpawnPoint.x = transform.position.x + SpawnOffset.x;
                SpawnPoint.y = transform.position.y + SpawnRadius * Mathf.Cos(T) + SpawnOffset.y;
                SpawnPoint.z = transform.position.z + SpawnRadius * Mathf.Sin(T) + SpawnOffset.z;

                var Object = Instantiate(ObjectToSpawn, SpawnPoint, ObjectToSpawn.transform.rotation);
                Object.name = ObjectToSpawn.name + "_" + i;

                T += Mathf.Deg2Rad * AngleInDegrees;
            }

            TotalObjects = NumberOfObjects;
        }
    }
}