using Assets.Scripts.Player;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Duck
{
	public class InfectedDuck : Duck
	{
		[Range(10, 100)]
		public int ScoreLoss = 100;

		private float DestroyDelay;

		[UsedImplicitly]
		private void Update()
		{
			if (Timer.Finished)
				return;

			if (transform.parent == Events.Parent)
			{
				Events.DisableInput();

				DestroyDelay += Time.deltaTime;

				if (DestroyDelay > 1.0f)
				{
					ScoreManager.Subtract(ScoreLoss);

					Events.EnableInput();

					Destroy(gameObject);
				}
			}
		}
	}
}
