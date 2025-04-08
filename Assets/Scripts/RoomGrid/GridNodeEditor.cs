#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridNode))]
public class GridNodeEditor : Editor
{
    int selectedNumber = 0; // �Է¹��� ����

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //�ν����� UI���� ���η� ����(����)�� ������ִ� �Լ�
        GUILayout.Space(10);
        //��
        GUILayout.Label("��ƼƼ ���� ����", EditorStyles.boldLabel);

        selectedNumber = EditorGUILayout.IntField("Ÿ�� ���̵� �Է�", selectedNumber);

        if (GUILayout.Button("���� ����", GUILayout.Height(30)))
        {
            GridNode node = (GridNode)target;
            node.CreateBox();
        }

        if (GUILayout.Button("���� ����", GUILayout.Height(30)))
        {
            GridNode node = (GridNode)target;
            node.CreateEnemy();
        }

        if (GUILayout.Button("��� ����", GUILayout.Height(30)))
        {

        }
    }
}
#endif