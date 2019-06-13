using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class TargetSpawner : MonoBehaviour
    {
        [Range(1.0f, 20.0f)]
        public float Radius = 1.0f;

        [Range(2, 50)]
        public int NumberOfObjects = 2;

        [Range(30.0f, 100.0f)]
        public float RotationSpeed = 30.0f;

        public bool Clockwise = true;

        public Target TargetPrefab = null;

        public Transform OriginalTransform;
        private List<GameObject> ChildObjects = new List<GameObject>();
        private float Angle;
        public bool bGenerating;

        [UsedImplicitly]
        private void Start()
        {
            OriginalTransform = transform;

            ChildObjects = GetAllChildObjects();
        }

        [UsedImplicitly]
        private void Update()
        {
            if (bGenerating)
                return;

            Rotate();

			if (GetAllChildObjects().Count == 0)
				Destroy(gameObject);
        }

        [UsedImplicitly]
        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
			// Draw the spawn radius
			Handles.DrawWireDisc(transform.position, transform.right, Radius);	

			Gizmos.color = Color.white;
			Gizmos.DrawSphere(transform.position, 0.1f);
            #endif
        }

        public void Generate()
        {
            bGenerating = true;

	        //ChildObjects = GetAllChildObjects();
            
	        DestroyAllChildren();

            // Spawn new targets in a shape of a circle
            Vector3 SpawnPoint = Vector3.zero;
            float Spacing = (360.0f / NumberOfObjects);

            for (int i = 0; i < NumberOfObjects; i++)
            {
				// The Point on circumference of circle
	            SpawnPoint.x = transform.position.x;
	            SpawnPoint.y = transform.position.y + Radius * Mathf.Cos(Angle*Mathf.Deg2Rad);
	            SpawnPoint.z = transform.position.z + Radius * Mathf.Sin(Angle*Mathf.Deg2Rad);

				// Spawn the target on the spawn point and initialize its variables
	            Target Target = Instantiate(TargetPrefab, SpawnPoint, TargetPrefab.transform.rotation);
                Target.transform.parent = transform;
	            Target.name = TargetPrefab.name + "_" + i;

                // Increase the angle for next iteration
	            Angle += Spacing;
            }

            bGenerating = false;
        }

        private void Rotate()
        {
           if (Clockwise)
               Angle += RotationSpeed * Time.deltaTime;
           else
               Angle -= RotationSpeed * Time.deltaTime;
           
           transform.Rotate(Vector3.right, Angle);
           
           Angle = 0.0f;
        }

        public void ResetTargets()
        {
            Generate();
        }

        private List<GameObject> GetAllChildObjects()
        {
	        List<GameObject> Children = new List<GameObject>();

	        for (int i = 0; i < transform.childCount; i++)
		        Children.Add(transform.GetChild(i).gameObject);

	        return Children;
        }

        public void DestroyAllChildren()
        {
            ChildObjects = GetAllChildObjects();

            #if UNITY_EDITOR
            foreach (GameObject Child in ChildObjects)
		        DestroyImmediate(Child.gameObject);
            #else
            
            foreach (GameObject Child in ChildObjects)
		        Destroy(Child.gameObject);

            #endif

        }
    }
}