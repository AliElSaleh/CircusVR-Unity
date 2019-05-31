using UnityEditor;
using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts
{
#if UNITY_EDITOR
    [CustomEditor(typeof(TargetSpawner))][UsedImplicitly]
    public class TargetSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            TargetSpawner TargetSpawnerScript = (TargetSpawner)target;

            if (GUILayout.Button("Spawn " + TargetSpawnerScript.NumberOfObjects + " " + TargetSpawnerScript.ObjectToSpawn.name + "s"))
            {
                TargetSpawnerScript.Generate();
            }
        }
    }
#endif
}