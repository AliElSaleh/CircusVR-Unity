using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    [UsedImplicitly]
    public class Pointer : MonoBehaviour
    {
        public float Distance = 10.0f;
        public LineRenderer LineRenderer = null;
        public LayerMask EverythingLayerMask;
        public LayerMask InteractableMask = 0;
        public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

        private Transform CurrentOrigin = null;
        private GameObject CurrentObject = null;

        private void Awake()
        {
            Events.OnControllerSource += UpdateOrigin;
            Events.OnTouchpadDown += ProcessTouchpadDown;
        }

        private void OnDestroy()
        {
            Events.OnControllerSource -= UpdateOrigin;
            Events.OnTouchpadDown -= ProcessTouchpadDown;
        }

        private void Start()
        {
            SetLineColor();
        }

        private void Update()
        {
            Vector3 HitPoint = UpdateLine();

            CurrentObject = UpdatePointerStatus();
            if (OnPointerUpdate != null)
                OnPointerUpdate(HitPoint, CurrentObject);
        }

        private Vector3 UpdateLine()
        {
            // Create ray
            RaycastHit Hit = CreateRaycast(EverythingLayerMask);

            // Line end
            Vector3 EndLocation = CurrentOrigin.position + (CurrentOrigin.forward * Distance);

            // Check hit
            if (Hit.collider != null)
                EndLocation = Hit.point;

            // Set position
            LineRenderer.SetPosition(0, CurrentOrigin.position);
            LineRenderer.SetPosition(1, EndLocation);

            return EndLocation;
        }

        private RaycastHit CreateRaycast(int Layer)
        {
            RaycastHit Hit;
            Ray Ray = new Ray(CurrentOrigin.position, CurrentOrigin.forward);
            Physics.Raycast(Ray, out Hit, Distance, Layer);

            return Hit;
        }

        private void SetLineColor()
        {
            if (!LineRenderer)
                return;

            Color EndColor = Color.white;
            EndColor.a = 0.0f;

            LineRenderer.endColor = EndColor;
        }

        private void UpdateOrigin(OVRInput.Controller Controller, GameObject ControllerObject)
        {
            // Set origin of pointer
            CurrentOrigin = ControllerObject.transform;

            // Is the laser visible?
            if (Controller == OVRInput.Controller.Touchpad)
            {
                LineRenderer.enabled = false;
            }
            else
            {
                LineRenderer.enabled = true;
            }
        }

        private GameObject UpdatePointerStatus()
        {
            // Create the ray
            RaycastHit Hit = CreateRaycast(InteractableMask);

            // Check hit
            if (Hit.collider)
                return Hit.collider.gameObject;

            // Return the game object
            return null;
        }

        private void ProcessTouchpadDown()
        {
            if (!CurrentObject)
                return;

            Interactable Interactable = CurrentObject.GetComponent<Interactable>();
            Interactable.Pressed();
        }
    }
}
