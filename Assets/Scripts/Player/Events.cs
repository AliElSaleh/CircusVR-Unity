using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class Events : MonoBehaviour
    {
        public static UnityAction OnTouchpadUp = null;
        public static UnityAction OnTouchpadDown = null;
        public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource;

        public GameObject RightAnchor;
        public GameObject HeadAnchor;

        private Dictionary<OVRInput.Controller, GameObject> ControllerSets = null;
        private OVRInput.Controller InputSource = OVRInput.Controller.None;
        private OVRInput.Controller Controller = OVRInput.Controller.None;
        private bool InputActive = true;

        private string Log;

        private void Awake()
        {
            OVRManager.HMDMounted += PlayerFound;
            OVRManager.HMDUnmounted += PlayerLost;

            ControllerSets = CreateControllerSets();
        }

        private void OnDestroy()
        {
            OVRManager.HMDMounted -= PlayerFound;
            OVRManager.HMDUnmounted -= PlayerLost;
        }

        [UsedImplicitly]
        private void Start()
        {
        }
	
        [UsedImplicitly]
        private void Update()
        {   
            // Check for active input
            if (!InputActive)
                return;

            CheckForController();

            CheckInputSource();

            Input();
        }

        private void CheckInputSource()
        {
            InputSource = UpdateSource(OVRInput.GetActiveController(), InputSource);
        }

        private void Input()
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (OnTouchpadDown != null)
                    OnTouchpadDown();
            }

            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (OnTouchpadUp != null)
                    OnTouchpadUp();
            }
        }

        private OVRInput.Controller UpdateSource(OVRInput.Controller Check, OVRInput.Controller Previous)
        {
            // If values are the same, return
            if (Check == Previous)
                return Previous;

            // Get Controller object
            GameObject ControllerObject;
            ControllerSets.TryGetValue(Check, out ControllerObject);

            // Send out event
            if (OnControllerSource != null)
                OnControllerSource(Check, ControllerObject);

            // Return new value
            return Check;
        }

        private void CheckForController()
        {
            OVRInput.Controller ControllerCheck = Controller;

            // Right remote
            if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
                ControllerCheck = OVRInput.Controller.RTrackedRemote;

            Controller = UpdateSource(ControllerCheck, Controller);
        }

        private void PlayerFound()
        {
            InputActive = true;
            Log = "Player Found";
        }

        private void PlayerLost()
        {
            InputActive = false;
            Log = "Player Lost";
        }

        private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets()
        {
            Dictionary<OVRInput.Controller, GameObject> NewSets = new Dictionary<OVRInput.Controller, GameObject>()
            {
                {OVRInput.Controller.RTrackedRemote, RightAnchor}
            };

            return NewSets;
        }
    }
}
