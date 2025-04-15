using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.PlayerSettings;

/// <summary>
/// ��
/// �÷��̾ �浹�� ���� �� ���¿��� ��ȣ�ۿ� �� �ش� �̿��Ǵ� ���� ������ �÷��̾ �̵���Ų��.
/// 
/// </summary>
/// 




public class Portal : MonoBehaviour
{
    public string fromNodeGUID;
    public string toNodeGUID;

    public Vector3 toNodePos;

    public void Test()
    {
        GameObject.Find("Player").transform.position = toNodePos;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && this.tag == "Door")
        {
            StartCoroutine(SetPlayerPositionNextFrame(toNodePos));
        }
        else if(other.gameObject.tag == "Player" && this.tag == "Stairs")
        {
            GameManager GM = GameManager.instance;
            GM.GoToNextFloor();
        }

    }

    IEnumerator SetPlayerPositionNextFrame(Vector3 pos)
    {
        yield return null; // ���� �����ӱ��� ��ٸ�
        GameObject.Find("Player").transform.position = pos;
    }

}



[CustomEditor(typeof(Portal))]
public class PortalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("���� ����", GUILayout.Height(30)))
        {
            Portal positioner = (Portal)target;
            positioner.Test();
        }
    }
}