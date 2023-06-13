using UnityEngine;

namespace Aezakmi.PlayerSystem
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool IsMoving => joystick.Horizontal != 0 || joystick.Vertical != 0;
        public float speed;

        [SerializeField] private Joystick joystick;

        private Camera m_mainCamera;
        private CharacterController m_characterController;

        private void Start()
        {
            m_mainCamera = Camera.main;
            m_characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            ApplyGravity();
            if (!IsMoving) return;
            Rotate();
            Move();
        }

        private void ApplyGravity()
        {
            var gravity = m_characterController.isGrounded ? 0f : Physics.gravity.y * Time.deltaTime;
            m_characterController.Move(gravity * Vector3.up);
        }

        private void Rotate()
        {
            transform.forward = new Vector3
            (
                joystick.Horizontal,
                0,
                joystick.Vertical
            );

            transform.localEulerAngles += m_mainCamera.transform.eulerAngles.y * Vector3.up;
        }

        private void Move()
        {
            var moveAmount = transform.forward * speed * Time.deltaTime;
            m_characterController.Move(moveAmount);
        }
    }
}