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

        if (data.Name == string.Empty)
        {
            EditorGUILayout.HelpBox(
                "Caution: No name specified. Please name the monster!", MessageType.Warning);
        }
        if (data.Type == MonsterType.Undefined)
        {
            EditorGUILayout.HelpBox("No Monster type selected", MessageType.Warning);
        }
        if (data.Health < 0)
        {
            EditorGUILayout.HelpBox("Should not have negative Health", MessageType.Warning);
        }
    }

    private static void DrawProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, EditorStyles.textField);
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space(10);
    }
}