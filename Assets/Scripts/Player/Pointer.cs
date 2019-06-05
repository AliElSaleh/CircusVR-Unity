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

        public Transform LineOrigin = null;
        public Transform GrabTransform = null;
        private GameObject CurrentObject = null;
        private GameObject CachedObject = null;

        private OVRInput.Controller Controller;

        private void Awake()
        {
            Events.OnControllerSource += UpdateOrigin;
            Events.OnTriggerDown += ProcessTriggerDown;
            Events.OnTriggerUp += ProcessTriggerUp;
        }

        private void OnDestroy()
        {
            Events.OnControllerSource -= UpdateOrigin;
            Events.OnTriggerDown -= ProcessTriggerDown;
            Events.OnTriggerUp -= ProcessTriggerUp;
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
            Vector3 EndLocation = LineOrigin.position + (LineOrigin.forward * Distance);

            // Check hit
            if (Hit.collider != null)
                EndLocation = Hit.point;

            // Set position
            LineRenderer.SetPosition(0, LineOrigin.position);
            LineRenderer.SetPosition(1, EndLocation);

            return EndLocation;
        }

        private RaycastHit CreateRaycast(int Layer)
        {
            RaycastHit Hit;
            Ray Ray = new Ray(LineOrigin.position, LineOrigin.forward);
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
            //CurrentOrigin = ControllerObject.transform;

            // Is the laser visible?
            if (Controller == OVRInput.Controller.Touchpad)
            {
                LineRenderer.enabled = false;
            }
            else
            {
                LineRenderer.enabled = true;
            }

            this.Controller = Controller;
        }

        private GameObject UpdatePointerStatus()
        {
            // Create the ray
            RaycastHit Hit = CreateRaycast(InteractableMask);

            // Check hit
            if (Hit.collider)
            {
                return Hit.collider.gameObject;
            }

            // Return nothing
            return null;
        }

        private void ProcessTriggerDown()
        {
            if (!CurrentObject)
                return;

            Interactable Interactable = CurrentObject.GetComponent<Interactable>();
            Interactable.Controller = Controller;
            Interactable.PickupLocation = GrabTransform.position;
            Interactable.Pressed(CurrentObject);
            CachedObject = CurrentObject;
        }

        private void ProcessTriggerUp()
        {
            if (!CachedObject)
                return;

            Interactable Interactable = CachedObject.GetComponent<Interactable>();
            Interactable.Released(CachedObject);
            CachedObject = null;
            CurrentObject = null;
        }
    }
}
