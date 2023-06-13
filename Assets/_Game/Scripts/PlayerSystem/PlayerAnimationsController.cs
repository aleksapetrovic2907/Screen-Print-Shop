using UnityEngine;

namespace Aezakmi.PlayerSystem
{
    public class PlayerAnimationsController : MonoBehaviour
    {
        private Animator m_animator;
        private PlayerMovement m_playerMovement;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_playerMovement = GetComponent<PlayerMovement>();
        }

        private void LateUpdate()
        {
            m_animator.SetBool("Running", m_playerMovement.enabled && m_playerMovement.IsMoving);
        }
    }
}