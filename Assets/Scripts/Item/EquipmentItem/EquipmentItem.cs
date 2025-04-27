using NUnit.Framework.Interfaces;
using System;
using UnityEngine;

public abstract class EquipmentItem : GameEffect, IPickupable
{
    public ItemData itemData;
    //��� �������� ��� �������� Ÿ���� �������ִ�.
    public eEuipmentType partType;
    public eTagType skilltag;
    public GameObject ability;
    private Vector3 rotationSpeed = new Vector3(0, 30, 0);
    //�ִϸ��̼� ����
    public RuntimeAnimatorController itemAnim;

    public (ItemData, GameAttribute) GetItemData() 
    {
        return (itemData, effect);
    }
    

    public virtual void OnPickup(Character source, GameObject picker) 
    {        
        Player player = source.GetComponent<Player>();

        if (ability != null)
        {
            var newskill = Instantiate(ability);
            var skillCompo = newskill.GetComponent<GameAbility>();
            player.GetAbilitySystem().AddAndActivateAbility(skilltag, skillCompo,source);
        }
        else
        {
            var newSKill = new GameObject();
            var skillCompo = CreateSkill(skilltag, newSKill);
            player.GetAbilitySystem().AddAndActivateAbility(skilltag, skillCompo, source);                      
        }

        ApplyGameplayEffectToSelf(source, partType);
        gameObject.SetActive(false);
    }




    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
    /// <summary>
    /// ��¦ �߸�®��.. ���ʿ� �����̶� �������� ������ Ŭ������ ������ٸ� �̰� �ʿ���µ�..
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public GameAbility CreateSkill(eTagType tag,GameObject obj)
    { 
        switch(tag)
        {
            case eTagType.ninjahead:
                return obj.AddComponent<GA_NinjaHead>();
            case eTagType.alienhead:
                return obj.AddComponent<GA_AlienHead>();
            case eTagType.bearhead:
                return obj.AddComponent<GA_BearHead>();
            case eTagType.ninjabody:
                return obj.AddComponent<GA_NinjaBody>();
            case eTagType.grasshead:
                return obj.AddComponent<GA_GrassHead>();
            case eTagType.Roostershead:
                return obj.AddComponent<GA_RoostersHead>();
            case eTagType.clownhair:
                return obj.AddComponent<GA_ClownHair>();
            case eTagType.boxhead:
                return obj.AddComponent<GA_BoxHead>();
            case eTagType.beartorso:
                return obj.AddComponent<GA_BearTorso>();
            case eTagType.grasstrunk:
                return obj.AddComponent<GA_GrassTrunk>();
            case eTagType.Roostersbody:
                return obj.AddComponent<GA_RoostersBody>();
            case eTagType.clowntorso:
                return obj.AddComponent<GA_ClownTorso>();
            case eTagType.boxbody:
                return obj.AddComponent<GA_BoxBody>();
            default:
                break;
               
        }

        return null;


    }


    public virtual void Init(PickupItemData data) 
    {
        ability = data.gameAbility;
        itemData.itemName = data.itemData.itemName;
        itemData.description = data.itemData.description;
        effect = data.attribute;
        partType = data.eEquipmentType;
        skilltag = data.tag;
    }

    public virtual void Init(PickupWeaponItemData data)
    {
        ability = data.gameAbility;
        itemData.itemName = data.itemData.itemName;
        itemData.description = data.itemData.description;
        itemAnim = data.runtimeAnimatorController;        
        partType = data.eEquipmentType;
        skilltag = data.tag;
    }


/// <summary>
/// ��� Ÿ���� ����,�Ӹ�,�ٵ�,���� ������� ������ ȿ���� �������ִ�.
/// �ڽĿ��� ȿ���� �ο��ϴµ� �߿��Ѱ� �����Ƽ�� �ߵ��ؾ��Ѵٴ���
/// �׸��� �±׸� �������Ѵٴ���
/// Ư�� ���� ���� ���� �ִϸ��̼� ��ä�� �ٲ��.
/// </summary>


}


