using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class ClownSpawner : MonoBehaviour
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

        private void Update()
        {
            #if UNITY_EDITOR
			if (Application.isEditor && !Application.isPlaying)
				UpdateObject();
            #endif
        }

        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
			// Draw the spawn radius
			Handles.DrawWireDisc(transform.position, transform.up, SpawnRadius);	

			Gizmos.color = Color.white;
			Gizmos.DrawSphere(transform.position, 0.1f);
            #endif
        }

        private void UpdateObject()
        {
            // Get all spawn points
            var Clowns = GameObject.FindGameObjectsWithTag("Clown");

            float AngleInDegrees = (360.0f / TotalObjects);
            float T = Mathf.Deg2Rad * AngleInDegrees;

            // Update them all
            foreach (GameObject Clown in Clowns)
            {
                Clown.transform.SetPositionAndRotation(new Vector3(
                    transform.position.x + SpawnRadius * Mathf.Cos(T) + SpawnOffset.x,
                    transform.position.y + SpawnOffset.y,
                    transform.position.z + SpawnRadius * Mathf.Sin(T) + SpawnOffset.z),
                    Quaternion.identity);

                T += Mathf.Deg2Rad * AngleInDegrees;
            }
        }

        public void Generate()
        {
            // Get all spawn points
            var Clowns = GameObject.FindGameObjectsWithTag("Clown");

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
                SpawnPoint.x = transform.position.x + SpawnRadius * Mathf.Cos(T) + SpawnOffset.x;
                SpawnPoint.y = transform.position.y + SpawnOffset.y;
                SpawnPoint.z = transform.position.z + SpawnRadius * Mathf.Sin(T) + SpawnOffset.z;

                GameObject Clown = Instantiate(ObjectToSpawn, SpawnPoint, Quaternion.identity);
                Clown.name = "Clown_" + i;

                T += Mathf.Deg2Rad * AngleInDegrees;
            }

            TotalObjects = NumberOfObjects;
        }
    }
}