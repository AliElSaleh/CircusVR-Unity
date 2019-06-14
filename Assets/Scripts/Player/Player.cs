using Assets.Scripts.Managers;
using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public static GameObject Grabber;

        public Pointer Pointer = null;

        [UsedImplicitly]
        private void Start()
        {
            Grabber = GameObject.Find("Grabber");
        }

        [UsedImplicitly]
        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                if (!LevelManager.IsPaused)
                {
                    Pointer.InteractableMask = LayerMask.NameToLayer("UI");
                    LevelManager.Pause();
                }
                else
                {
                    Pointer.InteractableMask = LayerMask.NameToLayer("Interactable");
                    LevelManager.Resume();
                }
            }
        }

        [UsedImplicitly]
        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.3f);
            #endif
        }
    }
}
