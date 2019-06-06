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
                ScoreManager.Add(Collision.gameObject.GetComponent<Target>().Reward);

                Destroy(Collision.gameObject); // Destroy target
                Destroy(gameObject); // Destroy duck
            }
            else if (Collision.transform.gameObject.tag == "Wall")
            {
                ScoreManager.Subtract(20);

                Destroy(gameObject); // Destroy duck
            }
        }
	}
}
