using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts
{
    public class Interactable : MonoBehaviour
    {
        public AudioClip[] audioClip;
        public AudioSource MusicSource;

        private void Start()
        {
            int i = Random.Range(0, audioClip.Length);
            MusicSource.clip = audioClip[i];
        }

        public void Pressed(GameObject GameObject)
        {
            GameObject.GetComponent<Rigidbody>().isKinematic = true;
            GameObject.transform.position = Events.PickupLocation;
            GameObject.transform.parent = Events.Parent;

            MusicSource.Play();
        }

        public void Released(GameObject GameObject)
        {
            GameObject.GetComponent<Rigidbody>().isKinematic = false;
            GameObject.transform.parent = null;

            Vector3 ThrowVector = (GameObject.transform.position - Events.PickupLocation);

            Vector3 Direction = Camera.main.transform.forward;

            GameObject.GetComponent<Rigidbody>().AddForce(ThrowVector + Direction * 15.0f, ForceMode.VelocityChange);
        }
    }
}
