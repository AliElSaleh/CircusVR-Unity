using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class Target : MonoBehaviour
    {
        [UsedImplicitly]
        private void Awake()
        {
            tag = "Target";
        }

        [UsedImplicitly]
        private void Update()
        {
        }
    }
}