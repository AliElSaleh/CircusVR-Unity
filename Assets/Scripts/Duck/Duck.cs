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
		private void OnEnable()
		{
			Rigidbody = GetComponent<Rigidbody>();

			Rigidbody.AddForce(Vector3.up * Force);
		}

        [UsedImplicitly]
		private void Start()
		{
			gameObject.SetActive(false);
		}

        public void SetPhysicalMaterial(PhysicMaterial NewPhysicMaterial)
		{
			GetComponent<SphereCollider>().material = NewPhysicMaterial;
		}

        [UsedImplicitly]
        private void OnCollisionEnter(Collision Collision)
        {
	        switch (Collision.transform.gameObject.tag)
	        {
		        case "Target":
			        ScoreManager.Add(Collision.gameObject.GetComponent<Target>().Reward);

			        Destroy(Collision.gameObject); // Destroy target
			        Destroy(gameObject); // Destroy duck
				break;

				case "Wall":
					ScoreManager.Subtract(20);

					Destroy(gameObject); // Destroy duck
				break;

				case "Water":
					Destroy(gameObject); // Destroy duck
				break;

				default:
					Debug.Log("Hit " + Collision.transform.gameObject.tag);
				break;
	        }
        }
	}
}
