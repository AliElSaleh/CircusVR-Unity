using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Duck
{
	public class Duck : MonoBehaviour
	{
		private Rigidbody Rigidbody;

		[HideInInspector]
		public float Force = 300.0f;

        [HideInInspector]
        public Vector3 Direction = new Vector3();

		[UsedImplicitly]
		private void Start()
		{
			Rigidbody = GetComponent<Rigidbody>();

			Rigidbody.AddForce(Vector3.up * Force);
		}

		[UsedImplicitly]
		private void Update()
		{
		
		}

		public void SetPhysicalMaterial(PhysicMaterial NewPhysicMaterial)
		{
			GetComponent<SphereCollider>().material = NewPhysicMaterial;
		}

        public void Throw()
        {
			Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.AddForce(Direction * Force);
        }

        private void OnCollisionEnter(Collision Collision)
        {
            if (Collision.transform.gameObject.tag == "Target")
            {
                Destroy(Collision.gameObject);
                Destroy(gameObject);
            }
        }
	}
}
