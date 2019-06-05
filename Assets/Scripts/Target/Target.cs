using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class Target : MonoBehaviour
    {
	    [UsedImplicitly]
	    private void Start()
	    {
	    }

        [UsedImplicitly]
        private void Update()
        {
        }

        private void OnCollisionEnter(Collision Collision)
        {
            Destroy(this);
        }
    }
}