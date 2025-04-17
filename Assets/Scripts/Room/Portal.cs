
using System.Collections;
using UnityEngine;

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
