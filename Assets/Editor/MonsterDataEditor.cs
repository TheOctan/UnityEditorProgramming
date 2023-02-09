﻿using OctanGames.MonsterMaker;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace OctanGames
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonsterData))]
    public class MonsterDataEditor : Editor
    {
        private SerializedProperty _name;
        private SerializedProperty _monsterType;
        private SerializedProperty _chanceToDropItem;
        private SerializedProperty _rangeOfAwareness;
        private SerializedProperty _canEnterCombat;

        private SerializedProperty _damage;
        private SerializedProperty _health;
        private SerializedProperty _speed;

        private SerializedProperty _battleCry;
        private SerializedProperty _abilities;

        private AnimBool _canEnterCombatAnimBool;

        private void OnEnable()
        {
            FindProperties();
            _canEnterCombatAnimBool = new AnimBool(_canEnterCombat.boolValue, Repaint);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.LabelField(_name.stringValue.ToUpper(), EditorStyles.boldLabel);
            float difficulty = _health.intValue + _damage.intValue + _speed.intValue;
            DrawProgressBar(difficulty / 100, "Difficulty");

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_name);
            if (_name.stringValue == string.Empty)
            {
                EditorGUILayout.HelpBox(
                    "Caution: No name specified. Please name the monster!", MessageType.Warning);
            }

            EditorGUILayout.PropertyField(_monsterType);
            if ((MonsterType)_monsterType.enumValueIndex == MonsterType.None)
            {
                EditorGUILayout.HelpBox("No Monster type selected", MessageType.Warning);
            }

            EditorGUILayout.PropertyField(_chanceToDropItem);
            EditorGUILayout.PropertyField(_rangeOfAwareness);
            EditorGUILayout.PropertyField(_canEnterCombat);

            _canEnterCombatAnimBool.target = _canEnterCombat.boolValue;
            if (EditorGUILayout.BeginFadeGroup(_canEnterCombatAnimBool.faded))
            {
                EditorGUI.indentLevel++;

                float defaultLabelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 70;
                EditorGUILayout.PropertyField(_health);
                if (_health.intValue < 0)
                {
                    EditorGUILayout.HelpBox("Should not have negative Health", MessageType.Warning);
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(_speed);
                EditorGUILayout.PropertyField(_damage);
                EditorGUILayout.EndHorizontal();
                EditorGUIUtility.labelWidth = defaultLabelWidth;

                EditorGUILayout.Space(10);
                if (GUILayout.Button("Randomize stats"))
                {
                    RandomizeStats();
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.PropertyField(_battleCry);
            EditorGUILayout.PropertyField(_abilities);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private static void DrawProgressBar(float value, string label)
        {
            Rect rect = GUILayoutUtility.GetRect(18, 18, EditorStyles.textField);
            EditorGUI.ProgressBar(rect, value, label);
        }

        private void RandomizeStats()
        {
            _health.intValue = Random.Range(1, 25);
            _speed.intValue = Random.Range(1, 25);
            _damage.intValue = Random.Range(1, 25);
        }

        private void FindProperties()
        {
            _name = serializedObject.FindProperty(nameof(_name));
            _monsterType = serializedObject.FindProperty(nameof(_monsterType));
            _chanceToDropItem = serializedObject.FindProperty(nameof(_chanceToDropItem));
            _rangeOfAwareness = serializedObject.FindProperty(nameof(_rangeOfAwareness));
            _canEnterCombat = serializedObject.FindProperty(nameof(_canEnterCombat));
            _damage = serializedObject.FindProperty(nameof(_damage));
            _health = serializedObject.FindProperty(nameof(_health));
            _speed = serializedObject.FindProperty(nameof(_speed));
            _battleCry = serializedObject.FindProperty(nameof(_battleCry));
            _abilities = serializedObject.FindProperty(nameof(_abilities));
        }
    }
}