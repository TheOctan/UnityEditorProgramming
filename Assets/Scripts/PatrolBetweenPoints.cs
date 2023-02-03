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

        private void OnDrawGizmos()
        {
            DrawPoints();
            DrawPath();
        }

        private void DrawPoints()
        {
            Gizmos.color = _patrolPointColor;
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