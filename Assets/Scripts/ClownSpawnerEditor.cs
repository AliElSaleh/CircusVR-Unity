using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    [CustomEditor(typeof(ClownSpawner))]
    public class ClownSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ClownSpawner ClownSpawnerScript = (ClownSpawner)target;

            if (GUILayout.Button("Spawn " + ClownSpawnerScript.NumberOfObjects + " " + ClownSpawnerScript.ObjectToSpawn.name + "s"))
            {
                ClownSpawnerScript.Generate();
            }
        }
    }
}