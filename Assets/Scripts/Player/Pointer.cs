using Assets.Scripts.Input;
using UnityEngine;
using UnityEngine.Events;
using JetBrains.Annotations;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Player
{
    [UsedImplicitly]
    public class Pointer : MonoBehaviour
    {
        public float Distance = 10.0f;
        public static LineRenderer LineRenderer;
        public VRInputModule InputModule;
        public LayerMask EverythingLayerMask;
        public LayerMask InteractableMask = 0;
        public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

        public Transform LineOrigin = null;
        public Transform GrabTransform = null;

        public static Vector3 LineStart;
        public static Vector3 LineEnd;

        private GameObject CurrentObject;
        private GameObject CachedObject;

        private OVRInput.Controller Controller;

        public static bool bHeld;

        [UsedImplicitly]
        private void Awake()
        {
            Events.OnControllerSource += UpdateOrigin;
            Events.OnTriggerDown += ProcessTriggerDown;
            Events.OnTriggerUp += ProcessTriggerUp;
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            Events.OnControllerSource -= UpdateOrigin;
            Events.OnTriggerDown -= ProcessTriggerDown;
            Events.OnTriggerUp -= ProcessTriggerUp;
        }

        [UsedImplicitly]
        private void Start()
        {
            LineRenderer = transform.GetChild(0).GetComponent<LineRenderer>();

            SetLineColor();
        }

        [UsedImplicitly]
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
            LineEnd = LineOrigin.position + (LineOrigin.forward * Distance);

            // Check hit
            if (Hit.collider != null)
                LineEnd = Hit.point;

            // Set position
            LineRenderer.SetPosition(0, LineOrigin.position);
            LineRenderer.SetPosition(1, LineEnd);

            return LineEnd;
        }

        private RaycastHit CreateRaycast(int Layer)
        {
            PointerEventData Data = InputModule.GetData();

            float Length = Data.pointerCurrentRaycast.distance == 0.0f ? Distance : Data.pointerCurrentRaycast.distance;

            RaycastHit Hit;
            Ray Ray = new Ray(LineOrigin.position, LineOrigin.forward);
            Physics.Raycast(Ray, out Hit, Length, Layer);

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

        private void UpdateOrigin(OVRInput.Controller InController, GameObject ControllerObject)
        {
            LineStart = LineOrigin.position;

            // Is the laser visible?
            LineRenderer.enabled = Controller != OVRInput.Controller.Touchpad;

            Controller = InController;
        }

        private GameObject UpdatePointerStatus()
        {
            // Create the ray
            RaycastHit Hit = CreateRaycast(InteractableMask);

            // Return the hit result
            return Hit.collider ? Hit.collider.gameObject : null;
        }

        private void ProcessTriggerDown()
        {
            if (!CurrentObject)
                return;

            Interactable Interactable = CurrentObject.GetComponent<Interactable>();
            Interactable.Pressed(CurrentObject);
            CachedObject = CurrentObject;
            bHeld = true;

            HideReticule();
            HideLine();
        }

        private void ProcessTriggerUp()
        {
            if (!CachedObject)
                return;

            Interactable Interactable = CachedObject.GetComponent<Interactable>();
            Interactable.Released(CachedObject);
            CachedObject = null;
            CurrentObject = null;

            bHeld = false;

            ShowReticule();
            ShowLine();
        }

        public static void HideLine()
        {
            LineRenderer.enabled = false;
        }

        public static void ShowLine()
        {
            if (!bHeld)
                LineRenderer.enabled = true;
        }

        public static void HideReticule()
        {
            Reticule.CircleRenderer.enabled = false;
        }

        public static void ShowReticule()
        {
            if (!bHeld)
                Reticule.CircleRenderer.enabled = true;
        }
    }
}
