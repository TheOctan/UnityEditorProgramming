using OctanGames.MonsterMaker;
using UnityEditor;
using UnityEngine;

namespace OctanGames
{
    public class MonsterSelectorWindow : EditorWindow
    {
        private MonsterType _selectedMonsterType = MonsterType.Undefined;
        
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
        }
    }
}