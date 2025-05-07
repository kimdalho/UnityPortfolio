#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridNode))]
public class GridNodeEditor : Editor
{
    int index = 0; // �Է¹��� ����

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //�ν����� UI���� ���η� ����(����)�� ������ִ� �Լ�
        GUILayout.Space(10);
        //��
        GUILayout.Label("��ƼƼ ���� ����", EditorStyles.boldLabel);

        index = EditorGUILayout.IntField("Ÿ�� ���̵� �Է�", index);

        if (GUILayout.Button("���� ����", GUILayout.Height(30)))
        {
            GridNode node = (GridNode)target;
            var newitem = ResourceManager.Instance.CreateWeaponItemToIndex(index, node.transform);            
        }

        if (GUILayout.Button("���� ����", GUILayout.Height(30)))
        {
            GridNode node = (GridNode)target;
            var newitem = ResourceManager.Instance.CreateMonsterToIndex(index, node.transform);            
        }

        if (GUILayout.Button("�ڽ� ũ�� Ȯ��", GUILayout.Height(30)))
        {
            GridNode node = (GridNode)target;            
        }
    }
}
#endif