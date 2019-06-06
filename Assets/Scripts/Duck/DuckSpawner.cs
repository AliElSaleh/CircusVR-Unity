﻿using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

namespace Assets.Scripts.Duck
{
	public class DuckSpawner : MonoBehaviour
	{
		[Range(1.0f, 20.0f)]
		public float Radius = 1.0f;

		[Range(2, 50)]
		public int NumberOfObjects = 2;

		[Range(10, 500)]
		public float ForceMin = 300.0f;

		[Range(500, 1000)]
		public float ForceMax = 500.0f;

		public PhysicMaterial PhysicMaterial = null;

		public Duck ObjectToSpawn = null;

		private List<GameObject> ChildObjects = new List<GameObject>();

        [UsedImplicitly]
		private void OnDrawGizmos()
		{
			#if UNITY_EDITOR
			// Draw the radius
			Handles.DrawWireDisc(transform.position, transform.up, Radius);	

			Gizmos.color = Color.white;
			Gizmos.DrawSphere(transform.position, 0.1f);
			#endif
		}

		public void Generate()
		{
			ChildObjects = GetAllChildObjects();

			DestroyAllChildren();

			// Spawn new targets in a shape of a circle
			Vector3 SpawnPoint = Vector3.zero;
			float Spacing = (360.0f / NumberOfObjects);
			float Angle = 0.0f;
			
			for (int i = 0; i < NumberOfObjects; i++)
			{
				// The Point on circumference of circle
				SpawnPoint.x = transform.position.x + Radius * Mathf.Cos(Angle*Mathf.Deg2Rad);
				SpawnPoint.y = transform.position.y;
				SpawnPoint.z = transform.position.z + Radius * Mathf.Sin(Angle*Mathf.Deg2Rad);
			
				// Spawn the target on the spawn point and initialize its variables
				Duck Object = Instantiate(ObjectToSpawn, SpawnPoint, ObjectToSpawn.transform.rotation);
				Object.transform.parent = gameObject.transform;
				Object.name = ObjectToSpawn.name + "_" + i;
				Object.Force = Random.Range(ForceMin, ForceMax);
				Object.SetPhysicalMaterial(PhysicMaterial);
			
				// Increase the angle for next iteration
				Angle += Spacing;
			}
		}

		private List<GameObject> GetAllChildObjects()
		{
			List<GameObject> Children = new List<GameObject>();

			for (int i = 0; i < transform.childCount; i++)
				Children.Add(transform.GetChild(i).gameObject);

			return Children;
		}

		private void DestroyAllChildren()
		{
			foreach (GameObject Child in ChildObjects)
				DestroyImmediate(Child.gameObject);
		}
	}
}