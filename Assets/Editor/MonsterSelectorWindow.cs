using System;
using OctanGames.MonsterMaker;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OctanGames
{
    public class MonsterSelectorWindow : EditorWindow
    {
        private MonsterType _selectedMonsterType = MonsterType.None;
        private MonsterType _previousSelectedMonsterType = MonsterType.None;
        private int _selectionIndex;

        private Object[] _selectedObjects;

        [MenuItem("Window/Monster Selector")]
        private static void ShowWindow()
        {
            GetWindow<MonsterSelectorWindow>("Monster Selector");
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(10);
            GUILayout.Label("Selection Filters:", EditorStyles.boldLabel);
            _selectedMonsterType =
                (MonsterType)EditorGUILayout.EnumPopup("Monster type to select:", _selectedMonsterType);

            EditorGUILayout.Space(5);
            if (GUILayout.Button("Select All"))
            {
                TryUpdateSelectedObjects();
                SelectAllMonsters();
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Cycle Selection:");
            if (GUILayout.Button("Previous"))
            {
                TryUpdateSelectedObjects();
                SelectPrevious();
            }
            if (GUILayout.Button("Next"))
            {
                TryUpdateSelectedObjects();
                SelectNext();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void OnHierarchyChange()
        {
            UpdateSelectedObjects();
        }

        private void SelectAllMonsters()
        {
            Selection.objects = _selectedObjects;
        }

        private void SelectPrevious()
        {
            if (_selectedObjects.Length <= 0) return;

            _selectionIndex--;
            if (_selectionIndex < 0)
            {
                _selectionIndex = _selectedObjects.Length - 1;
            }

            Object selectedObject = _selectedObjects[_selectionIndex];
            if (selectedObject == null) return;

            Selection.activeObject = selectedObject;
        }

        private void SelectNext()
        {
            if (_selectedObjects.Length <= 0) return;

            _selectionIndex++;
            if (_selectionIndex > _selectedObjects.Length - 1)
            {
                _selectionIndex = 0;
            }

            Object selectedObject = _selectedObjects[_selectionIndex];
            if (selectedObject == null) return;

            Selection.activeObject = selectedObject;
        }

        private void TryUpdateSelectedObjects()
        {
            if (_selectedMonsterType == _previousSelectedMonsterType) return;
            UpdateSelectedObjects();
            _previousSelectedMonsterType = _selectedMonsterType;
        }

        private void UpdateSelectedObjects()
        {
            _selectionIndex = -1;
            _selectedObjects = FindObjectsOfType<Monster>()
                .Where(m => m.Data.Type == _selectedMonsterType)
                .Select(m => m.gameObject as Object)
                .ToArray();
        }
    }
}