using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts.Duck
{
	public class SuperDuck : Duck
	{
		private const float DetectionRadius = 1.5f;

		[UsedImplicitly]
		private void OnCollisionEnter(Collision Collision)
		{
			switch (Collision.transform.gameObject.tag)
			{
				case "Target":
					if (Timer.Finished)
						return;

					// Sphere cast
					RaycastHit[] OutHit;
					OutHit = Physics.SphereCastAll(transform.position, DetectionRadius, transform.forward);

					// Destroy every target if the sphere cast has hit it
					foreach (RaycastHit Hit in OutHit)
					{
						ScoreManager.Add(Collision.gameObject.GetComponent<Target>().Tier2);

                        if (Hit.transform.gameObject.tag == "Target")
                        {
                            Hit.transform.GetComponent<Target>().Hit = true;
                            Hit.transform.GetComponent<Target>().Hide();
                        }
					}
                    break;

                case "Water":
					Destroy(gameObject); // Destroy duck
				break;
			}
		}

        [UsedImplicitly]
		private void OnDrawGizmos()
		{
			#if UNITY_EDITOR
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, DetectionRadius);
			#endif
		}
	}
}
