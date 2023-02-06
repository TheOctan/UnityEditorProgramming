using OctanGames.VisualDebug;
using UnityEditor;
using UnityEditor.AnimatedValues;
using GizmoType = OctanGames.VisualDebug.GizmoType;

namespace OctanGames
{
    [CustomEditor(typeof(CustomGizmos))]
    public class CustomGizmosEditor : Editor
    {
        private SerializedProperty _enabled;
        private SerializedProperty _type;
        private SerializedProperty _color;

        private SerializedProperty _offset;

        private SerializedProperty _startPoint;
        private SerializedProperty _endPoint;

        private SerializedProperty _direction;

        private SerializedProperty _radius;

        private SerializedProperty _size;

        private SerializedProperty _mesh;

        private SerializedProperty _fieldOfView;
        private SerializedProperty _near;
        private SerializedProperty _far;
        private SerializedProperty _width;
        private SerializedProperty _height;

        private SerializedProperty _textureSize;
        private SerializedProperty _textureOffset;

        private SerializedProperty _texture;
        private SerializedProperty _material;

        private SerializedProperty _iconName;

        private SerializedProperty _drawOnlySelected;
        private SerializedProperty _transformWithObject;

        private AnimBool _hasOffsetAnimBool;
        private AnimBool _lineAnimBool;
        private AnimBool _rayAnimBool;
        private AnimBool _sphereAnimBool;
        private AnimBool _cubeAnimBool;
        private AnimBool _meshAnimBool;
        private AnimBool _frustumAnimBool;
        private AnimBool _textureAnimBool;
        private AnimBool _iconAnimBool;

        private void OnEnable()
        {
            FindProperties();
            InitializeAnimBool();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_enabled);
            EditorGUILayout.PropertyField(_type);
            EditorGUILayout.PropertyField(_color);

            EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);
            var type = (GizmoType)_type.enumValueIndex;

            _hasOffsetAnimBool.target = HasOffset(type);
            if (EditorGUILayout.BeginFadeGroup(_hasOffsetAnimBool.faded))
            {
                EditorGUILayout.PropertyField(_offset);
            }
            EditorGUILayout.EndFadeGroup();

            _lineAnimBool.target = type == GizmoType.Line;
            if (EditorGUILayout.BeginFadeGroup(_lineAnimBool.faded))
            {
                EditorGUILayout.PropertyField(_startPoint);
                EditorGUILayout.PropertyField(_endPoint);
            }
            EditorGUILayout.EndFadeGroup();

            _rayAnimBool.target = type == GizmoType.Ray;
            if (EditorGUILayout.BeginFadeGroup(_rayAnimBool.faded))
            {
                EditorGUILayout.PropertyField(_direction);
            }
            EditorGUILayout.EndFadeGroup();

            _sphereAnimBool.target = type is GizmoType.Sphere or GizmoType.WireSphere;
            if (EditorGUILayout.BeginFadeGroup(_sphereAnimBool.faded))
            {
                EditorGUILayout.PropertyField(_radius);
            }
            EditorGUILayout.EndFadeGroup();

            _cubeAnimBool.target = type is GizmoType.Cube or GizmoType.WireCube;
            if (EditorGUILayout.BeginFadeGroup(_cubeAnimBool.faded))
            {
                EditorGUILayout.PropertyField(_size);
            }
            EditorGUILayout.EndFadeGroup();

            _meshAnimBool.target = type is GizmoType.Mesh or GizmoType.WireMesh;
            if (EditorGUILayout.BeginFadeGroup(_meshAnimBool.faded))
            {
                EditorGUILayout.PropertyField(_mesh);
            }
            EditorGUILayout.EndFadeGroup();

            _frustumAnimBool.target = type == GizmoType.Frustum;
            if (EditorGUILayout.BeginFadeGroup(_frustumAnimBool.faded))
            {
                EditorGUILayout.PropertyField(_fieldOfView);
                EditorGUILayout.PropertyField(_near);
                EditorGUILayout.PropertyField(_far);
                EditorGUILayout.PropertyField(_width);
                EditorGUILayout.PropertyField(_height);
            }
            EditorGUILayout.EndFadeGroup();

            _textureAnimBool.target = type == GizmoType.Texture;
            if (EditorGUILayout.BeginFadeGroup(_textureAnimBool.faded))
            {
                EditorGUILayout.PropertyField(_textureSize);
                EditorGUILayout.PropertyField(_textureOffset);
                EditorGUILayout.PropertyField(_texture);
                EditorGUILayout.PropertyField(_material);
            }
            EditorGUILayout.EndFadeGroup();

            _iconAnimBool.target = type == GizmoType.Icon;
            if (EditorGUILayout.BeginFadeGroup(_iconAnimBool.faded))
            {
                EditorGUILayout.PropertyField(_iconName);
            }
            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.PropertyField(_drawOnlySelected);
            EditorGUILayout.PropertyField(_transformWithObject);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private static bool HasOffset(GizmoType type)
        {
            return type is not (GizmoType.Texture or GizmoType.Icon or GizmoType.Frustum);
        }

        private void FindProperties()
        {
            _enabled = serializedObject.FindProperty(nameof(_enabled));
            _type = serializedObject.FindProperty(nameof(_type));
            _color = serializedObject.FindProperty(nameof(_color));
            _offset = serializedObject.FindProperty(nameof(_offset));
            _startPoint = serializedObject.FindProperty(nameof(_startPoint));
            _endPoint = serializedObject.FindProperty(nameof(_endPoint));
            _direction = serializedObject.FindProperty(nameof(_direction));
            _radius = serializedObject.FindProperty(nameof(_radius));
            _size = serializedObject.FindProperty(nameof(_size));
            _mesh = serializedObject.FindProperty(nameof(_mesh));
            _fieldOfView = serializedObject.FindProperty(nameof(_fieldOfView));
            _near = serializedObject.FindProperty(nameof(_near));
            _far = serializedObject.FindProperty(nameof(_far));
            _width = serializedObject.FindProperty(nameof(_width));
            _height = serializedObject.FindProperty(nameof(_height));
            _textureSize = serializedObject.FindProperty(nameof(_textureSize));
            _textureOffset = serializedObject.FindProperty(nameof(_textureOffset));
            _texture = serializedObject.FindProperty(nameof(_texture));
            _material = serializedObject.FindProperty(nameof(_material));
            _iconName = serializedObject.FindProperty(nameof(_iconName));
            _drawOnlySelected = serializedObject.FindProperty(nameof(_drawOnlySelected));
            _transformWithObject = serializedObject.FindProperty(nameof(_transformWithObject));
        }

        private void InitializeAnimBool()
        {
            var type = (GizmoType)_type.enumValueIndex;

            _hasOffsetAnimBool = new AnimBool(HasOffset(type), Repaint);
            _lineAnimBool = new AnimBool(type == GizmoType.Line, Repaint);
            _rayAnimBool = new AnimBool(type == GizmoType.Ray, Repaint);
            _sphereAnimBool = new AnimBool(type is GizmoType.Sphere or GizmoType.WireSphere, Repaint);
            _cubeAnimBool = new AnimBool(type is GizmoType.Cube or GizmoType.WireCube, Repaint);
            _meshAnimBool = new AnimBool(type is GizmoType.Mesh or GizmoType.WireMesh, Repaint);
            _frustumAnimBool = new AnimBool(type == GizmoType.Frustum, Repaint);
            _textureAnimBool = new AnimBool(type == GizmoType.Texture, Repaint);
            _iconAnimBool = new AnimBool(type == GizmoType.Icon, Repaint);
        }
    }
}