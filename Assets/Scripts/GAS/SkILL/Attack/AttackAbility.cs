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
    [Header("���� ����")]
    protected WeaponSetting weaponSetting;

    //������ ���� ī��Ʈ
    [SerializeField] public int fireCount = 1; // �߻� Ƚ��
    //��Ƽ�� ��ų ȹ�� ��
    [SerializeField] public float fireMultypleAngleRange = 20f;
    [SerializeField] public int fireMultypleCount = 1;

    public eWeaponType eWeaponType;
    
    public eTagType Equip_state_Tag = eTagType.Equip_Weapon_State_default;

    private bool isExecuting = false;

    //�������϶�
    protected eTagType stateTagType = eTagType.Attacking;

    protected override IEnumerator ExecuteAbility()
    {
        Task task = animTask.AnimExecute(AnimState.Attack);        
        yield return new WaitForTask(task);
        owner.GetGameplayTagSystem().AddTag(stateTagType);
            
        EndAbility();
    }


    private void CreateDetectObject()
    {
        float takeDamage = owner.attribute.GetCurValue(eAttributeType.Attack);
        Vector3 spherePosition = owner.transform.position + owner.transform.forward * 1f; // ���鿡�� +3 �̵�
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
        }
    }

    protected override void OnFireAnimationApply()
    {
        base.OnFireAnimationApply();
        CreateDetectObject();
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
    