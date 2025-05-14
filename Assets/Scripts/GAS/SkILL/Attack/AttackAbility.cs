using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 1.�÷��̾� ���� ��� Attack�����Ƽ�� �������ִ�
/// 2.�÷��̾� ���� ��� tagAttack�� �������ִ�.
/// 3.�÷��̾��� ��� �پ��� ���⸦ �����Ҽ��ִ�.
/// 4.�÷��̾ ���⸦ ��ü�Ұ�� ������ tag�� Ability�� �����Ѵ�.
/// 5.�÷��̾�� Equip_state_Tag�� ���� ������ ����Ÿ���� gameplayTagSystem�� �˷��ش�.
/// 
/// </summary>
public class AttackAbility : GameAbility
{

    //������ ���� ī��Ʈ
    [SerializeField] public int fireCount = 1; // �߻� Ƚ��
    //��Ƽ�� ��ų ȹ�� ��
    [SerializeField] public float fireMultypleAngleRange = 20f;
    [SerializeField] public int fireMultypleCount = 1;

    public eWeaponType eWeaponType;
    
    public eTagType Equip_state_Tag = eTagType.Equip_Weapon_State_default;
    
    //�������϶�
    protected eTagType stateTagType = eTagType.Attacking;
    protected readonly string String_AttackTrigger = "Trg_Attack";
    
    protected override IEnumerator ExecuteAbility()
    {         
        Character character = owner.GetComponent<Character>();
        Animator animator = character.GetAnimator();
        //character.GetAnimator().SetTrigger("Trg_Attack");
        //AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        //float animationDuration = animationState.length;
        //if (animationDuration > Duration)
        //{
        //    Duration = animationDuration;
        //}

        GameAbilityTask animTask = new GameAbilityTask(owner);
        Task task = animTask.AnimExecute(AnimState.Attack);
        //yield return new WaitForTask(task);






        owner.GetGameplayTagSystem().AddTag(stateTagType);

        CreateDetectObject();
       
        float delay = Duration / Mathf.Max(character.attribute.GetCurValue(eAttributeType.AttackSpeed), 0.01f); // 0���� ������ �� ����
        Debug.Log($"Duration {Duration} , {delay}");
        yield return new WaitForSeconds(delay);  // ���� ȿ�� ó��
        EndAbility();
    }


    private void CreateDetectObject()
    {
        float takeDamage = owner.attribute.GetCurValue(eAttributeType.Attack);
        Vector3 spherePosition = owner.transform.position + owner.transform.forward * 1f; // ���鿡�� +3 �̵�
        int layerMask = LayerMask.GetMask("Item"); // "Enemy" ���̾ ����
        Collider[] results = SphereDetector.DetectObjectsInSphere(spherePosition, 1, targetMask);
        foreach (var col in results)
        {
            AttributeEntity ae = col.GetComponent<AttributeEntity>();
            if (ae != null) 
            {

                var character = ae as Character;

                if (character != null && character.fxSystem != null)
                {
                    GameEffect effect = new GameEffect(eModifier.Add);
                    effect.AddModifier(eAttributeType.Health, -takeDamage);
                    ae.ApplyEffect(effect);
                }
                else
                {
                    Debug.LogWarning("���� ��� FX�� ����");
                }
            }

            Debug.Log("Detected: " + col.name);
        }
    }
    
    public override void EndAbility()
    {
        base.EndAbility();
        if (owner == null)
            return;
        
        if(owner.GetGameplayTagSystem().HasTag(stateTagType))       
            owner.GetGameplayTagSystem().RemoveTag(stateTagType);
    }

}
    