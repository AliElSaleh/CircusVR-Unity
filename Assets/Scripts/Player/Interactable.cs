using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts.Player
{
    public class Interactable : MonoBehaviour
    {
        public AudioClip[] AudioClip = null;
        public AudioSource AudioSource;

		[UsedImplicitly]
        private void Start()
        {
            AudioSource = GetComponent<AudioSource>();

	        if (AudioClip.Length > 0)
	        {
	            int i = Random.Range(0, AudioClip.Length);
	            AudioSource.clip = AudioClip[i];
	        }

            //AudioSource[2].clip = AirbornDuck;
        }

        public void Pressed(GameObject GameObject)
        {
            GameObject.GetComponent<Rigidbody>().isKinematic = true;
            GameObject.transform.position = Events.PickupLocation;
            GameObject.transform.parent = Events.Parent;

            ScoreManager.SetText("Pressed");

            AudioSource.Play();
        }

        public void Released(GameObject GameObject)
        {
            //AudioSource.Play();
            ScoreManager.SetText("Released");

            GameObject.GetComponent<Rigidbody>().isKinematic = false;
            GameObject.transform.parent = null;

            Vector3 ThrowVector = (GameObject.transform.position - Events.PickupLocation);

            Vector3 Direction = Camera.main.transform.forward;

            GameObject.GetComponent<Rigidbody>().AddForce(ThrowVector + Direction * 15.0f, ForceMode.VelocityChange);
        }
    }
}
