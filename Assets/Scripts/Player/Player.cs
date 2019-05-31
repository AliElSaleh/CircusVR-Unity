using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [UsedImplicitly]
        private void Awake()
        {
            tag = "Player";
        }

        [UsedImplicitly]
	    private void Start()
        {
		    
	    }
	    
        [UsedImplicitly]
	    private void Update()
        {
            CheckForApplicationQuit();
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
