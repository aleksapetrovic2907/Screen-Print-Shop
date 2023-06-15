using UnityEngine;
using Pathfinding;

namespace Aezakmi.CustomerSystem
{
    public class CustomerAnimationsController : MonoBehaviour
    {
        private Animator m_animator;
        private AIPath m_aiPath;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_aiPath = GetComponent<AIPath>();
        }
        
        private void LateUpdate()
        {
            m_animator.SetBool("Walking", m_aiPath.canMove);
        }
    }
}