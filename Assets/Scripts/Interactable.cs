using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts
{
    public class Interactable : MonoBehaviour
    {
        public Vector3 PickupLocation;

        public void Pressed(GameObject GameObject)
        {
            GameObject.GetComponent<Rigidbody>().isKinematic = true;
            GameObject.transform.position = Events.PickupLocation;
            GameObject.transform.parent = Events.Parent;
        }

        public void Released(GameObject GameObject)
        {
            GameObject.GetComponent<Rigidbody>().isKinematic = false;
            GameObject.transform.parent = null;

            Vector3 ThrowVector = (GameObject.transform.position - Events.PickupLocation);

            GameObject.GetComponent<Rigidbody>().AddForce(ThrowVector + Camera.main.transform.forward * 10.0f, ForceMode.VelocityChange);
        }
    }
}
