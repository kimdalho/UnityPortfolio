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
            // ȸ���ϴ� ������ ���ظ� ���� ����
            gameObject.layer = LayerMask.NameToLayer("Default");

            calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            // Monster�� Update �������� ȣ��Ǳ� ���� HitAction�� ȣ��ǹǷ� �̸� �ѹ� ȣ��
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
            // ���� ������ �ش� ���� ����
            if (isGrounded)
            {
                IsHit = false;
                IsEvade = false;
                // �ٽ� ���ظ� ���� �� �ִ� ���·� Ȱ��ȭ
                gameObject.layer = LayerMask.NameToLayer("Monster");
            }
            else
            {
                // ���� �ϴ� �������� �̵� Update
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
