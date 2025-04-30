using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTraversal : MonoBehaviour
{
    public List<JumpPoint> jumpPoints;
    [SerializeField] AnimationCurve speedCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private Room nextRoom;
    public void StartPlayerTraversal(Player player, Portal potal)
    {
        jumpPoints.Last().nextPoint = potal.toNextPoint;
        nextRoom = potal.toNextRoom;
        player.MoveAnimStop();
        player.gameplayTagSystem.AddTag(eTagType.Player_State_IgnoreInput);
        StartCoroutine(TraverseTo(player, jumpPoints[0]));
    }

    public IEnumerator TraverseTo(Player player, JumpPoint target)
    {
        Vector3 start = player.transform.position;
        Vector3 end = target.transform.position;
        float height = 2.5f; // ������ �ְ� ����
        float duration = 0.8f;
        float time = 0f;
        player.OnJumpStart();
        
        yield return new WaitForSeconds(0.2f);
        while (time < duration)
        {
            float t = time / duration;
            float curvedT = speedCurve.Evaluate(t); // �ӵ� � ����

            // ���� �̵� (X, Z)
            Vector3 horizontal = Vector3.Lerp(start, end, curvedT);

            // ������ Y ��ǥ ���
            //float y = Mathf.Lerp(start.y, end.y, t) + height * Mathf.Sin(Mathf.PI * t);
            float y = Mathf.Lerp(start.y, end.y, curvedT) + height * Mathf.Sin(Mathf.PI * curvedT);

            player.transform.position = new Vector3(horizontal.x, y, horizontal.z);
            player.OnFalling();
           
            time += Time.deltaTime;
            yield return null;
        }

        player.transform.position = end;

        // ���� �� �������� �̵�
        if (target.nextPoint != null)
        {
            player.OnEndJump();
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(TraverseTo(player,target.nextPoint));
        }
        else
        {
            // ������ ���� ��! ���� ���� ���·� ����
            player.gameplayTagSystem.RemoveTag(eTagType.Player_State_IgnoreInput);
            player.PortalDelay();
            GameManager.instance.ChangeCurrentRoom(nextRoom);
        }
    }
}
