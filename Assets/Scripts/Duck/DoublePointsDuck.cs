using UnityEngine;
using JetBrains.Annotations;

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
                    // Add particle

                    if (Timer.Finished)
						return;

					float Distance = Vector3.Distance(Collision.gameObject.transform.position, Collision.contacts[0].point);

					if (Distance < 0.3f)
					{
						ScoreManager.Add(Collision.gameObject.GetComponent<Target>().Tier3*2);
					}
					else if (Distance > 0.3f && Distance < 0.5f)
					{
						ScoreManager.Add(Collision.gameObject.GetComponent<Target>().Tier2*2);
					}
					else
					{
						ScoreManager.Add(Collision.gameObject.GetComponent<Target>().Tier1*2);
					}

                    Destroy(gameObject);
				break;

                case "Water":
					Destroy(gameObject); // Destroy duck
				break;
			}
		}
	}
}
