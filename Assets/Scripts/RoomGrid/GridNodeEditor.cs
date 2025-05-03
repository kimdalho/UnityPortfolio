#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridNode))]
public class GridNodeEditor : Editor
{
    int selectedNumber = 0; // 입력받을 숫자

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //인스펙터 UI에서 세로로 공간(간격)을 만들어주는 함수
        GUILayout.Space(10);
        //라벨
        GUILayout.Label("엔티티 생성 도구", EditorStyles.boldLabel);

        selectedNumber = EditorGUILayout.IntField("타겟 아이디 입력", selectedNumber);

        if (GUILayout.Button("상자 생성", GUILayout.Height(30)))
        {
            GridNode node = (GridNode)target;
            node.CreateBox();
        }

        if (GUILayout.Button("몬스터 생성", GUILayout.Height(30)))
        {
            GridNode node = (GridNode)target;            
        }

        if (GUILayout.Button("박스 크기 확장", GUILayout.Height(30)))
        {
            GridNode node = (GridNode)target;
            node.gameObject.transform.localScale = new Vector3(1, 3, 1);
        }
    }
}
#endif