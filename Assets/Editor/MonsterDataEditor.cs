using OctanGames.MonsterMaker;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterData))]
public class MonsterDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var data = target as MonsterData;

        EditorGUILayout.LabelField(data.Name.ToUpper(), EditorStyles.boldLabel);
        float difficulty = data.Health + data.Damage + data.Speed;
        DrawProgressBar(difficulty / 100, "Difficulty");

        base.OnInspectorGUI();
    }

    private static void DrawProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, EditorStyles.textField);
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space(10);
    }
}