using UnityEditor;
using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    [CustomEditor(typeof(TargetSpawner))][UsedImplicitly]
    public class TargetSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var ClownSpawnerScript = (TargetSpawner)target;

            if (GUILayout.Button("Spawn " + ClownSpawnerScript.NumberOfObjects + " " + ClownSpawnerScript.ObjectToSpawn.name + "s"))
            {
                ClownSpawnerScript.Generate();
            }
        }
    }
}