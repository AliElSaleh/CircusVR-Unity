namespace Assets.Scripts.Input
{
    public class OculusInputModule : VRInputModule
    {
        public OVRInput.Controller Source = OVRInput.Controller.RTrackedRemote;
        public OVRInput.Button Click = OVRInput.Button.PrimaryIndexTrigger;

        public override void Process()
        {
            base.Process();

            // Press
            if (OVRInput.GetDown(Click, Source) /*|| UnityEngine.Input.GetMouseButtonDown(0)*/)
                ProcessPress(Data);

            // Release
            if (OVRInput.GetUp(Click, Source) /*|| UnityEngine.Input.GetMouseButtonUp(0)*/)
                ProcessRelease(Data);
        }
    }
}
