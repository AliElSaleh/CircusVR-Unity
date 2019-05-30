using UnityEditor;
using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    [CustomEditor(typeof(ClownSpawner))][UsedImplicitly]
    public class ClownSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var ClownSpawnerScript = (ClownSpawner)target;

            if (GUILayout.Button("Spawn " + ClownSpawnerScript.NumberOfObjects + " " + ClownSpawnerScript.ObjectToSpawn.name + "s"))
            {
                ClownSpawnerScript.Generate();
            }
        }
    }
}