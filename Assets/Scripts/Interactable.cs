using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void Pressed()
    {
        MeshRenderer Renderer = GetComponent<MeshRenderer>();
        bool bFlip = !Renderer.enabled;

        Renderer.enabled = bFlip;
    }
}
