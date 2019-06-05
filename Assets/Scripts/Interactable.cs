using Assets.Scripts.Player;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public static OVRInput.Controller Controller;

    public Vector3 PickupLocation;

    public void Pressed(GameObject GameObject)
    {
        GameObject.GetComponent<Rigidbody>().isKinematic = true;
        GameObject.transform.position = PickupLocation;
        GameObject.transform.parent = Events.Parent;

        //MeshRenderer Renderer = GetComponent<MeshRenderer>();
        //bool bFlip = !Renderer.enabled;
        //
        //Renderer.enabled = bFlip;
    }

    public void Released(GameObject GameObject)
    {
        GameObject.GetComponent<Rigidbody>().isKinematic = false;
        GameObject.transform.parent = null;

        Vector3 ThrowVector = GameObject.transform.position - Events.PickupLocation;
        GameObject.GetComponent<Rigidbody>().AddForce(ThrowVector + Camera.main.transform.forward * 10.0f, ForceMode.VelocityChange);
    }
}
