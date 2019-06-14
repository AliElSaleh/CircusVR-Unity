using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts.Duck
{
	public class Duck : MonoBehaviour
	{
		protected Rigidbody Rigidbody;

        [Range(1, 200)]
		public float Force = 30.0f;

        [UsedImplicitly]
		protected void Start()
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

					//Destroy(Collision.gameObject); // Destroy target
				break;

				case "Wall":
					if (Timer.Finished)
						return;

					ScoreManager.Subtract(20);

					Destroy(gameObject); // Destroy duck
				break;

				case "Water":
					Destroy(gameObject); // Destroy duck
				break;
	        }
        }
	}
}
