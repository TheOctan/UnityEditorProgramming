using OctanGames.MonsterMaker;
using UnityEditor;

[CustomEditor(typeof(MonsterData))]
public class MonsterDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var data = target as MonsterData;

        if (data is not null) EditorGUILayout.LabelField(data.Name.ToUpper(), EditorStyles.boldLabel);
        base.OnInspectorGUI();
    }
}