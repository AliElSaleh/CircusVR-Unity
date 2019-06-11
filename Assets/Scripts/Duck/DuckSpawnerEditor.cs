using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

namespace Assets.Scripts.Duck
{
#if UNITY_EDITOR
	[CustomEditor(typeof(DuckSpawner))][UsedImplicitly]
	public class DuckSpawnerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			DuckSpawner Spawner = (DuckSpawner)target;

			if (GUILayout.Button("Spawn " + (Spawner.NumberOfDucks + Spawner.NumberOfSuperDucks) + " " + Spawner.DuckToSpawn.name + "s"))
			{
				Spawner.Generate();
			}
		}
	}
#endif
}
