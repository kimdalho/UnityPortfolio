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

    public bool isTravaling;
    public void StartPlayerTraversal(Player player, Portal potal)
    {
        if (isTravaling == true)
            return;

        isTravaling = true;
        jumpPoints.Last().nextPoint = potal.toNextSpawnPoint;
        nextRoom = potal.toNextRoom;
        player.GetGameplayTagSystem().AddTag(eTagType.Player_State_IgnoreInput);
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
        
        //점프시작 애니메이션 끝날때까지 대기
        while(player.GetModelController().GetState() != AnimState.InAir)
        {          
            yield return null;
        }

        //트레벌중인 플레이어의 뱡항을 다음 방향 로테이션
        if(target.nextPoint != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position.normalized);
            player.transform.rotation = targetRotation;
        }  

        // 포물선 이동 로직
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
    
           
            time += Time.deltaTime;
            yield return null;
        }
        
        //착지 이후 애니메이션 재생
        player.transform.position = end;
        yield return null;
        player.OnJumpEnd();
        while (player.GetModelController().GetState() != AnimState.Idle)
        {
            yield return null;
        }
        

        //착지 애니메이션이 끝나면 다음 로직 체크
        if (target.nextPoint != null)
        {
            flow++;
            StartCoroutine(TraverseTo(player,target.nextPoint));
        }
        else
        {
            isTravaling = false;
            cams[0].Priority = 0;
            cams[1].Priority = 0;
            flow = 0;
            // 마지막 점프 끝! 조작 가능 상태로 복구
            player.GetGameplayTagSystem().RemoveTag(eTagType.Player_State_IgnoreInput);
            player.PortalDelay();            
            player.PlayAnimIdle();

            if(nextRoom == null)
            {
                Debug.LogError("PlayerTraversal => Next Room is Null");
            }
            else
            {
                StartCoroutine(GameManager.instance.ChangeCurrentRoom(nextRoom));
            }
            
        }


    }
}
