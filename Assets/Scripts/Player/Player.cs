using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public Duck.Duck DuckToSpawn= null;

        public GameObject SpawnLocation = null;

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
            //if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            //{
            //    var Duck = Instantiate(DuckToSpawn, SpawnLocation.transform.position, Quaternion.identity);
            //    Duck.Direction = Camera.main.transform.forward;
            //    Duck.Force = 450.0f;
            //    Duck.Throw();
            //}

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
