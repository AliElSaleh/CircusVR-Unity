using UnityEngine;
using JetBrains.Annotations;
using TMPro;

namespace Assets.Scripts.Duck
{
	public class DoublePointsDuck : Duck
	{
		[UsedImplicitly]
		private void OnCollisionEnter(Collision Collision)
		{
			switch (Collision.transform.gameObject.tag)
			{
				case "Target":
                    if (!gameObject)
                    {
                        ParticleHitPrefab = Instantiate(ParticleHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                        ParticleHitPrefab.GetComponent<ParticleSystem>().Play();
                    }

                    if (Timer.Finished)
						return;

					float Distance = Vector3.Distance(Collision.gameObject.transform.position, Collision.contacts[0].point);

                    if (Distance < 0.3f)
                    {
                        int ScoreToAdd = Collision.gameObject.GetComponent<Target>().Tier3 * 2;
                        ScoreManager.Add(ScoreToAdd);

                        // Spawn the score hit UI element (for feedback)
                        ScoreHitPrefab = Instantiate(ScoreHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                        TextComponent = ScoreHitPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                        TextComponent.color = Color.red;
                        TextComponent.fontSize = 38892.0f;
                        TextComponent.text = "+" + ScoreToAdd;
                        ScoreHitPrefab.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
                        ScoreHitPrefab.transform.rotation = Quaternion.LookRotation(Vector3.zero - ScoreHitPrefab.transform.position) * Quaternion.Inverse(new Quaternion(0.0f, 180.0f, 0.0f, 1.0f));
                    }
                    else if (Distance > 0.3f && Distance < 0.5f)
                    {
                        int ScoreToAdd = Collision.gameObject.GetComponent<Target>().Tier2 * 2;
                        ScoreManager.Add(ScoreToAdd);

                        // Spawn the score hit UI element (for feedback)
                        ScoreHitPrefab = Instantiate(ScoreHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                        TextComponent = ScoreHitPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                        TextComponent.color = Color.red;
                        TextComponent.fontSize = 38892.0f;
                        TextComponent.text = "+" + ScoreToAdd;
                        ScoreHitPrefab.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
                        ScoreHitPrefab.transform.rotation = Quaternion.LookRotation(Vector3.zero - ScoreHitPrefab.transform.position) * Quaternion.Inverse(new Quaternion(0.0f, 180.0f, 0.0f, 1.0f));
                    }
                    else
                    {
                        int ScoreToAdd = Collision.gameObject.GetComponent<Target>().Tier1 * 2;

                        ScoreManager.Add(ScoreToAdd);

                        // Spawn the score hit UI element (for feedback)
                        ScoreHitPrefab = Instantiate(ScoreHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                        TextComponent = ScoreHitPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                        TextComponent.color = Color.red;
                        TextComponent.fontSize = 38892.0f;
                        TextComponent.text = "+" + ScoreToAdd;
                        ScoreHitPrefab.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
                        ScoreHitPrefab.transform.rotation = Quaternion.LookRotation(Vector3.zero - ScoreHitPrefab.transform.position) * Quaternion.Inverse(new Quaternion(0.0f, 180.0f, 0.0f, 1.0f));
                    }

                    break;

                case "Water":
					Destroy(gameObject); // Destroy duck
				break;
			}
		}
	}
}
