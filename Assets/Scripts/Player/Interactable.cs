﻿using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts.Player
{
    public class Interactable : MonoBehaviour
    {
        public AudioClip[] AudioClip = null;
        public AudioSource AudioSource = null;

		[UsedImplicitly]
        private void Start()
        {
	        if (AudioClip.Length > 0)
	        {
	            int i = Random.Range(0, AudioClip.Length);
	            AudioSource.clip = AudioClip[i];
	        }
        }

        public void Pressed(GameObject GameObject)
        {
            GameObject.GetComponent<Rigidbody>().isKinematic = true;
            GameObject.transform.position = Events.PickupLocation;
            GameObject.transform.parent = Events.Parent;

            AudioSource.Play();
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
