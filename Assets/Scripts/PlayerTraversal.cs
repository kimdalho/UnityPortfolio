using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTraversal : MonoBehaviour
{
    public List<CinemachineCamera> cams;

    public List<JumpPoint> jumpPoints;
    [SerializeField] AnimationCurve speedCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private Room nextRoom;
    [SerializeField]
    private uint flow = 0;
    public void StartPlayerTraversal(Player player, Portal potal)
    {
        jumpPoints.Last().nextPoint = potal.toNextSpawnPoint;
        nextRoom = potal.toNextRoom;
        player.MoveAnimStop();
        player.gameplayTagSystem.AddTag(eTagType.Player_State_IgnoreInput);
        StartCoroutine(TraverseTo(player, jumpPoints[0]));
        cams[0].LookAt = player.transform;
        cams[1].LookAt = player.transform;
    }

    public IEnumerator TraverseTo(Player player, JumpPoint target)
    {

        Vector3 start = player.transform.position;
        Vector3 end = target.transform.position;
        float height = 2.5f; // 포물선 최고 높이
        float duration = 0.8f;
        float time = 0f;
        player.OnJumpStart();

        if(flow == 1)
        {
            cams[0].Priority = 10;
        }
        else if(flow == 4)
        {
            cams[0].Priority = 0;
            cams[1].Priority = 10;
        }
        
        yield return new WaitForSeconds(0.2f);
        if(SoundManager.instance != null)
                SoundManager.instance.PlayEffect(eEffectType.Jump);
        while (time < duration)
        {
            float t = time / duration;
            float curvedT = speedCurve.Evaluate(t); // 속도 곡선 적용

            // 수평 이동 (X, Z)
            Vector3 horizontal = Vector3.Lerp(start, end, curvedT);

            // 포물선 Y 좌표 계산
            //float y = Mathf.Lerp(start.y, end.y, t) + height * Mathf.Sin(Mathf.PI * t);
            float y = Mathf.Lerp(start.y, end.y, curvedT) + height * Mathf.Sin(Mathf.PI * curvedT);

            player.transform.position = new Vector3(horizontal.x, y, horizontal.z);
            player.OnFalling();
           
            time += Time.deltaTime;
            yield return null;
        }

        player.transform.position = end;

        // 착지 후 다음으로 이동
        if (target.nextPoint != null)
        {
            flow++;
            player.OnEndJump();
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(TraverseTo(player,target.nextPoint));
        }
        else
        {
            cams[0].Priority = 0;
            cams[1].Priority = 0;
            flow = 0;
            // 마지막 점프 끝! 조작 가능 상태로 복구
            player.gameplayTagSystem.RemoveTag(eTagType.Player_State_IgnoreInput);
            player.PortalDelay();            
            player.PlayAnimIdle();
            StartCoroutine(GameManager.instance.ChangeCurrentRoom(nextRoom));
        }
    }
}
