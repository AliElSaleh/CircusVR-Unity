using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts.Duck
{
	public class Duck : MonoBehaviour
	{
		private Rigidbody Rigidbody;

		[HideInInspector]
		public float Force = 300.0f;

        [UsedImplicitly]
		private void Start()
		{
			Rigidbody = GetComponent<Rigidbody>();

			Rigidbody.AddForce(Vector3.up * Force);
		}

        public void SetPhysicalMaterial(PhysicMaterial NewPhysicMaterial)
		{
			GetComponent<SphereCollider>().material = NewPhysicMaterial;
		}

        [UsedImplicitly]
        private void OnCollisionEnter(Collision Collision)
        {
            if (Collision.transform.gameObject.tag == "Target")
            {
                Score.Add(20);

                Destroy(Collision.gameObject); // Destroy target
                Destroy(gameObject); // Destroy duck
            }
            else
            {
                Score.Subtract(20);
            }
        }
	}
}
