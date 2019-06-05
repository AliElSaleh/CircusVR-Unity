using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public Pointer Pointer;
    public SpriteRenderer CircleRenderer;

    public Sprite OpenSprite;
    public Sprite ClosedSprite;

    private Camera Camera = null;

    private void Awake()
    {
        Pointer.OnPointerUpdate += UpdateSprite;

        Camera = Camera.main;
    }

	// Update is called once per frame
	private void Update()
    {
	    transform.LookAt(Camera.gameObject.transform);	
	}

    private void OnDestroy()
    {
        Pointer.OnPointerUpdate -= UpdateSprite;
    }

    private void UpdateSprite(Vector3 Point, GameObject HitObject)
    {
        transform.position = Point;

        if (HitObject)
        {
            CircleRenderer.sprite = OpenSprite;
        }
        else
        {
            CircleRenderer.sprite = ClosedSprite;
        }
    }
}
