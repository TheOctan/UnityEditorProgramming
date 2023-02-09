using UnityEngine;

namespace OctanGames
{
    public class PatrolBetweenPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] _patrolPoints;

        [Header("Gizmo settings")]
        [SerializeField] private float _patrolPointSize = 0.25f;
        [SerializeField] private Color _patrolPointColor = Color.cyan;
        [SerializeField] private Color _patrolPathColor = Color.blue;
        
        [Header("Editor Only")]
        [SerializeField] private bool _drawOnlySelected = true;

        private void OnDrawGizmos()
        {
            if (_drawOnlySelected) return;
            DrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            if (!_drawOnlySelected) return;
            DrawGizmos();
        }

        private void DrawGizmos()
        {
            DrawPoints();
            DrawPath();
        }

        private void DrawPoints()
        {
            Gizmos.color = _patrolPointColor;
            if (_patrolPoints == null) return;
            foreach (Transform point in _patrolPoints)
            {
                if(point == null) continue;
                Gizmos.DrawSphere(point.position, _patrolPointSize);
            }
        }

        private void DrawPath()
        {
            Gizmos.color = _patrolPathColor;
            for (var i = 0; i < _patrolPoints.Length - 1; i++)
            {
                if (_patrolPoints[i] == null || _patrolPoints[i + 1] == null) continue;

                Vector3 point = _patrolPoints[i].position;
                Vector3 nextPoint = _patrolPoints[i + 1].position;

                Gizmos.DrawLine(point, nextPoint);
            }
        }
    }
}