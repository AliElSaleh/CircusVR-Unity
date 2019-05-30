using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class Target : MonoBehaviour
    {
        public float MoveSpeed = 5;

        private Vector3 Direction;
        private Vector3 Location;

        private void Awake()
        {
            tag = "Target";
        }

        [UsedImplicitly]
        private void Start()
        {
        }

        [UsedImplicitly]
        private void Update()
        {

        }
    }
}