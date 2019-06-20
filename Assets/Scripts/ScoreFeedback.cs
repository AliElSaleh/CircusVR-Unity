using UnityEngine;
using JetBrains.Annotations;

public class ScoreFeedback : MonoBehaviour
{
    private float TimerCounter;

    [UsedImplicitly]
	private void Update()
    {
        TimerCounter += Time.deltaTime;

        if (TimerCounter > 2.0f)
        {
            TimerCounter = 0.0f;
            Destroy(gameObject);
        }
    }
}
