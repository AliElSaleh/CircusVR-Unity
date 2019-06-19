using UnityEngine;
using Assets.Scripts.Player;
using JetBrains.Annotations;

namespace Assets.Scripts.Duck
{
	public class InfectedDuck : Duck
	{
		[Range(10, 100)]
		public int ScoreLoss = 100;
        public ParticleSystem effect;
        public GameObject ParticlePrefab;

		private float DestroyDelay;

        [UsedImplicitly]
		private void Update()
		{
			if (Timer.Finished)
				return;

			if (transform.parent == Events.Parent)
			{
                Events.DisableInput();
                Pointer.HideLine();
                Pointer.HideReticule();

                DestroyDelay += Time.deltaTime;

				if (DestroyDelay > 1.0f)
				{
                    if (ScoreManager.TextMeshProUGUIComponent)
					    ScoreManager.Subtract(ScoreLoss);

                    Instantiate(ParticlePrefab, transform);
                    ParticlePrefab.GetComponent<ParticleSystem>().Play();

                    Pointer.bHeld = false;
                    Events.EnableInput();
                    Pointer.ShowLine();
                    Pointer.ShowReticule();

                    Destroy(gameObject);
				}
			}
		}
	}
}
