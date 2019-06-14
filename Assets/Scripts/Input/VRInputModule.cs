using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Input
{
    public class VRInputModule : BaseInputModule
    {
        public Camera Camera;

        private GameObject CurrentObject;
        protected PointerEventData Data;

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
        }

        public PointerEventData GetData()
        {
            return Data;
        }

        protected void ProcessPress(PointerEventData InData)
        {
            // Set raycast
            Data.pointerPressRaycast = Data.pointerCurrentRaycast;

            // Check for object hit, get the down handler, call
            GameObject NewPointerPress = ExecuteEvents.ExecuteHierarchy(CurrentObject, Data, ExecuteEvents.pointerDownHandler);

            // If no handler, try and get click handler
            if (NewPointerPress == null)
            {
                NewPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(CurrentObject);
            }

            // Set the data
            Data.pressPosition = Data.position;
            Data.pointerPress = NewPointerPress;
            Data.rawPointerPress = CurrentObject;
        }

        protected void ProcessRelease(PointerEventData InData)
        {
            // Execute pointer up
            ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerUpHandler);

            // Check for a click handler
            GameObject PointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(CurrentObject);

            // Check if actual
            if (Data.pointerPress == PointerUpHandler)
            {
                ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerClickHandler);
            }

            // Clear selected gameobject
            eventSystem.SetSelectedGameObject(null);

            // Reset data
            Data.pressPosition = Vector2.zero;
            Data.pointerPress = null;
            Data.rawPointerPress = null;
        }
    }
}
