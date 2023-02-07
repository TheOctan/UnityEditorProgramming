using OctanGames.MonsterMaker;
using UnityEditor;
using UnityEngine;

namespace OctanGames.Drawers
{
    [CustomPropertyDrawer(typeof(MonsterAbility))]
    public class MonsterAbilityDrawer : PropertyDrawer
    {
        private SerializedProperty _name;
        private SerializedProperty _damage;
        private SerializedProperty _element;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            FindProperties(property);

            var foldOutBox = new Rect(position.xMin, position.yMin, position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded, label);

            if (property.isExpanded)
            {
                DrawProperties(position);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var totalLines = 1;
            float lineHeight = EditorGUIUtility.singleLineHeight;

            if (property.isExpanded)
            {
                totalLines += 3;
            }

            return lineHeight * totalLines;
        }

        private void FindProperties(SerializedProperty property)
        {
            _name = property.FindPropertyRelative(nameof(_name));
            _damage = property.FindPropertyRelative(nameof(_damage));
            _element = property.FindPropertyRelative(nameof(_element)); 
        }

        private void DrawProperties(Rect position)
        {
            float h = EditorGUIUtility.singleLineHeight;

            float defaultLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 60;

            var rect = new Rect(position.xMin, position.yMin + h, position.width * 0.5f, h);
            EditorGUI.PropertyField(rect, _name);

            rect = new Rect(position.xMin + position.width * 0.55f, position.yMin + h, position.width * 0.45f, h);
            EditorGUI.PropertyField(rect, _damage);

            rect = new Rect(position.xMin, position.yMin + h * 2, position.width * 0.5f, h);
            EditorGUI.PropertyField(rect, _element);
            EditorGUIUtility.labelWidth = defaultLabelWidth;

        }
    }
}