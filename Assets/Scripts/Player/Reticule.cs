using UnityEngine;
using Assets.Scripts.Player;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class Reticule : MonoBehaviour
    {
        public Pointer Pointer;

        [SerializeField]
        public static SpriteRenderer CircleRenderer;

        public Sprite OpenSprite;
        public Sprite ClosedSprite;

        private Camera Camera;

        [UsedImplicitly]
        private void Awake()
        {
            Pointer.OnPointerUpdate += UpdateSprite;

            Camera = Camera.main;
        }

        [UsedImplicitly]
        private void Start()
        {
            CircleRenderer = GetComponent<SpriteRenderer>();
        }

        [UsedImplicitly]
        private void Update()
        {
            transform.LookAt(Camera.gameObject.transform);	
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            Pointer.OnPointerUpdate -= UpdateSprite;
        }

        private void UpdateSprite(Vector3 Point, GameObject HitObject)
        {
            transform.position = Point;

            // Which sprite to display?
            CircleRenderer.sprite = HitObject ? OpenSprite : ClosedSprite;
        }
    }
}
