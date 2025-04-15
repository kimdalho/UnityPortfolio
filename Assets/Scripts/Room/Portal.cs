using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.PlayerSettings;

/// <summary>
/// 문
/// 플레이어가 충돌을 유지 된 상태에서 상호작용 시 해당 이웃되는 룸의 문으로 플레이어를 이동시킨다.
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
        var tagSystem = GameManager.instance.GetPlayer().gameplayTagSystem;
        if (tagSystem.HasTag(eTagType.portalLock) == true)
            return;

        if (other.gameObject.tag == "Player" && 
            this.tag == "Door")
        {
            StartCoroutine(NextRoom());
            StartCoroutine(SetPortalLock());
            
        }
        else if(other.gameObject.tag == "Player" 
            && this.tag == "Stairs")
        {            
            var GM = GameManager.instance;
            GM.GoToNextFloor();
        }

    }

    IEnumerator NextRoom()
    {
        yield return null;
        GameManager.instance.GetPlayer().transform.position = toNodePos;
    }


    IEnumerator SetPortalLock()
    {        
        var tagSystem = GameManager.instance.GetPlayer().gameplayTagSystem;
        tagSystem.AddTag(eTagType.portalLock);       
        yield return new WaitForSeconds(1f); 
        tagSystem.RemoveTag(eTagType.portalLock);

    }

}



[CustomEditor(typeof(Portal))]
public class PortalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("던전 생성", GUILayout.Height(30)))
        {
            Portal positioner = (Portal)target;
            positioner.Test();
        }
    }
}