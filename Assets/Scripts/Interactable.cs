using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Vector3 PickupLocation;

    public void Pressed(GameObject GameObject)
    {
        GameObject.transform.position = PickupLocation;

        //MeshRenderer Renderer = GetComponent<MeshRenderer>();
        //bool bFlip = !Renderer.enabled;
        //
        //Renderer.enabled = bFlip;
    }
}
