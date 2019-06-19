using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts.Duck
{
	public class Duck : MonoBehaviour
	{
		protected Rigidbody Rigidbody;

        [Range(1, 1000)] [HideInInspector]
		public float LaunchForce = 300.0f;

        [Range(1, 200)] [HideInInspector]
        public float ThrowForce = 30.0f;

        [UsedImplicitly]
		protected void Start()
		{
			Rigidbody = GetComponent<Rigidbody>();

			Rigidbody.AddForce(Vector3.up * LaunchForce);
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
			        if (Timer.Finished)
				        return;

			        float Distance = Vector3.Distance(Collision.gameObject.transform.position, Collision.contacts[0].point);

			        if (Distance < 0.3f)
			        {
						ScoreManager.Add(Collision.gameObject.GetComponent<Target>().Tier3);
			        }
					else if (Distance > 0.3f && Distance < 0.5f)
			        {
				        ScoreManager.Add(Collision.gameObject.GetComponent<Target>().Tier2);
			        }
			        else
			        {
				        ScoreManager.Add(Collision.gameObject.GetComponent<Target>().Tier1);
			        }

					//Collision.gameObject.SetActive(false); // Destroy target
				break;

                case "Water":
					Destroy(gameObject); // Destroy duck
				break;
	        }
        }
	}
}
