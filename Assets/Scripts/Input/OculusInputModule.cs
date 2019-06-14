namespace Assets.Scripts.Input
{
    public class OculusInputModule : VRInputModule
    {
        public OVRInput.Controller Source = OVRInput.Controller.RTrackedRemote;
        public OVRInput.Button Click = OVRInput.Button.Any;

        public override void Process()
        {
            base.Process();

            // Press
            if (OVRInput.GetDown(Click, Source))
                ProcessPress(Data);

            // Release
            if (OVRInput.GetUp(Click, Source))
                ProcessRelease(Data);
        }
    }
}
