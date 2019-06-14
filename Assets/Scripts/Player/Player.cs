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
        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.3f);
            #endif
        }
    }
}
