using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Range(1.0f, 10.0f)]
        public float MovementSpeed = 10.0f;

        private CharacterController CharacterController;

        [UsedImplicitly]
        private void Start()
        {
		    CharacterController = GetComponent<CharacterController>();
        }
	
        [UsedImplicitly]
        private void Update()
        {
            // Get input keys
            float HorizontalInput = Input.GetAxisRaw("Horizontal") * MovementSpeed;
            float VerticalInput = Input.GetAxisRaw("Vertical") * MovementSpeed;

            // Calculate movement directions
            Vector3 ForwardMovement = transform.forward * HorizontalInput;
            Vector3 RightMovement = transform.right * VerticalInput;

            ApplyMovement(ForwardMovement + -RightMovement);
        }

        private void ApplyMovement(Vector3 WorldDirection)
        {
            CharacterController.SimpleMove(WorldDirection);
        }
    }
}
