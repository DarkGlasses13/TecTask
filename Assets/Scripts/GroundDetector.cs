using UnityEngine;

namespace Assets.Scripts
{
    public class GroundDetector : MonoBehaviour
    {
        private LayerMask _groundLayer;
        private readonly float _detectionDistance = 1;
        private readonly float _detectionSize = 0.2f;

        private void Awake() => _groundLayer = LayerMask.GetMask("Ground");

        public bool IsGround()
        {
            Vector2 origin = transform.position + Vector3.down;
            Vector2 size = Vector3.one * _detectionSize;

            return Physics2D.BoxCast(origin, size, 0, Vector3.down, _detectionDistance, _groundLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + Vector3.down, Vector3.one * _detectionSize);
        }
    }
}