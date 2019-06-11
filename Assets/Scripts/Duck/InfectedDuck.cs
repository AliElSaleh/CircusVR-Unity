using Assets.Scripts.Player;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Duck
{
	public class InfectedDuck : Duck
	{
		[Range(10, 100)]
		public int ScoreLoss = 100;

		private float Timer;

		[UsedImplicitly]
		private void Update()
		{
			if (transform.parent == Events.Parent)
			{
				Timer += Time.deltaTime;

				if (Timer > 1.0f)
				{
					ScoreManager.Subtract(ScoreLoss);

					Destroy(gameObject);
				}
			}
		}
	}
}
