using System.IO;
using UnityEngine;

namespace OctanGames.VisualDebug
{
    [AddComponentMenu("Editor/Custom Gizmos")]
    public class CustomGizmos : MonoBehaviour
    {
        private const string CUSTOM_GIZMOS_ICON = "CustomGizmos Icon";
        private const string GIZMOS_PATH = "OctanGames/VisualDebug";

        [SerializeField] private bool _enabled = true;
        [SerializeField] private GizmoType _type = GizmoType.WireSphere;
        [SerializeField] private Color _color = Color.white;

        [SerializeField] private Vector3 _offset;

        [SerializeField] private Vector3 _startPoint;
        [SerializeField] private Vector3 _endPoint = Vector3.one;

        [SerializeField] private Vector3 _direction = Vector3.one;

        [SerializeField] private float _radius = 0.5f;

        [SerializeField] private Vector3 _size = Vector3.one;

        [SerializeField] private Mesh _mesh;

        [Range(0.3f, 179)]
        [SerializeField] private float _fieldOfView = 60f;
        [Header("Clipping planes")]
        [SerializeField] private float _near = 0.3f;
        [SerializeField] private float _far = 5f;
        [Header("Aspect")]
        [SerializeField] private float _width = 16f;
        [SerializeField] private float _height = 9f;

        [SerializeField] private Vector2 _textureSize = Vector2.one;
        [SerializeField] private Vector2 _textureOffset;

        [SerializeField] private Texture _texture;
        [SerializeField] private Material _material;

        [SerializeField] private string _iconName = CUSTOM_GIZMOS_ICON;

        [Header("Flags")]
        [SerializeField] private bool _drawOnlySelected;
        [SerializeField] private bool _transformWithObject;

        public bool Enabled
        {
            get => _enabled;
            set => _enabled = value;
        }

        private void OnDrawGizmos()
        {
            if (!_enabled || _drawOnlySelected) return;
            DrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            if (!_enabled || !_drawOnlySelected) return;
            DrawGizmos();
        }

        private void DrawGizmos()
        {
            Gizmos.color = _color;

            CheckTransformation();
            Vector3 position = GetPosition();

            switch (_type)
            {
                case GizmoType.Line:
                    Gizmos.DrawLine(_startPoint + position, _endPoint + position);
                    break;
                case GizmoType.Ray:
                    Gizmos.DrawRay(position, _direction.normalized);
                    break;
                case GizmoType.Sphere:
                    Gizmos.DrawSphere(position, _radius);
                    break;
                case GizmoType.Cube:
                    Gizmos.DrawCube(position, _size);
                    break;
                case GizmoType.Mesh:
                    if (_mesh == null) break;
                    Gizmos.DrawMesh(_mesh, position);
                    break;
                case GizmoType.WireSphere:
                    Gizmos.DrawWireSphere(position, _radius);
                    break;
                case GizmoType.WireCube:
                    Gizmos.DrawWireCube(position, _size);
                    break;
                case GizmoType.WireMesh:
                    if (_mesh == null) break;
                    Gizmos.DrawWireMesh(_mesh, position);
                    break;
                case GizmoType.Frustum:
                    Gizmos.DrawFrustum(position, _fieldOfView,
                        _far, _near, _width / _height);
                    break;
                case GizmoType.Texture:
                    Vector2 texturePosition = (Vector2)transform.position + _textureOffset;
                    var rect = new Rect(texturePosition, _textureSize);
                    Gizmos.DrawGUITexture(rect, _texture, _material);
                    break;
                case GizmoType.Icon:
                    string iconName = string.IsNullOrEmpty(_iconName)
                        ? CUSTOM_GIZMOS_ICON
                        : _iconName;
                    string path = Path.Combine(GIZMOS_PATH, iconName);
                    Gizmos.DrawIcon(transform.position, path, true, _color);
                    break;
            }

            Gizmos.color = Color.white;
        }

        private void CheckTransformation()
        {
            switch (_type)
            {
                case GizmoType.Frustum:
                    _transformWithObject = true;
                    break;
                case GizmoType.Icon:
                case GizmoType.Texture:
                    _transformWithObject = false;
                    break;
            }

            if (_transformWithObject)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
            }
        }

        private Vector3 GetPosition()
        {
            return _offset + (_transformWithObject
                ? Vector3.zero
                : transform.position);
        }
    }
}