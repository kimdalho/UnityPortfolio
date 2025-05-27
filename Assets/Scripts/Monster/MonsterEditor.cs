#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MonsterFSM))]
public class MonsterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //�ν����� UI���� ���η� ����(����)�� ������ִ� �Լ�
        GUILayout.Space(10);
        GUILayout.Label("MonsterEditor", EditorStyles.boldLabel);

        if (GUILayout.Button("ResetPos", GUILayout.Height(30)))
        {
            MonsterFSM mon = (MonsterFSM)target;
            mon.GetMon().ResetPos();
        }

        if (GUILayout.Button("Kill", GUILayout.Height(30)))
        {
            MonsterFSM mon = (MonsterFSM)target;
            mon.GetMon().Kill();
        }
    }
}
#endif