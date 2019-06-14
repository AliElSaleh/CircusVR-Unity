using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Input
{
    public class VRInputModule : BaseInputModule
    {
        public Camera Camera;
        public OVRInput.Controller TargetSource;
        public UnityAction<bool> ClickAction;

        private GameObject CurrentObject;
        private PointerEventData Data = null;

        protected override void Awake()
        {
            base.Awake();

            Data = new PointerEventData(eventSystem);
        }

        public override void Process()
        {
            // Reset data and set position
            Data.Reset();
            Data.position = new Vector2(Camera.pixelWidth / 2.0f, Camera.pixelHeight / 2.0f);

            // Raycast
            eventSystem.RaycastAll(Data, m_RaycastResultCache);
            Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            CurrentObject = Data.pointerCurrentRaycast.gameObject;

            // Clear the cache
            m_RaycastResultCache.Clear();

            // Hover
            HandlePointerExitAndEnter(Data, CurrentObject);

            // Press
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                ProcessPress(Data);

            // Release
            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
                ProcessRelease(Data);
        }

        public PointerEventData GetData()
        {
            return Data;
        }

        private void ProcessPress(PointerEventData InData)
        {

        }

        private void ProcessRelease(PointerEventData InData)
        {

        }
    }
}
