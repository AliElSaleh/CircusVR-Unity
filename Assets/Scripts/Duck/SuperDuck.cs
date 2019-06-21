using UnityEngine;
using JetBrains.Annotations;
using TMPro;

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
                    ParticleHitPrefab = Instantiate(ParticleHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                    ParticleHitPrefab.GetComponent<ParticleSystem>().Play();

                    if (Timer.Finished)
						return;

					// Sphere cast
					RaycastHit[] OutHit;
					OutHit = Physics.SphereCastAll(transform.position, DetectionRadius, transform.forward);

                    // Spawn the score hit UI element (for feedback)
                    int ScoreToAdd = Collision.gameObject.GetComponent<Target>().Tier2;
                    ScoreManager.Add(ScoreToAdd);

                    ScoreHitPrefab = Instantiate(ScoreHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                    TextComponent.color = Color.blue;
                    TextComponent.fontSize = 38892.0f;
                    TextComponent.text = "+" + ScoreToAdd;
                    ScoreHitPrefab.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
                    ScoreHitPrefab.transform.rotation = Quaternion.LookRotation(Vector3.zero - ScoreHitPrefab.transform.position) * Quaternion.Inverse(new Quaternion(0.0f, 180.0f, 0.0f, 1.0f));

					// Destroy every target in the sphere's reach
					foreach (RaycastHit Hit in OutHit)
                    {
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
