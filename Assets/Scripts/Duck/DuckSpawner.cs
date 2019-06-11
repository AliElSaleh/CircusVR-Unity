using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

namespace Assets.Scripts.Duck
{
	public enum DuckType
	{
		Duck, Super, DoublePoints, Infected
	}

	public class DuckSpawner : MonoBehaviour
	{
		[Range(1.0f, 20.0f)]
		public float Radius = 1.0f;

		[Range(10, 500)]
		public float ForceMin = 300.0f;

		[Range(500, 1000)]
		public float ForceMax = 500.0f;

		[Range(0.1f, 10.0f)]
		public float SpawnDelay = 1.0f;

		public PhysicMaterial PhysicsMaterial = null;

		public Duck DuckPrefab = null;
		public SuperDuck SuperDuckPrefab = null;
		public DoublePointsDuck DPDuckPrefab = null;
		public InfectedDuck InfectedDuckPrefab = null;

		private int DucksSpawned;
		private int DPDucksSpawned;
		private int InfectedDuckID;

		private float TimeInSeconds;

		[UsedImplicitly]
		private void Start()
		{
			InfectedDuckID = Random.Range(0, 10);
		}

		[UsedImplicitly]
		private void Update()
		{
			TimeInSeconds += Time.deltaTime;

			if (TimeInSeconds > SpawnDelay)
			{
				// Spawn double points
				if (DucksSpawned > Random.Range(10, 15))
				{
					SpawnDuck(DuckType.DoublePoints);
					DucksSpawned = 0;
				}
				// Spawn super duck
				else if (DPDucksSpawned > Random.Range(2, 4))
				{
					SpawnDuck(DuckType.Super);
					DPDucksSpawned = 0;
				}
				// Spawn normal duck
				else
				{
					SpawnDuck(DuckType.Duck);

					if (Random.Range(0, 10) == InfectedDuckID)
						SpawnDuck(DuckType.Infected);
				}

				TimeInSeconds = 0.0f;
			}
		}

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

		private void SpawnDuck(DuckType Type)
		{
			Vector3 SpawnPoint = Vector3.zero;
			float Angle = Random.Range(0.0f, 360.0f);

			// The point on circumference of circle
			SpawnPoint.x = transform.position.x + Radius * Mathf.Cos(Angle*Mathf.Deg2Rad);
			SpawnPoint.y = transform.position.y;
			SpawnPoint.z = transform.position.z + Radius * Mathf.Sin(Angle*Mathf.Deg2Rad);

			switch (Type)
			{
				case DuckType.Duck:
				{
					// Spawn the duck on the spawn point and initialize its variables
					Duck Object = Instantiate(DuckPrefab, SpawnPoint, DuckPrefab.transform.rotation);
					Object.transform.parent = gameObject.transform;
					Object.name = DuckPrefab.name;
					Object.Force = Random.Range(ForceMin, ForceMax);
					Object.SetPhysicalMaterial(PhysicsMaterial);

					DucksSpawned++;
				}
				break;

				case DuckType.Super:
				{
					// Spawn the super duck on the spawn point and initialize its variables
					SuperDuck Object = Instantiate(SuperDuckPrefab, SpawnPoint, SuperDuckPrefab.transform.rotation);
					Object.transform.parent = gameObject.transform;
					Object.name = SuperDuckPrefab.name;
					Object.Force = Random.Range(ForceMin, ForceMax);
					Object.SetPhysicalMaterial(PhysicsMaterial);
				}
				break;

				case DuckType.DoublePoints:
				{
					// Spawn the double points duck on the spawn point and initialize its variables
					DoublePointsDuck Object = Instantiate(DPDuckPrefab, SpawnPoint, DPDuckPrefab.transform.rotation);
					Object.transform.parent = gameObject.transform;
					Object.name = DPDuckPrefab.name;
					Object.Force = Random.Range(ForceMin, ForceMax);
					Object.SetPhysicalMaterial(PhysicsMaterial);

					DPDucksSpawned++;
				}
				break;

				case DuckType.Infected:
				{
					// Spawn the double points duck on the spawn point and initialize its variables
					InfectedDuck Object = Instantiate(InfectedDuckPrefab, SpawnPoint, InfectedDuckPrefab.transform.rotation);
					Object.transform.parent = gameObject.transform;
					Object.name = InfectedDuckPrefab.name;
					Object.Force = Random.Range(ForceMin, ForceMax);
					Object.SetPhysicalMaterial(PhysicsMaterial);
				}
				break;
			}			
		}
	}
}
