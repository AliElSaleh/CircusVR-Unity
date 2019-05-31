using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts.Player
{
    public class PlayerLook : MonoBehaviour
    {
        private const string MouseXInputName = "Mouse X";
        private const string MouseYInputName = "Mouse Y";

        [SerializeField]
        [Range(1.0f, 10.0f)]
        private const float MouseSensitivity = 2.0f;

        [SerializeField]
        private Transform Player;

        private float XAxisClamp;

        [UsedImplicitly]
        private void Awake()
        {
            Player = transform.parent;

            // Lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
        }
	
        [UsedImplicitly]
        private void Update()
        {
            RotateCamera();
        }

        private void RotateCamera()
        {
            float MouseX = Input.GetAxisRaw(MouseXInputName) * MouseSensitivity;
            float MouseY = Input.GetAxisRaw(MouseYInputName) * MouseSensitivity;

            XAxisClamp += MouseY;

            if (XAxisClamp > 90.0f)
            {
                XAxisClamp = 90.0f;
                MouseY = 0.0f;
                ClampXAxisRotationToValue(270.0f);
            }
            else if (XAxisClamp < -90.0f)
            {
                XAxisClamp = -90.0f;
                MouseY = 0.0f;
                ClampXAxisRotationToValue(90.0f);
            }

            transform.Rotate(Vector3.left * MouseY);
            Player.Rotate(Vector3.up * MouseX);
        }

        private void ClampXAxisRotationToValue(float Value)
        {
            Vector3 EulerRotation = transform.eulerAngles;
            EulerRotation.x = Value;
            transform.eulerAngles = EulerRotation;
        }
    }
}
