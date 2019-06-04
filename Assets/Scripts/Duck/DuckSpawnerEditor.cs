using JetBrains.Annotations;
using UnityEngine;
using UnityEditor;

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

			if (GUILayout.Button("Spawn " + Spawner.NumberOfObjects + " " + Spawner.ObjectToSpawn.name + "s"))
			{
				Spawner.Generate();
			}
		}
	}
#endif
}
