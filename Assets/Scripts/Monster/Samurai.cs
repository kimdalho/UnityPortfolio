using System.Collections;
using UnityEngine;

public class Samurai : Monster
{
    private bool IsEvade = false;
    public float evadeRatio = 0.3f;

    protected override void ExecuteAttack()
    {
        // Create Dectect Object
        var _hits = SphereDetector.DetectObjectsInSphere(transform.position + transform.forward * attackRange, 1, LayerMask.GetMask("Player"));
        if (_hits == null || _hits.Length.Equals(0)) return;

        if (_hits[0].TryGetComponent<AttributeEntity>(out var _obj))
        {
            var _effect = new GameEffect(new DamageExecution());
            _effect.Apply(this, _obj);
        }
    }

    protected override void TakeDamage()
    {
        // ������ �¾��� �� ȸ�� ������ ������ ��������� ĳ���Ϳ� �־�������
        if (Random.Range(0f, 1f) <= evadeRatio && attribute.CurHart > 0)
        {
            IsAtk = false;
            animator.SetTrigger("Evade");
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

    protected override void HitAction()
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
                characterController.Move(-transform.forward * Time.deltaTime * attribute.speed);
            }
        }
        else
        {
            base.HitAction();
        }
    }
}
