using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public static GameObject Grabber;

        [UsedImplicitly]
        private void Start()
        {
            Grabber = GameObject.Find("Grabber");
        }

        [UsedImplicitly]
	    private void Update()
        {
            CheckForApplicationQuit();
        }

        [UsedImplicitly]
        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.3f);
            #endif
        }

        private static void CheckForApplicationQuit()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                #if UNITY_EDITOR
                    if (EditorApplication.isPlaying)
                        EditorApplication.isPlaying = false;
                #else

                Application.Quit();

                #endif
            }
        }
    }
}
