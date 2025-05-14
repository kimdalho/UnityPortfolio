using System;
using System.Collections;
using UnityEngine;

public class Samurai : BlackHood
{
    private bool IsEvade = false;
    public float evadeRatio = 0.3f;

    protected override void TakeDamage()
    {
        var CurHealth = attribute.GetCurValue(eAttributeType.Health);
        
        if (UnityEngine.Random.Range(0f, 1f) <= evadeRatio && CurHealth > 0)
        {
            GetAnimator().SetTrigger("Evade");
            // 회피하는 동안은 피해를 입지 않음
            gameObject.layer = LayerMask.NameToLayer("Default");

            calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            // Monster의 Update 프레임이 호출되기 전에 HitAction이 호출되므로 미리 한번 호출
            ApplyGravity();

            IsEvade = true;
            IsHit = true;
        }
        else
        {
            base.TakeDamage();
        }
    }

    public override void HitAction()
    {
        if (IsEvade)
        {
            // 땅에 닿으면 해당 상태 종료
            if (isGrounded)
            {
                IsHit = false;
                IsEvade = false;
                // 다시 피해를 받을 수 있는 상태로 활성화
                gameObject.layer = LayerMask.NameToLayer("Monster");
            }
            else
            {
                // 덤블링 하는 방향으로 이동 Update
                var speed = attribute.GetCurValue(eAttributeType.Speed);
                characterController.Move(-transform.forward * Time.deltaTime * speed);
            }
        }
        else
        {
            base.HitAction();
        }
    }
}
